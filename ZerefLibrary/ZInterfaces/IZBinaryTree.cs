namespace ZerefLibrary.ZCollections.ZInterfaces;

public interface IZBinaryTree<T>
{
    IZBinaryTreeNode<T> Root { get; }
    int Count { get; }
    
    void SetRoot(T item);
    void InsertLeft(IZBinaryTreeNode<T> parent, T item);
    void InsertRight(IZBinaryTreeNode<T> parent, T item);
    
    void Remove(T item);
    void RemoveAll(T item);
    void RemoveLeft(IZBinaryTreeNode<T> parent);
    void RemoveRight(IZBinaryTreeNode<T> parent);
    
    bool Contains(T item);
    
    // Action allows for a method to be passed. "PrintNode" fo each node thats traversed
    void TraverseInOrder(Action<T> action);
    void TraversePreOrder(Action<T> action);
    void TraversePostOrder(Action<T> action);
    
    void Clear();

    public interface IZBinaryTreeNode<T>
    {
        IZBinaryTreeNode<T> Left { get; set; }

        IZBinaryTreeNode<T> Right { get; set; }

        // Value last since it might be different sizes
        T Value { get; set; }
    }
}