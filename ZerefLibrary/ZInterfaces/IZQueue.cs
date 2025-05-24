namespace ZerefLibrary.ZCollections.ZInterfaces;

// Why does the member go at the bottom?
public interface IZQueue<T>
{
    T Peek();
    void Enqueue(T item);
    void Dequeue();
    bool TryDequeue(out T item);
    int Count { get; }
}