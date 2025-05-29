using ZerefLibrary.ZCollections;

namespace ZerefTests;

public class ZListTests
{
    [Fact]
    public void Add_ShouldIncreaseCount()
    {
        // Arrange
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

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
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

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
        Assert.Equal(16, list.Items.Length);
        
        // Failing
        /*Assert.Equal(91, list.Count);
        Assert.Equal(6, list[8]);
        Assert.Equal(12, list.Items.Length);*/
    }
    
    [Fact]
    public void Indexer_ShouldIndex()
    {
        // Arrange
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

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
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

        // Act
        list.Add(42);
        list.Add(2);

        // Assert
        Assert.Throws<IndexOutOfRangeException>(() => list[-1]);
        Assert.Throws<IndexOutOfRangeException>(() => list[list.Count + 30]);
        
        // Failing
        /*Assert.Throws<IndexOutOfRangeException>(() => list[0]);
        Assert.Throws<IndexOutOfRangeException>(() => list[1]);*/
    }
    
    [Fact]
    public void Remove_ShouldDecreaseCount()
    {
        // Arrange
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

        // Act
        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);   // Count should be 4

        list.Remove(42);

        // Assert
        Assert.Equal(3, list.Count);
        Assert.Equal(43, list[0]);
        
        // Failing
        /*Assert.Equal(1, list.Count);
        Assert.Equal(42, list[0]);*/
    }
    
    [Fact]
    public void Remove_ShouldRemove()
    {
        // Arrange
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

        // Act
        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);   // Count should be 4

        list.Remove(2);

        bool twoExists = list.Items.Contains(2);

        // Assert
        Assert.False(twoExists);
        
        // Failing
        /*
        Assert.True(twoExists);
    */
    }

    [Fact]
    public void RemoveAt_ShouldRemoveAt()
    {
        // Arrange
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

        // Act
        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);   // Count should be 4

        list.RemoveAt(1);

        bool indexExists = list.Items.Contains(43);

        // Assert
        Assert.False(indexExists);
        Assert.Equal(2, list[1]);
        
        // Failing
        /*
        Assert.True(indexExists);
        Assert.Equal(43, list[1]);
    */
    }
    
    [Fact]
    public void Insert_ShouldIncreaseCount()
    {
        // Arrange
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

        // Act
        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);   // Count should be 4
        int beforeCount = list.Count;

        list.Insert(2, 3);

        // Assert
        Assert.Equal(4, beforeCount);
        Assert.Equal(5, list.Count);
        
        // Failing
        /*Assert.Equal(2, beforeCount);
        Assert.Equal(3, list.Count);*/
    }
    
    [Fact]
    public void Insert_ShouldInsert()
    {
        // Arrange
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

        // Act
        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);   // Count should be 4

        list.Insert(2, 3);

        // Assert
        Assert.Equal(3, list[2]);

        
        // Failing
        /*Assert.Equal(3, list[1]);*/
    }
    
    [Fact]
    public void Clear_ShouldClear()
    {
        // Arrange
        int[] ints = [];
        var list = ZList<int>.MakeZList(ints);

        // Act
        list.Add(42);
        list.Add(43);
        list.Add(2);
        list.Add(5);   // Count should be 4

        list.Clear();

        // Assert
        Assert.Equal(0, list.Count);
        
        // Failing
        /*Assert.Equal(3, list.Count);*/
    }
}