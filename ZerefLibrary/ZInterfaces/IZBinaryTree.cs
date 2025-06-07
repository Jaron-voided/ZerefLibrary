namespace ZerefLibrary.ZInterfaces;

public interface IZBinaryTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    TValue this[TKey key] { get; set; }
    IZBinaryTreeNode Root { get; }
    int Count { get; }
    /*ICollection<TKey> Keys { get; }
    ICollection<TValue> Values { get; }*/
    void Add(TKey key, TValue value);
    TKey AddWithoutKey(TValue value, out TKey key);
    bool ContainsKey(TKey key);
    bool TryGetValue(TKey key, out TValue value);
    //TValue GetValue(TKey key);
    TKey GetFirstKey(TValue value);
    IEnumerable<TKey> GetAllKeys(TValue value);
    
    TKey SetRootWithoutKey(TValue item, out TKey key);
    // Should I have overloads for all these methods?
    void SetRoot(TKey key, TValue item);
    TKey InsertWithoutKey(TValue value, out TKey key);
    // If I insert it in the middle somewhere, do I have to move everything else around?
    void Insert(TKey key, TValue value);
    
    bool RemoveFirst(TValue item);
    bool RemoveAt(TKey key);
    void RemoveAll(TValue item);
    
    // Could these be paired together also?
    void RemoveLeft(IZBinaryTreeNode parent);
    void RemoveRight(IZBinaryTreeNode parent);
    
    bool Contains(TValue item);
    
    // I can't return TKey and TValue for the IEnumerables
    IEnumerable<KeyValuePair<TKey, TValue>> TraverseInOrder();
    IEnumerable<KeyValuePair<TKey, TValue>> TraversePreOrder();
    IEnumerable<KeyValuePair<TKey, TValue>> TraversePostOrder();
    
    void Clear();
    
    // Should I remove the TKey TValue from the node so it's not being shadowed?

    public interface IZBinaryTreeNode/*<TKey, TValue>*/
    {
        IZBinaryTreeNode Left { get; set; }

        IZBinaryTreeNode Right { get; set; }

        // Value last since it might be different sizes
        TKey Key { get; }
        TValue Value { get; set; }
    }
}