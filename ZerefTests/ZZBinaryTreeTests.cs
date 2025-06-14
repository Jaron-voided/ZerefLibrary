using ZerefLibrary.ZCollections;

namespace ZerefTests;

public class ZZBinaryTreeTests
{
    [Fact]
    public void Create_ShouldInitializeEmptyTree()
    {
        var tree = ZZBinaryTree<int, string>.Create();
        Assert.Equal(0, tree.Count);
    }

    [Fact]
    public void CreateWithRoot_ShouldHaveOneNode()
    {
        var tree = ZZBinaryTree<int, string>.Create(1, "root");
        Assert.Equal(1, tree.Count);
        Assert.True(tree.ContainsKey(1));
        Assert.Equal("root", tree[1]);
    }

    [Fact]
    public void Add_ShouldIncreaseCount()
    {
        var tree = ZZBinaryTree<int, string>.Create();
        tree.Add(5, "five");
        Assert.Equal(1, tree.Count);
        Assert.True(tree.ContainsKey(5));
    }

    [Fact]
    public void Indexer_ShouldReturnCorrectValue()
    {
        var tree = ZZBinaryTree<int, string>.Create();
        tree.Add(10, "ten");
        var value = tree[10];
        Assert.Equal("ten", value);
    }

    [Fact]
    public void Remove_ShouldRemoveNode()
    {
        var tree = ZZBinaryTree<int, string>.Create();
        tree.Add(7, "seven");
        var result = tree.Remove(7);
        Assert.True(result);
        Assert.False(tree.ContainsKey(7));
    }

    [Fact]
    public void Clear_ShouldResetTree()
    {
        var tree = ZZBinaryTree<int, string>.Create();
        tree.Add(1, "one");
        tree.Add(2, "two");
        tree.Clear();
        Assert.Equal(0, tree.Count);
        Assert.False(tree.ContainsKey(1));
    }

    [Fact]
    public void TryGetValue_ShouldReturnTrueAndValue()
    {
        var tree = ZZBinaryTree<int, string>.Create();
        tree.Add(3, "three");
        var result = tree.TryGetValue(3, out var value);
        Assert.True(result);
        Assert.Equal("three", value);
    }

    [Fact]
    public void TryGetValue_ShouldReturnFalseIfMissing()
    {
        var tree = ZZBinaryTree<int, string>.Create();
        var result = tree.TryGetValue(99, out var value);
        Assert.False(result);
        Assert.Null(value);
    }

    [Fact]
    public void KeysAndValues_ShouldReturnCorrectCollections()
    {
        var tree = ZZBinaryTree<int, string>.Create();
        tree.Add(1, "one");
        tree.Add(2, "two");
        var keys = tree.Keys;
        var values = tree.Values;
        Assert.Contains(1, keys);
        Assert.Contains(2, keys);
        Assert.Contains("one", values);
        Assert.Contains("two", values);
    }
}
