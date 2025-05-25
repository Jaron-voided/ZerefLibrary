using ZerefLibrary.ZCollections;

namespace ZerefTests;

public class ZListTests
{
    [Fact]
    public void Add_ShouldIncreaseCount()
    {
        // Arrange
        var list = new ZList<int>();

        // Act
        list.Add(42);

        // Assert
        Assert.Equal(1, list.Count);
        Assert.Equal(42, list[0]);   
        
        // Failing
        /*Assert.Equal(2, list.Count);
        Assert.Equal(4, list[0]);*/
    }
    
    [Fact]
    public void Add_ShouldResizeArray()
    {
        // Arrange
        var list = new ZList<int>();

        // Act
        list.Add(42);
        list.Add(2);
        list.Add(5);
        list.Add(8);
        list.Add(45);
        list.Add(89);
        list.Add(1);
        list.Add(23);
        list.Add(65);

        // Assert
        Assert.Equal(9, list.Count);
        Assert.Equal(65, list[8]);
        Assert.Equal(16, list._items.Length);
        
        // Failing
        /*Assert.Equal(91, list.Count);
        Assert.Equal(6, list[8]);
        Assert.Equal(12, list._items.Length);*/
    }
    
    [Fact]
    public void Indexer_ShouldIndex()
    {
        // Arrange
        var list = new ZList<int>();

        // Act
        list.Add(42);
        list.Add(2);
        list.Add(5);
        list.Add(8);
        list.Add(45);
        list.Add(89);
        list.Add(1);
        list.Add(23);
        list.Add(65);

        var zero = list[0];
        var one = list[1];
        var two = list[2];
        var three = list[3];

        // Assert
        Assert.Equal(zero, list[0]);
        Assert.Equal(one, list[1]);
        Assert.Equal(two, list[2]);
        Assert.Equal(three, list[3]);
        
        // Failing
        /*Assert.Equal(zero, list[1]);
        Assert.Equal(one, list[2]);
        Assert.Equal(two, list[3]);
        Assert.Equal(three, list[0]);*/
    }
    
    [Fact]
    public void Indexer_ShouldThrowException()
    {
        // Arrange
        var list = new ZList<int>();

        // Act
        list.Add(42);
        list.Add(2);

        // Assert
        Assert.Throws<IndexOutOfRangeException>(() => list[-1]);
        Assert.Throws<IndexOutOfRangeException>(() => list[list.Count]);
        
        // Failing
        /*Assert.Throws<IndexOutOfRangeException>(() => list[0]);
        Assert.Throws<IndexOutOfRangeException>(() => list[1]);*/
    }
    
    [Fact]
    public void Remove_ShouldDecreaseCount()
    {
        // Arrange
        var list = new ZList<int>();

        // Act
        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);   // Count should be 4

        list.Remove(2);
        list.Remove(0);

        // Assert
        Assert.Equal(2, list.Count);
        Assert.Equal(43, list[0]);
        
        // Failing
        /*Assert.Equal(1, list.Count);
        Assert.Equal(42, list[0]);*/
    }
}