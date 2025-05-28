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
        if (Head == null)
            throw new InvalidOperationException("Head is null.");
        
        ZLinkedListNode<T> current = Head;

        int i = 0;
        
        while (i < Count)
        {
            yield return current.Value;
            current = current.Next;
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
        
        if (Head == null)
            throw new InvalidOperationException("Head is null.");
        
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));
        
        ZLinkedListNode<T> current = Head;
        int i = 0;
        while (i < index)
        {
            current = current.Next;
            i++;
        }
        return current.Value;
    }

    private void SetThat(int index, T value)
    {
        if (Head == null)
            throw new InvalidOperationException("Head is null.");
        
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        ZLinkedListNode<T> current = Head;
        int count = 0;
        while (count < index)
        {
            current = current.Next;
            count++;
        }
        current.Value = value;
    }
    
    public void Add(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        ZLinkedListNode<T> node = new ZLinkedListNode<T>(Tail, item, new ZLinkedListNode<T>());
        Tail.Next = node;
        Tail = node;
        if (Count == 0) Head = node;
        Count++;
    }

    public void Insert(int index, T item)
    {
        if (Head == null)
            throw new InvalidOperationException("Head is null.");
        
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));        
        
        if (Count <= 0)
            throw new ArgumentOutOfRangeException(nameof(Count));

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
            current =  current.Next;
        }

        current.Next.Next.Previous = node;
        current.Next = node;
    }

    public void Remove(T item)
    {
        if (Head == null)
            throw new InvalidOperationException("Head is null.");
     
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        
        ZLinkedListNode<T> current = Head;
        for (int i = 0; i < Count; i++)
        {
            if (current.Value != null && current.Value.Equals(item))
            {
                current.Previous.Next = current.Next;
                current.Next.Previous = current.Previous;
                
                // Not sure why this is throwing a warning
                current = null;
                break;
            }
        }

        Count--;
    }

    public void RemoveAt(int index)
    {
        if (Head == null)
            throw new InvalidOperationException("Head is null.");
        
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));        
        
        ZLinkedListNode<T> current = Head;
        for (int i = 0; i < Count; i++)
        {
            current = current.Next;
        }
        current.Previous.Next = current.Next;
        current.Next.Previous = current.Previous;
        current = null;
        Count--;
    }

    public void Clear()
    {
        ZLinkedListNode<T> node = Head;
        while (node != null)
        {
            var next = node.Next;
            node.Previous = null;
            node.Next = null;
            node = next;
        }
        Head = null;
        Tail = null;
        Count = 0;
    }

    internal class ZLinkedListNode<T>()
    {
        internal ZLinkedListNode<T> Previous;
        internal ZLinkedListNode<T> Next;
        internal T Value;
        
        internal ZLinkedListNode(ZLinkedListNode<T> previous, T value, ZLinkedListNode<T> next) : this()
        {
            Previous = previous;
            Next = next;
            Value = value;
        }
    }
}