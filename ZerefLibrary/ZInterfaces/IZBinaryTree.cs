namespace ZerefLibrary.ZInterfaces;

internal interface IZBinaryTree<TKey, TValue>
{
    IZBinaryTreeNode Root { get; }
    int Count { get; }
    
    bool ContainsKey(TKey key);
    TValue GetValue(TKey key);
    TKey GetFirstKey(TValue value);
    IEnumerable<TKey> GetAllKeys(TValue value);
    
    void SetRoot(TValue item);
    // Should I have overloads for all these methods?
    void SetRoot(TKey key, TValue item);
    /*void InsertLeft(IZBinaryTreeNode<T> parent, T item);
    void InsertRight(IZBinaryTreeNode<T> parent, T item);*/

    void Insert(TValue value);
    // If I insert it in the middle somewhere, do I have to move everything else around?
    void Insert(TKey key, TValue value);
    
    void Remove(TValue item);
    void RemoveAt(TKey key);
    void RemoveAll(TValue item);
    
    // Could these be paired together also?
    void RemoveLeft(IZBinaryTreeNode parent);
    void RemoveRight(IZBinaryTreeNode parent);
    
    bool Contains(TValue item);
    
    // I can't return TKey and TValue for the IEnumerables
    IEnumerable<TValue> TraverseInOrder();
    IEnumerable<TValue> TraversePreOrder();
    IEnumerable<TValue> TraversePostOrder();
    
    void Clear();
    
    // Should I remove the TKey TValue from the node so it's not being shadowed?

    internal interface IZBinaryTreeNode/*<TKey, TValue>*/
    {
        IZBinaryTreeNode Left { get; set; }

        IZBinaryTreeNode Right { get; set; }

        // Value last since it might be different sizes
        TKey Key { get; }
        TValue Value { get; set; }
    }
}