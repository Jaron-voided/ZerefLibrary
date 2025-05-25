using System.Collections;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZList<T> : IZList<T>
{
    internal T[] _items;
    public int Count { get; private set; }
    public IEnumerator<T> GetEnumerator()
    {
        foreach (T item in _items)
        {
            yield return item;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public T this[int index]
    {
        get => GetThis(index);
        set => SetThat(index, value);
    }

    private T GetThis(int index)
    {
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException();
        return _items[index];
    }

    private void SetThat(int index, T value)
    {
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException();
        _items[index] = value;
    }
    
    public ZList()
    {
        _items = new T[8];
        Count = 0;
    }
    
    public void Add(T item)
    {
        if (Count < _items.Length)
        {
            _items[Count] = item;
            Count++;
        }
        else
        {
            // Maybe handle this differently instead of doubling??
            T[] newArray = new T[Count * 2];
            Array.Copy(_items, newArray, Count);
            newArray[Count] = item;
            _items = newArray;
            Count++;
        }
    }
    
    public void Insert(int index, T item)
    {
        // I need to shift everything right?
        if (index < Count)
        {
            // Shifts everything to the right one
            for (int i = Count; i > index; i--)
            {
                _items[i] = _items[i - 1];
            }
            
            _items[index] = item;
        }
        else
        {
            T[] newArray = new T[Count * 2];
            Array.Copy(_items, newArray, Count);
            newArray[index] = item;
            _items = newArray;
        }

        Count++;
    }
    
    public void Remove(T item)
    {
        int index = Array.IndexOf(_items, item);
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException();
        Remove(index);
    }
    
    public void Remove(int index)
    {
        for (int i = index; i < Count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
        
        Count--;
    }
    
    public void Clear()
    {
        Array.Clear(_items, 0, Count);
        Count = 0;
    }
}