using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZStack<T, S> : IZStack<T> where S : IZList<T>
{
    private IZList<T> _zlist;

    // Make this a factory method
    public ZStack(IZList<T> storage)
    {
        _zlist = storage;
    }
    
    public T Peek()
    {
        if (Count <= 0)
            throw new ArgumentOutOfRangeException(nameof(Count));
        
        return _zlist[Count - 1];
    }

    public void Push(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        
        _zlist.Add(item);
    }

    public void Pop()
    {
        if (Count <= 0)
            throw new ArgumentOutOfRangeException(nameof(Count));

        _zlist.RemoveAt(Count - 1);
    }

    public bool TryPop(out T item)
    {
        if (Count == 0)
        {
            item = default;
            return false;
        }

        item = _zlist[Count - 1];
        _zlist.RemoveAt(Count - 1);
        return true;
    }

    public int Count => _zlist.Count;
}
