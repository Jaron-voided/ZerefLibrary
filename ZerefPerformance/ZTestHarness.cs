using System.Diagnostics;

namespace ZerefPerformance;

public static class ZTestHarness
{
    public static double RunTest(Action testMethod, int iterations = 1000, int warmup = 50, int trim = 10)
    {
        List<long> timings = new List<long>();

        // Let the test warmup before collecting results
        for (int i = 0; i < warmup; i++) testMethod();

        for (int i = 0; i < iterations; i++)
        {
            var stopwatch = Stopwatch.StartNew();
            testMethod();
            stopwatch.Stop();
            timings.Add(stopwatch.ElapsedMilliseconds);
        }
        
        // Drop outliers
        timings.Sort();
        var trimmed = timings.Skip(trim).Take(timings.Count - trim * 2).ToList();

        var average = trimmed.Average();
        // Console.WriteLine($"Average Time: {average} ticks over {trimmed.Count} runs");
        Console.WriteLine("done");
        return average;
    }
}