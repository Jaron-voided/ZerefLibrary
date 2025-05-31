using ZerefLibrary.ZCollections;

namespace ZerefLibrary.utils;

internal static class ErrorHandling<T>
{
    internal static void ThrowIfOutOfRange(int index, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, count);
    }
    
    internal static void ThrowIfItemIsNull(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
    }


}