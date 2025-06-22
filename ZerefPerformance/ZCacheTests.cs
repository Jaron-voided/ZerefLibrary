using Microsoft.Diagnostics.Tracing.Parsers.JScript;
using ScottPlot;

namespace ZerefPerformance;

public static class ZCacheTests
{
    public static Action ZCacheTest(int arraySize)
    {
        // I have to wrap this in a return function, because Action returns a function? I can't use Func on RunTest
        // Because not all methods will have returns??
        return () =>
        {
            int[] data = new int[arraySize];

            // Seed the random so its psuedo-random
            var rand = new Random(42);

            for (int i = 0; i < arraySize; i++)
            {
                data[i] = rand.Next(0, 1000);
            }

            for (int i = 0; i < data.Length; i++)
            {
                data[i] *= 2;
            }
        };
    }

    public static void ZCacheLoop()
    {
        int arraySize = 8;
        var results = new List<(int size, double time)>();
        int done = 22;

        for (int i = 0; i < done; i++)
        {
            var time = ZTestHarness.RunTest(ZCacheTest(arraySize));
            results.Add((arraySize, time));
            arraySize *= 2;
        }

        foreach (var (size, time) in results)
        {
            Console.WriteLine($"Size = {size}: Time = {time}: TimePerElement: {time/size}");
        }
  
        var plot = new ScottPlot.Plot();
        
        double[] sizes = results.Select(r => (double)r.size).ToArray();
        double[] times = results.Select(r => (double)r.time).ToArray();
        
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        plot.Add.Scatter(sizes, times);
        plot.XLabel("Array size (ints)");
        plot.YLabel("Average time (ms)");
        plot.Title("Zee Cache Performance Test");
        

        plot.SavePng($"cache_plot_{timestamp}.png", 800, 600);
    }
}