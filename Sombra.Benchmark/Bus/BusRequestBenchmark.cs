using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using Moq;
using Sombra.Messaging;

namespace Sombra.Benchmark.Bus
{
    public static class BusRequestBenchmark
    {
        private const int ITERATIONS = 1000000;
        public static async Task Run()
        {
            Console.WriteLine("BusRequestBenchmark starting");

            var intermediaryLogFormat = $"{ITERATIONS} calls on {{0}} took {{1}}ms";
            var finalLogFormat = "Average duration of {0} over {1} iterations is {2}ms";

            var reportA = await RunBenchmark(async (bus, req) => await RequestAsyncWithRepeatedReflection(bus, req), "RequestAsyncWithRepeatedReflection", intermediaryLogFormat, finalLogFormat, 20);
            var reportB = await RunBenchmark(async (bus, req) => await RequestAsyncWithConcurrentDictionary(bus, req), "RequestAsyncWithConcurrentDictionary", intermediaryLogFormat, finalLogFormat, 20);
            var reportC = await RunBenchmark(async (bus, req) => await RequestAsyncWithConcurrentDictionaryFuncs(bus, req), "RequestAsyncWithConcurrentDictionaryFuncs", intermediaryLogFormat, finalLogFormat, 20);
            var reportD = await RunBenchmark(async (bus, req) => await RequestAsyncWithConcurrentDictionaryDelegates(bus, req), "RequestAsyncWithConcurrentDictionaryDelegates", intermediaryLogFormat, finalLogFormat, 20);

            Console.WriteLine(reportA);
            Console.WriteLine(reportB);
            Console.WriteLine(reportC);
            Console.WriteLine(reportD);

            Console.WriteLine("BusRequestBenchmark finished");
        }

        private static async Task<string> RunBenchmark(Func<IBus, BenchmarkRequest, Task<BenchmarkResponse>> method, string methodName, string intermediaryLogFormat, string finalLogFormat, int iterations = 1)
        {
            var request = new BenchmarkRequest();
            long totalDuration = 0;

            for (var j = 0; j < iterations; j++)
            {
                var bus = new Mock<IBus>();
                bus.Setup(m => m.PublishAsync(It.IsAny<BenchmarkRequest>())).Returns(Task.FromResult(new BenchmarkResponse()));
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                for (var i = 0; i < ITERATIONS; i++)
                {
                    await method(bus.Object, request);
                }

                stopwatch.Stop();
                totalDuration += stopwatch.ElapsedMilliseconds;
                Console.WriteLine(intermediaryLogFormat, methodName, stopwatch.ElapsedMilliseconds);
            }

            return string.Format(finalLogFormat, methodName, iterations, totalDuration / iterations);
        }

        public static Task<TResponse> RequestAsyncWithRepeatedReflection<TResponse>(IBus bus, IRequest<TResponse> request)
            where TResponse : class, IResponse
        {
            var requestMethod = typeof(IBus).GetMethods().Single(m => m.Name == nameof(IBus.RequestAsync) && m.GetParameters().Length == 1);
            var typedRequestMethod = requestMethod.MakeGenericMethod(request.GetType(), typeof(TResponse));

            return (Task<TResponse>)typedRequestMethod.Invoke(bus, new object[] { request });
        }


        private static readonly MethodInfo _genericRequestMethod = typeof(IBus).GetMethods().Single(m => m.Name == nameof(IBus.RequestAsync) && m.GetParameters().Length == 1);
        private static readonly ConcurrentDictionary<Type, MethodInfo> _typedRequestMethods = new ConcurrentDictionary<Type, MethodInfo>();

        public static Task<TResponse> RequestAsyncWithConcurrentDictionary<TResponse>(IBus bus, IRequest<TResponse> request)
            where TResponse : class, IResponse
        {
            var requestMethod = _typedRequestMethods.GetOrAdd(request.GetType(),
                requestType => _genericRequestMethod.MakeGenericMethod(requestType, typeof(TResponse)));

            return (Task<TResponse>)requestMethod.Invoke(bus, new object[] { request });
        }

        private static readonly ConcurrentDictionary<Type, Func<object, object>> _typedRequestFuncs = new ConcurrentDictionary<Type, Func<object, object>>();

        public static Task<TResponse> RequestAsyncWithConcurrentDictionaryFuncs<TResponse>(IBus bus, IRequest<TResponse> request)
            where TResponse : class, IResponse
        {
            var requestMethod = _typedRequestFuncs.GetOrAdd(request.GetType(),
                requestType => (Func<object, object>) Delegate.CreateDelegate(typeof(Func<,>).MakeGenericType(requestType, typeof(Task<>).MakeGenericType(typeof(TResponse))) , _genericRequestMethod.MakeGenericMethod(requestType, typeof(TResponse))));

            return (Task<TResponse>)requestMethod(request);
        }

        private static readonly ConcurrentDictionary<Type, Delegate> _typedRequestDelegates = new ConcurrentDictionary<Type, Delegate>();

        public static Task<TResponse> RequestAsyncWithConcurrentDictionaryDelegates<TResponse>(IBus bus, IRequest<TResponse> request)
            where TResponse : class, IResponse
        {
            var requestMethod = _typedRequestDelegates.GetOrAdd(request.GetType(),
                requestType => Delegate.CreateDelegate(typeof(Func<,>).MakeGenericType(requestType, typeof(Task<>).MakeGenericType(typeof(TResponse))), _genericRequestMethod.MakeGenericMethod(requestType, typeof(TResponse))));

            return ((Func<IRequest<TResponse>, Task<TResponse>>)requestMethod)(request);
        }

        public class BenchmarkRequest : Request<BenchmarkResponse> { }

        public class BenchmarkResponse : Response { }
    }
}