using ZerefLibrary.ZCollections;

namespace ZerefLibrary.ZInterfaces;

public interface IZKeyValueStore<TKey, TValue> 
    : IEnumerable<KeyValuePair<TKey, TValue>> 
    where TKey : IComparable<TKey>, IEquatable<TKey>
{
    // Properties
    TValue this[TKey key] { get; set; }
    int Count { get; }
    ICollection<TKey> Keys { get; }
    ICollection<TValue> Values { get; }

    // Methods 
    void Add(TKey key, TValue value);
    bool ContainsKey(TKey key);
    bool TryGetValue(TKey key, out TValue value);

    // Copys values into an array
    TValue[] CopyTo();
    bool Remove(TKey key);
    void Clear();
}