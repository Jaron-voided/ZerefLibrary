using System.Collections;
using ZerefLibrary.utils;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

internal class ZLinkedList<T> : IZList<T>
{
    private ZLinkedListNode<T>? Head { get; set; }
    private ZLinkedListNode<T>? Tail { get; set; }
    public int Count { get; private set; }
    
    public IEnumerator<T> GetEnumerator()
    {
        ErrorHandling<T>.IsNodeNull(Head);
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

    internal static ZLinkedList<T> MakeZLinkedList(ZLinkedListNode<T>[] zNodeArray)
    {
        ZLinkedList<T> zList = new ZLinkedList<T>();
        
        if (zNodeArray.Length > 0)
        {
            zList.Head = zNodeArray[0];
            zList.Tail = zNodeArray[^1];
            zList.Count = zNodeArray.Length;
        }
        else
        {
            zList.Head = null;
            zList.Tail = null;
            zList.Count = 0;
        }

        return zList;
    }
    
    public T this[int index]
    {
        get => GetThis(index);
        set => SetThat(index, value);
    }

    private T GetThis(int index)
    {
        var current = GetNodeAt(index);

        return current.Value;
    }

    private void SetThat(int index, T value)
    {
        var current = GetNodeAt(index);
        current.Value = value;
    }
    
    public void Add(T item)
    {
        ErrorHandling<T>.IsItemNull(item);

        ZLinkedListNode<T> node = ZLinkedListNode<T>.MakeTailNode(Tail, item);
        
        if (Count == 0)
        {
            Head = Tail = node;
        }
        else
        {
            Tail.Next = node;
            node.Previous = Tail;
            Tail = node;
        }

        Count++;
    }

    public void Insert(int index, T item)
    {
        ErrorHandling<T>.IsNodeNull(Head);
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Count);
        
        // If there are no items, this is Tail and Head
        if (index == 0)
        {
            var node = ZLinkedListNode<T>.MakeHeadNode(item, Head);
            Head.Previous = node;
            Head = node;
            if (Count == 0) Tail = node;
        }
        else if (index == Count)
        {
            Add(item);
        }
        else
        {
            var current = GetNodeAt(index);

            var node = ZLinkedListNode<T>.MakeZNode(current.Previous, current, item);
            current.Previous.Next = node;
            current.Previous = node;
        }
        
        Count++;
    }

    // Once other remove methods find the correct node, this function removes it
    private void Remove(ZLinkedListNode<T> node)
    {
        if (node.Previous != null)
            node.Previous.Next = node.Next;
        else
            Head = node.Next;

        if (node.Next != null)
            node.Next.Previous = node.Previous;
        else
            Tail = node.Previous;

        Count--;
    }

    // Should this be able to remove multiple instances of the same item, or only the first?
    public void Remove(T item)
    {
        ErrorHandling<T>.IsNodeNull(Head);
        ErrorHandling<T>.IsItemNull(item);
        
        ZLinkedListNode<T> current = Head;

        var i = 0;
        while (i < Count)
        {
            if (current.Value.Equals(item))
            {
                Remove(current);
                break;
            }
            current = current.Next;
            i++;
        }
    }

    public void RemoveAt(int index)
    {
        var current = GetNodeAt(index);
        
        Remove(current);
    }
    
    public void Clear()
    {
        ZLinkedListNode<T> node = Head;
        
        var i = 0;
        while (i < Count)
        {
            var next = node.Next;
            node.Previous = null;
            node.Next = null;
            node = next;
            i++;
        }
        Head = null;
        Tail = null;
        Count = 0;
    }
    
    private ZLinkedListNode<T> GetNodeAt(int index)
    {
        ErrorHandling<T>.IsNodeNull(Head, "Head is null");
        ErrorHandling<T>.IsIndexInRange(index, Count);

        var current = Head;
        int i = 0;

        while (i < index)
        {
            current = current.Next;
            i++;
        }

        return current;
    }

    internal class ZLinkedListNode<T>
    {
        internal ZLinkedListNode<T>? Previous;
        internal ZLinkedListNode<T>? Next;
        internal required T Value;
        
        internal static ZLinkedListNode<T> MakeZNode(ZLinkedListNode<T>? previous, ZLinkedListNode<T>? next, T value)
        {
            return new ZLinkedListNode<T>
            {
                Previous = previous,
                Next = next,
                Value = value
            };
        }

        internal static ZLinkedListNode<T> MakeHeadNode(T value, ZLinkedListNode<T> next)
            => MakeZNode(null, next, value);
        internal static ZLinkedListNode<T> MakeTailNode(ZLinkedListNode<T> previous, T value)
            => MakeZNode(previous, null, value);
    }
}