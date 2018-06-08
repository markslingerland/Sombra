using System;
using System.Threading.Tasks;
using Sombra.Benchmark.Bus;

namespace Sombra.Benchmark
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Benchmark started");
            await BusRequestBenchmark.Run();
            Console.ReadLine();
        }
    }
}
