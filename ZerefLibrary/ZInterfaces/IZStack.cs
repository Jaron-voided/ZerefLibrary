namespace ZerefLibrary.ZCollections.ZInterfaces;

public interface IZStack<T>
{
    T Peek();
    void Push(T item);
    void Pop();
    bool TryPop(out T item);
    int Count { get; }
}