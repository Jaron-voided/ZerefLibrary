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

    private T GetThis(int index)
    {
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException();
        return Items[index];
    }

    private void SetThat(int index, T value)
    {
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException();
        Items[index] = value;
    }
    
    public ZList()
    {
        Items = new T[8];
        Count = 0;
    }
    
    public void Add(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        
        if (Count < Items.Length)
        {
            Items[Count] = item;
        }
        else
        {
            // Maybe handle this differently instead of doubling??
            T[] newArray = new T[Count * 2];
            Array.Copy(Items, newArray, Count);
            newArray[Count] = item;
            Items = newArray;
        }

        Count++;
    }
    
    public void Insert(int index, T item)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));        

        // I need to shift everything right?
        if (index < Count)
        {
            // Shifts everything to the right one
            for (int i = Count; i > index; i--)
            {
                Items[i] = Items[i - 1];
            }
            
            Items[index] = item;
        }
        else
        {
            // Should it be allowed to put an item extra spots past the last item
            T[] newArray = new T[Count * 2];
            Array.Copy(Items, newArray, Count);
            newArray[index] = item;
            Items = newArray;
        }

        Count++;
    }
    
    public void Remove(T item)
    {
        int index = Array.IndexOf(Items, item);
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException();
        
        RemoveAt(index);
    }
    
    public void RemoveAt(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));        

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