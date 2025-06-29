using BenchmarkDotNet.Running;
using ZerefPerformance;

/*
ZThroughPutPlot.PlotThroughPut();*/
/*BenchmarkRunner.Run<ZThroughputTests>();*/
/*ZCacheTests.ZCacheLoop();*/

/*List<(int, double)> results = ZThroughPutManualTests.ZSumTestLoop();
foreach (var (size, time) in results)
{
    Console.WriteLine($"Size = {size} , Time = {time}");
}*/

ZThroughPutManualTests.RunZeeTests();