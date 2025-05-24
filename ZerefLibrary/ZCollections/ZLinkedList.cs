using System.Collections;
using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZLinkedList<T> : IZList<T>
{
    public ZLinkedListNode<T> Head { get; set; }
    public ZLinkedListNode<T> Tail { get; set; }

    private int _count;
    public int Count => _count;
    
    public IEnumerator<T> GetEnumerator()
    {
        ZLinkedListNode<T> current = Head;
        for (int i = 0; i < Count; i++)
        {
            yield return current._value;
            current = current._next;
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public T this[int index]
    {
        get
        {
            ZLinkedListNode<T> current = Head;
            for (int i = 0; i < index; i++)
            {
                current = current._next;
            }
            return current._value;
        }
        set
        {
            ZLinkedListNode<T> current = Head;
            for (int i = 0; i < index; i++)
            {
                current = current._next;
            }
            current._value = value;
        }
    }
    public void Add(T item)
    {
        ZLinkedListNode<T> node = new ZLinkedListNode<T>(Tail, item, new ZLinkedListNode<T>());
        Tail._next = node;
    }

    public void Insert(int index, T item)
    {
        ZLinkedListNode<T> node = new ZLinkedListNode<T>(Tail, item, new ZLinkedListNode<T>());
        ZLinkedListNode<T> current = Head;
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
            if (current._value.Equals(item))
            {
                current._previous._next = current._next;
                current._next._previous = current._previous;
                current = null;
            }
        }
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
        }
    }

    public class ZLinkedListNode<T>()
    {
        internal ZLinkedListNode<T> _previous;
        internal T _value;
        internal ZLinkedListNode<T> _next;

        public ZLinkedListNode(ZLinkedListNode<T> previous, T value,  ZLinkedListNode<T> next) : this()
        {
            _previous = previous;
            _value = value;
            _next = next;
        }
    }
}