using System.Data.SqlTypes;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

// Type S is never used, what am I doing wrong here?
public class ZQueue<T, S> : IZQueue<T> where S : IZList<T>
{
    private IZList<T> _zlist;

    public ZQueue(IZList<T> storage)
    {
        _zlist = storage;
    }
    
    public T Peek()
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Count);
  
        return _zlist[0];
    }

    public void Enqueue(T item)
    {
        // Boxing allocation again!
        ArgumentNullException.ThrowIfNull(item);
        
        _zlist.Add(item);
    }

    public void Dequeue()
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Count);
        
        _zlist.RemoveAt(0);
    }

    public bool TryDequeue(out T item)
    {
        if (Count == 0)
        {
            item = default;
            return false;
        }

        item = _zlist[0];
        _zlist.RemoveAt(0);
        return true;
    }

    public int Count => _zlist.Count;
}