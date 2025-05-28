using ZerefLibrary.ZCollections;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefTests;

public class ZQueueTests
{
    [Fact]
    public void Enqueue_AddsItemToQueue()
    {
        IZList<int> zlist = new ZList<int>();
        var queue = new ZQueue<int, IZList<int>>(zlist);

        queue.Enqueue(10);

        Assert.Equal(1, queue.Count);
        Assert.Equal(10, queue.Peek());
    }

    [Fact]
    public void Dequeue_RemovesFrontItem()
    {
        IZList<int> zlist = new ZList<int>();
        var queue = new ZQueue<int, IZList<int>>(zlist);


        queue.Enqueue(1);
        queue.Enqueue(2);

        queue.Dequeue();

        Assert.Equal(1, queue.Count);
        Assert.Equal(2, queue.Peek());
    }

    [Fact]
    public void TryDequeue_ReturnsTrueAndItem_WhenNotEmpty()
    {
        IZList<int> zlist = new ZList<int>();
        var queue = new ZQueue<int, IZList<int>>(zlist);


        queue.Enqueue(4);

        var result = queue.TryDequeue(out var item);

        Assert.True(result);
        Assert.Equal(4, item);
        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void TryDequeue_ReturnsFalse_WhenEmpty()
    {
        IZList<int> zlist = new ZList<int>();
        var queue = new ZQueue<int, IZList<int>>(zlist);


        var result = queue.TryDequeue(out var item);

        Assert.False(result);
    }

    [Fact]
    public void Peek_Throws_WhenEmpty()
    {
        IZList<int> zlist = new ZList<int>();
        var queue = new ZQueue<int, IZList<int>>(zlist);


        Assert.Throws<ArgumentOutOfRangeException>(() => queue.Peek());
    }
}