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
        foreach (var item in Items)
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
        ArgumentNullException.ThrowIfNull(item);
        
        Insert(Count, item);
    }
    
    public void Insert(int index, T item)
    {
        ArgumentNullException.ThrowIfNull(item);
        
        //Unsure if I need this currently.
        ErrorHandling<T>.ThrowIfOutOfRange(index, Count);
        
        EnsureCapacity();

        // This is probably used elsewhere, try to extract it
        ShiftToInsert(index);
        
        Items[index] = item;
        Count++;
    }

    private void ShiftToInsert(int index)
    {
        for (var i = Count; i > index; i--)
        {
            Items[i] = Items[i - 1];
        }
    }

    // Figure out how to make this protected
    internal void EnsureCapacity()
    {
        if (Count >= Items.Length)
        {
            var newArray = new T[Count * GrowthFactor];
            Array.Copy(Items, newArray, Count);
            Items = newArray;
        }
    }

    public void Remove(T item)
    {
        var index = Array.IndexOf(Items, item);
        // ?? Should I not throw an exception and continue?
        //ArgumentOutOfRangeException.ThrowIfNegative(index);
      
        // if index does not exist (returns a negative) just continue
        if (index < 0) return;
        RemoveAt(index);
    }
    
    public void RemoveAt(int index)
    {
        // ?? Should I not throw an exception and continue?
        ErrorHandling<T>.ThrowIfOutOfRange(index, Count);

        // Yup, ALMOST used again here it seems
        ShiftToRemove(index);
        
        Count--;
    }

    private void ShiftToRemove(int index)
    {
        for (var i = index; i < Count - 1; i++)
        {
            Items[i] = Items[i + 1];
        }
    }

    public void RemoveAll(T item)
    {
        List<int> indexes = FindAllIndexes(item);
        for (int i = Count - 1; i >= 0; i--)
        {
            if (Items[i].Equals(item))
            {
                RemoveAt(i);
            }
        }
    }

    public List<int> FindAllIndexes<T>(T item)
    {
        List<int> indexes = new();
        
        for (int i = 0; i < Count; i++)
        {
            if (Items[i].Equals(item))
            {
                indexes.Add(i);
            }
        }

        return indexes;
    }
    public void Clear()
    {
        Array.Clear(Items, 0, Count);
        Count = 0;
    }
}