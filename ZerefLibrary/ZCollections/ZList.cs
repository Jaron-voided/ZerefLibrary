using System.Collections;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZList<T> : IZList<T>
{
    private T[] _items;
    // Why did these 2 get auto made?
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
        get => _items[index];
        set => _items[index] = value;
    }

    private int _count;
    public int Count => _count;

    public ZList()
    {
        _items = new T[2];
        _count = 0;
    }

    public void Add(T item)
    {
        if (_count < _items.Length)
        {
            _items[_count] = item;
            _count++;
        }
        else
        {
            _count++;
            
            // Should I only add one new spot?
            T[] newArray = new T[_count];
            Array.Copy(_items, newArray, _count);
            newArray[_count] = item;
            _items = newArray;
        }
    }

    public void Insert(int index, T item)
    {
        // I need to shift everything right?
        if (index <= _count)
        {
            _items[index] = item;
        }
        else
        {
            T[] newArray = new T[index];
            Array.Copy(_items, newArray, _count);
            newArray[index] = item;
            _items = newArray;
        }
    }

    public void Remove(T item)
    {
        // I could run the lower Remove function in this after the first line
        int index = Array.IndexOf(_items, item);
        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
        _count--;
    }

    public void Remove(int index)
    {
        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
        _count--;
    }

    public void Clear()
    {
        for (int i = 0; i < _count - 1; i++)
        {
            _items[i] = OU;
        }
    }
}