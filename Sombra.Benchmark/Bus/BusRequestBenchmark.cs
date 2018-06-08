using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using Moq;
using Sombra.Messaging;

namespace Sombra.Benchmark.Bus
{
    public static class BusRequestBenchmark
    {
        public static async Task Run()
        {
            Console.WriteLine("BusRequestBenchmark starting");
            var request = new BenchmarkRequest();
            var bus = new Mock<IBus>();
            bus.Setup(m => m.PublishAsync(It.IsAny<BenchmarkRequest>())).Returns(Task.FromResult(new BenchmarkResponse()));

            var reflectionStopWatch = new Stopwatch();
            var dictionaryStopWatch = new Stopwatch();

            reflectionStopWatch.Start();
            for (var i = 0; i < 1000; i++)
            {
                await RequestAsyncWithRepeatedReflection(bus.Object, request);
            }

            reflectionStopWatch.Stop();


            dictionaryStopWatch.Start();
            for (var i = 0; i < 1000; i++)
            {
                await RequestAsyncWithConcurrentDictionary(bus.Object, request);
            }

            dictionaryStopWatch.Stop();

            Console.WriteLine("BusRequestBenchmark finished");
            Console.WriteLine($"1000 calls on RequestAsyncWithRepeatedReflection took {reflectionStopWatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"1000 calls on RequestAsyncWithConcurrentDictionary took {dictionaryStopWatch.ElapsedMilliseconds}ms");
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

        public class BenchmarkRequest : Request<BenchmarkResponse> { }

        public class BenchmarkResponse : Response { }
    }
}