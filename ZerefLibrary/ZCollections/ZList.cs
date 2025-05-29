using System.Collections;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZList<T> : IZList<T>
{
    internal T[] Items;
    public int Count { get; private set; }
    public IEnumerator<T> GetEnumerator()
    {
        foreach (T item in Items)
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

    private void IsIndexInRange(int index)
    {
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException();
    }

    private void IsItemNull(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
    }

    private T GetThis(int index)
    {
        IsIndexInRange(index);
        return Items[index];
    }

    private void SetThat(int index, T value)
    {
        IsIndexInRange(index);
        Items[index] = value;
    }
    
    public static ZList<T> MakeZList(T[] items)
    {
        var list = new ZList<T>();
        list.Count = items.Length;
        list.Items = new T[list.Count * 8];
        Array.Copy(items, list.Items, items.Length);
        return list;
    }
    
    public void Add(T item)
    {
        IsItemNull(item);
        
        if (Count >= Items.Length)
        {
            T[] newArray = new T[Count * 2];
            Array.Copy(Items, newArray, Count);
            Items = newArray;
        }
        
        Insert(Count, item);
    }
    
    public void Insert(int index, T item)
    {
        IsItemNull(item);
        IsIndexInRange(index);

        for (int i = Count; i > index; i--)
        {
            Items[i] = Items[i - 1];
        }
        
        Items[index] = item;
        Count++;
    }
    
    public void Remove(T item)
    {
        int index = Array.IndexOf(Items, item);
        IsIndexInRange(index);
        
        RemoveAt(index);
    }
    
    public void RemoveAt(int index)
    {
        IsIndexInRange(index);

        for (int i = index; i < Count - 1; i++)
        {
            Items[i] = Items[i + 1];
        }
        
        Count--;
    }
    
    public void Clear()
    {
        Array.Clear(Items, 0, Count);
        Count = 0;
    }
}