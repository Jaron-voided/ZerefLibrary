using System.Diagnostics;

namespace ZerefPerformance;

public class ZThroughPutManualTests
{
    private static int[] Sizes = { 50_000, 100_000, 200_000, 500_000, 1_000_000/*, 2_000_000, 5_000_000*/ };

    public static void RunZeeTests()
    {
        // I want to put the functions in a IEnumerable and iterate over them!!
        //IEnumerable<Action<int>> tests = new List<Action<int>> { ZSumTest(43), };
        foreach (var size in Sizes)
        {
            var zSumTestResults = ZThroughputTestLoop(ZSumTest(size), size);
            var zEasyMutateSamePlaceTestResults = ZThroughputTestLoop(ZEasyMutateSamePlace(size), size);
            var zHardMutateSamePlaceTestResults = ZThroughputTestLoop(ZHardMutateSamePlace(size), size);
            var zEasyMutateDifferentPlaceResults = ZThroughputTestLoop(ZEasyMutateDifferentPlace(size), size);
            var zHardMutateDifferentPlaceTestResults = ZThroughputTestLoop(ZHardMutateDifferentPlace(size), size);
            
            Console.WriteLine($"\nTests for size {size}:");

            PrintResults("ZSumTest", zSumTestResults);
            PrintResults("ZEasyMutateSamePlace", zEasyMutateSamePlaceTestResults);
            PrintResults("ZHardMutateSamePlace", zHardMutateSamePlaceTestResults);
            PrintResults("ZEasyMutateDifferentPlace", zEasyMutateDifferentPlaceResults);
            PrintResults("ZHardMutateDifferentPlace", zHardMutateDifferentPlaceTestResults);
        }
    }

    public static List<(int, double)> ZThroughputTestLoop(Action action, int size)
    {
        var results = new List<(int size, double time)>();
        int done = 5;
        /*foreach (int size in Sizes)
        {*/
            for (int i = 0; i < done; i++)
            {
                // Why did I have to put the () => ??
                var time = ZTestHarness.RunTest(action);
                results.Add((size, time));
            }
        /*}*/

        return results;
    }
    static (int[] readBuffer, int[] writeBuffer, int[] flushBuffer) CreateArrays(int size)
    {
        int[] read = new int[size];
        int[] write = new int[size];
        int[] flush = new int[size * 3];
        
        read = Enumerable.Range(0, size).ToArray();
        write = Enumerable.Range(0, size).ToArray();
        flush = Enumerable.Range(0, size * 3).ToArray();
        
        return (read, write, flush);
    }
    
    public static Action ZSumTest(int size)
    {

        return () =>
        {
            var (readBuffer, writeBuffer, flushBuffer) = CreateArrays(size);
            int flushSum = 0;
            int checkSum = 0;
            
            int done = 5;

            for (int x = 0; x < done; x++)
            {
                for (int i = 0; i < flushBuffer.Length; i++)
                {
                    flushSum += flushBuffer[i];
                }
            }

            for (int i = 0; i < size; i++)
            {
                checkSum += readBuffer[i];
            }
        };
    }

    public static Action ZEasyMutateSamePlace(int size)
    {
        return () =>
        {
            var (readBuffer, writeBuffer, flushBuffer) = CreateArrays(size);
            int flushSum = 0;
            int checkSum = 0;
            
            int done = 5;


            for (int i = 0; i < done; i++)
            {
                readBuffer[i] += 1;
                checkSum += readBuffer[i];
            }
        };
    }
    
    public static Action ZHardMutateSamePlace(int size)
    {

        return () =>
        {
            var (readBuffer, writeBuffer, flushBuffer) = CreateArrays(size);
            int flushSum = 0;
            int checkSum = 0;

            int done = 5;

            for (int i = 0; i < done; i++)
            {
                readBuffer[i] = (int)Math.Sqrt(readBuffer[i] * 123.456 + 789);
                checkSum += readBuffer[i];
            }
        };
    }
    
    public static Action ZEasyMutateDifferentPlace(int size)
    {
        return () =>
        {
            var (readBuffer, writeBuffer, flushBuffer) = CreateArrays(size);
            int flushSum = 0;
            int checkSum = 0;

            int done = 5;

            for (int i = 0; i < done; i++)
            {
                writeBuffer[i] = readBuffer[i] + 1;
                checkSum += writeBuffer[i];
            }
        };
    }
    
    public static Action ZHardMutateDifferentPlace(int size)
    {
        return () =>
        {
            var (readBuffer, writeBuffer, flushBuffer) = CreateArrays(size);
            int flushSum = 0;
            int checkSum = 0;

            int done = 5;

            for (int i = 0; i < done; i++)
            {
                writeBuffer[i] = (int)Math.Sqrt(readBuffer[i] * 123.456 + 789);
                checkSum += writeBuffer[i];
            }
        };
    }

    static void PrintResults(string testName, List<(int, double)> results)
    {
        Console.WriteLine($"  {testName}:");

        foreach (var (sz, time) in results)
        {
            Console.WriteLine($"    Run with size {sz}, avg time: {time:F2} ms");
        }
    }

}