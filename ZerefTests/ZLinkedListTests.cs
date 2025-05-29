using ZerefLibrary.ZCollections;

namespace ZerefTests;

public class ZLinkedListTests
{
    private ZLinkedList<int> MakeEmptyList()
    {
        return ZLinkedList<int>.MakeZLinkedList(Array.Empty<ZLinkedList<int>.ZLinkedListNode<int>>());
    }

    [Fact]
    public void Add_ShouldIncreaseCount()
    {
        var list = MakeEmptyList();

        list.Add(42);

        Assert.Equal(1, list.Count);
        Assert.Equal(42, list[0]);
    }

    [Fact]
    public void Insert_ShouldIncreaseCount()
    {
        var list = MakeEmptyList();

        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);

        int beforeCount = list.Count;
        list.Insert(2, 3);

        Assert.Equal(4, beforeCount);
        Assert.Equal(5, list.Count);
    }

    [Fact]
    public void Insert_ShouldInsertAtCorrectPosition()
    {
        var list = MakeEmptyList();

        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);

        list.Insert(2, 3);

        Assert.Equal(3, list[2]);
    }

    [Fact]
    public void Indexer_ShouldReturnCorrectValues()
    {
        var list = MakeEmptyList();

        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);

        Assert.Equal(42, list[0]);
        Assert.Equal(43, list[1]);
        Assert.Equal(2, list[2]);
        Assert.Equal(5, list[3]);
    }
    
    [Fact]
    public void Remove_ShouldDecreaseCount()
    {
        var list = MakeEmptyList();

        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);

        list.Remove(42);

        Assert.Equal(3, list.Count);
        Assert.Equal(43, list[0]);
    }

    [Fact]
    public void Remove_ShouldRemoveItem()
    {
        var list = MakeEmptyList();

        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);

        list.Remove(2);

        var found = false;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == 2) found = true;
        }

        Assert.False(found);
    }

    [Fact]
    public void RemoveAt_ShouldRemoveCorrectItem()
    {
        var list = MakeEmptyList();

        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);

        list.RemoveAt(1);

        Assert.Equal(2, list[1]);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void Clear_ShouldResetList()
    {
        var list = MakeEmptyList();

        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);

        list.Clear();

        Assert.Equal(0, list.Count);
    }

    [Fact]
    public void Enumerator_ShouldIterateThroughItems()
    {
        var list = MakeEmptyList();
        var items = new List<int> { 42, 43, 2, 5 };

        foreach (var item in items)
        {
            list.Add(item);
        }

        var index = 0;
        foreach (var value in list)
        {
            Assert.Equal(items[index], value);
            index++;
        }
    }
}
