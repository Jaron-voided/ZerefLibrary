using ZerefLibrary.ZCollections;

namespace ZerefLibrary.utils;

internal static class ErrorHandling<T>
{
    internal static void IsIndexInRange(int index)
    {
        if (index < 0)
            throw new IndexOutOfRangeException();
    }
    internal static void IsIndexInRange(int index, int count)
    {
        if (index < 0 || index > count)
            throw new IndexOutOfRangeException();
    }
    
    internal static void IsItemNull(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
    }

    internal static void IsNodeNull(ZLinkedList<T>.ZLinkedListNode<T> node, string warning = "Node is null")
    {
        if (node == null)
            throw new InvalidOperationException(warning);
    }
}