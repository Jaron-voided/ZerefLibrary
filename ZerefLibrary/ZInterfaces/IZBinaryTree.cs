using ZerefLibrary.ZCollections;

namespace ZerefLibrary.ZInterfaces;

public interface IZKeyValueStore<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
{
    // Properties
    TValue this[TKey key] { get; set; }
    int Count { get; }
    ICollection<TKey> Keys { get; }
    ICollection<TValue> Values { get; }
    
    // Methods 
    void Add(TKey key, TValue value);
    bool Contains(TValue item);
    bool ContainsKey(TKey key);
    bool ContainsValue(TValue value);
    bool TryGetValue(TKey key, out TValue value);
    
    
    // Copys values into an array
    TValue[] CopyTo(int index);
    bool Remove(TValue item);
    bool RemoveAt(TKey key);
    void Clear();
    
    // Traversals
    IEnumerable<KeyValuePair<TKey, TValue>> TraverseInOrder();
    IEnumerable<KeyValuePair<TKey, TValue>> TraversePreOrder();
    IEnumerable<KeyValuePair<TKey, TValue>> TraversePostOrder();
}