using ZerefLibrary.ZCollections;

namespace ZerefLibrary.utils;

internal static class ErrorHandling<T>
{
    internal static void ThrowIfOutOfRange(int index)
    {
        if (index < 0)
            throw new IndexOutOfRangeException();
    }
    internal static void ThrowIfOutOfRange(int index, int count)
    {
        if (index < 0 || index > count)
            throw new IndexOutOfRangeException();
    }
    
    internal static void ThrowIfItemIsNull(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
    }


}