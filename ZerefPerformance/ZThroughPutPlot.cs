using ScottPlot;
namespace ZerefPerformance;

public class ZThroughPutPlot
{
    public static void PlotThroughPut()
    {
        // Data sizes
        double[] Ns = { 100_000, 1_000_000, 10_000_000 };

        // Means in microseconds → convert to milliseconds directly
        double[] sumMeans = { 0.2525, 2.0116, 33.0247 };
        double[] easyMutateSame = { 0.2840, 2.2843, 34.3645 };
        double[] hardMutateSame = { 0.7351, 6.7708, 63.9558 };
        double[] easyMutateDiff = { 0.3521, 4.3030, 55.4490 };
        double[] hardMutateDiff = { 0.8373, 8.7794, 94.9507 };

        // Create plot
        var plt = new ScottPlot.Plot();

        var s1 = plt.Add.Scatter(Ns, sumMeans);
        s1.Label = "Sum";

        var s2 = plt.Add.Scatter(Ns, easyMutateSame);
        s2.Label = "Easy Mutate Same Place";

        var s3 = plt.Add.Scatter(Ns, hardMutateSame);
        s3.Label = "Hard Mutate Same Place";

        var s4 = plt.Add.Scatter(Ns, easyMutateDiff);
        s4.Label = "Easy Mutate Different Place";

        var s5 = plt.Add.Scatter(Ns, hardMutateDiff);
        s5.Label = "Hard Mutate Different Place";

        plt.Axes.Title.Label.Text = "Throughput Benchmark";
        plt.Axes.Bottom.Label.Text = "N (Data Size)";
        plt.Axes.Left.Label.Text = "Time (ms)";
        plt.Legend.IsVisible = true;
        

        plt.SavePng("benchmark_plot2.png", 800, 600);

        Console.WriteLine("Plot saved as 'benchmark_plot.png'");
    }
   
}