using ZerefLibrary.ZCollections;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefTests;

public class ZStackTests
{
    [Fact]
    public void Push_AddsItemToStack()
    {
        IZList<int> zlist = new ZList<int>();
        var stack = new ZStack<int, IZList<int>>(zlist);

        stack.Push(10);

        Assert.Equal(1, stack.Count);
        Assert.Equal(10, stack.Peek());
    }

    [Fact]
    public void Pop_RemovesTopItem()
    {
        IZList<int> zlist = new ZList<int>();
        var stack = new ZStack<int, IZList<int>>(zlist);

        stack.Push(1);
        stack.Push(2);

        stack.Pop();

        Assert.Equal(1, stack.Count);
        Assert.Equal(1, stack.Peek());
    }

    [Fact]
    public void TryPop_ReturnsTrueAndItem_WhenNotEmpty()
    {
        IZList<int> zlist = new ZList<int>();
        var stack = new ZStack<int, IZList<int>>(zlist);

        stack.Push(5);

        var result = stack.TryPop(out var item);

        Assert.True(result);
        Assert.Equal(5, item);
        Assert.Equal(0, stack.Count);
    }

    [Fact]
    public void TryPop_ReturnsFalse_WhenEmpty()
    {
        IZList<int> zlist = new ZList<int>();
        var stack = new ZStack<int, IZList<int>>(zlist);

        var result = stack.TryPop(out var item);

        Assert.False(result);
    }
}