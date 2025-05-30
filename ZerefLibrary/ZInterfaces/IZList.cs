namespace ZerefLibrary.ZCollections.ZInterfaces;

// had to add the <T> after IZList
public interface IZList<T> : IEnumerable<T>
{
    T this[int index] { get; set; }
    int Count { get; }
    void Add(T item);
    void Insert(int index, T item);
    void Remove(T item);
    void RemoveAt(int index);
    void RemoveAll(T item);
    void Clear();
}