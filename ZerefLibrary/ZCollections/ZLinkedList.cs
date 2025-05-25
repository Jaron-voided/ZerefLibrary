using System.Collections;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

internal class ZLinkedList<T> : IZList<T>
{
    private ZLinkedListNode<T> Head { get; set; } = new ZLinkedListNode<T>();
    private ZLinkedListNode<T> Tail { get; set; } = new ZLinkedListNode<T>();
    public int Count { get; private set; }
    
    public IEnumerator<T> GetEnumerator()
    {
        ZLinkedListNode<T> current = Head;

        int i = 0;
        while (i < Count)
        {
            yield return current._value;
            current = current._next;
            i++;
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
        ZLinkedListNode<T> current = Head;
        int count = 0;
        while (count < index)
        {
            current = current._next;
            count++;
        }
        return current._value;
    }

    private void SetThat(int index, T value)
    {
        ZLinkedListNode<T> current = Head;
        int count = 0;
        while (count < index)
        {
            current = current._next;
            count++;
        }
        current._value = value;
    }
    
    public void Add(T item)
    {
        ZLinkedListNode<T> node = new ZLinkedListNode<T>(Tail, item, new ZLinkedListNode<T>());
        Tail._next = node;
        Tail = node;
        if (Count == 0) Head = node;
        Count++;
    }

    public void Insert(int index, T item)
    {
        ZLinkedListNode<T> node = new ZLinkedListNode<T>(Tail, item, new ZLinkedListNode<T>());
        ZLinkedListNode<T> current = Head;
        
        // If there are no items, this is Tail and Head
        if (index == 0)
        {
            Head = node;
        }

        if (index == Count)
        {
            Tail = node;
        }
        
        for (int i = 0; i < index; i++)
        {
            current =  current._next;
        }

        current._next._next._previous = node;
        current._next = node;
    }

    public void Remove(T item)
    {
        ZLinkedListNode<T> current = Head;
        for (int i = 0; i < Count; i++)
        {
            if (current._value != null && current._value.Equals(item))
            {
                current._previous._next = current._next;
                current._next._previous = current._previous;
                
                // Not sure why this is throwing a warning
                current = null;
                break;
            }
        }

        Count--;
    }

    public void Remove(int index)
    {
        ZLinkedListNode<T> current = Head;
        for (int i = 0; i < Count; i++)
        {
            current = current._next;
        }
        current._previous._next = current._next;
        current._next._previous = current._previous;
        current = null;
        Count--;
    }

    public void Clear()
    {
        ZLinkedListNode<T> current = Head._next;
        ZLinkedListNode<T> temp = current._next;
        Head = null;
        for (int i = 0; i < Count; i++)
        {
            temp = temp._next;
            current = null;
            current._previous = null;
            current._next = null;
        }

        Count = 0;
    }

    internal class ZLinkedListNode<T>()
    {
        internal ZLinkedListNode<T> _previous;
        internal ZLinkedListNode<T> _next;
        internal T _value;
        
        internal ZLinkedListNode(ZLinkedListNode<T> previous, T value, ZLinkedListNode<T> next) : this()
        {
            _previous = previous;
            _next = next;
            _value = value;
        }
    }
}