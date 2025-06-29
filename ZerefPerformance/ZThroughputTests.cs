using BenchmarkDotNet.Attributes;

namespace ZerefPerformance;

public class ZThroughputTests
{
    [Params(50_000, 100_000, 200_000, 500_000, 1_000_000, 2_000_000, 5_000_000)]
    public int N;

    private int[] data;

    [GlobalSetup]
    public void Setup()
    {
        data = Enumerable.Range(0, N).ToArray();
    }
    
    // Testing moving data into CPU, I need to sum all the numbers so the compiler doesn't optimize
    [Benchmark]
    public int Sum()
    {
        var local = (int[])data.Clone();
        int sum = 0;

        for (int i = 0; i < N; i++)
        {
            sum += local[i];
        }

        return sum;
    }

    [Benchmark]
    public int EasyMutateSamePlace()
    {
        var local = (int[])data.Clone();
        int checkSum = 0;
        for (int i = 0; i < N; i++)
        {
            local[i] += 1;
            checkSum += local[i];
        }
        
        return checkSum;
    }
    
    [Benchmark]
    public int HardMutateSamePlace()
    {
        var local = (int[])data.Clone();
        int checkSum = 0;
        for (int i = 0; i < N; i++)
        {
            local[i] = (int)Math.Sqrt(local[i] * 123.456 + 789);
            checkSum += local[i];
        }
        
        return checkSum;
    }
    
    [Benchmark]
    public int EasyMutateDifferentPlace()
    {
        var local = (int[])data.Clone();
        int[] newData = new int[N];
        int checkSum = 0;
        
        for (int i = 0; i < N; i++)
        {
            newData[i] = local[i] + 1;
            checkSum += newData[i];
        }
        
        return checkSum;
    }
    
    [Benchmark]
    public int HardMutateDifferentPlace()
    {
        var local = (int[])data.Clone();
        int[] newData = new int[N];
        int checkSum = 0;
        
        for (int i = 0; i < N; i++)
        {
            newData[i] = (int)Math.Sqrt(local[i] * 123.456 + 789);
            checkSum += newData[i];
        }
        
        return checkSum;
    }
}