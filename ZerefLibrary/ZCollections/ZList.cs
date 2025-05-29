using System.Collections;
using ZerefLibrary.utils;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZList<T> : IZList<T>
{
    internal T[] Items;
    public int Count { get; private set; }
    private const int StartingFactor = 8;
    private const int GrowthFactor = 2;
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
    
    private T GetThis(int index)
    {
        ErrorHandling<T>.ThrowIfOutOfRange(index, Count);
        return Items[index];
    }

    private void SetThat(int index, T value)
    {
        ErrorHandling<T>.ThrowIfOutOfRange(index, Count);
        Items[index] = value;
    }
    
    public static ZList<T> Create(T[] items)
    {
        var list = new ZList<T>();
        list.Count = items.Length;
        
        // If an empty list is passed, I still need some space to be allocated
        list.Items = list.Count == 0 ? new T[StartingFactor] : new T[list.Count * StartingFactor];
        
        Array.Copy(items, list.Items, items.Length);
        return list;
    }
    
    public void Add(T item)
    {
        ErrorHandling<T>.ThrowIfItemIsNull(item);
        
        Insert(Count, item);
    }
    
    public void Insert(int index, T item)
    {
        ErrorHandling<T>.ThrowIfItemIsNull(item);
        
        //Unsure if I need this currently.
        //ErrorHandling<T>.ThrowIfOutOfRange(index, Count);
        
        EnsureCapacity();

        // This is probably used elsewhere, try to extract it
        for (int i = Count; i > index; i--)
        {
            Items[i] = Items[i - 1];
        }
        
        Items[index] = item;
        Count++;
    }

    protected void EnsureCapacity()
    {
        if (Count >= Items.Length)
        {
            T[] newArray = new T[Count * GrowthFactor];
            Array.Copy(Items, newArray, Count);
            Items = newArray;
        }
    }

    public void Remove(T item)
    {
        int index = Array.IndexOf(Items, item);
        // ?? Should I not throw an exception and continue?
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        
        RemoveAt(index);
    }
    
    public void RemoveAt(int index)
    {
        // ?? Should I not throw an exception and continue?
        ErrorHandling<T>.ThrowIfOutOfRange(index, Count);

        // Yup, used again here it seems
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