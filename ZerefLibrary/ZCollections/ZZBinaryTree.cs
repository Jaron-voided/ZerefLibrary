using System.Collections;
using ZerefLibrary.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZZBinaryTree<TKey, TValue> 
    : IZKeyValueStore<TKey, TValue>
    where TKey : IComparable<TKey>, IEquatable<TKey>
{
    
    public int Count { get; private set; }
    public ICollection<TKey>? Keys { get; }
    public ICollection<TValue>? Values { get; }
    
    //I forget how to handle this, is it private set?
    /*private ZBinaryTreeNode _root;
    public ZBinaryTreeNode Root => _root;*/
    internal ZBinaryTreeNode? Root { get; private set; }
    
    // This would return the items in order, is this the correct traversal for this method?
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return TraverseInOrder().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public TValue this[TKey key]
    {
        get => GetThis(key);
        set => SetThat(key, value);
    }
    
    private TValue GetThis(TKey key)
    {
        var current = Root;
        // Still won't let me use the == operand
        while (true)
        {
            if (current.Key.Equals(key))
            {
                return current.Value;
            }
            else if (key.CompareTo(current.Key) < 0)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }
    }
    
    // These are similar, can I compress them?
    // Need to add null checks... later
    private void SetThat(TKey key, TValue value)
    {
        var current = Root;
        // Still won't let me use the == operand
        while (true)
        {
            if (current.Key.Equals(key))
            {
                current.Value = value;
                return;
            }
            else if (key.CompareTo(current.Key) < 0)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }
    }
    
    public void Add(TKey key, TValue value)
    {
        var current = Root;
        // Still won't let me use the == operand
        while (true)
        {
            if (current.Left == null || current.Right == null)
            {
                var newNode = ZBinaryTreeNode.Create(key, value);
                if (key.CompareTo(current.Key) < 0)
                {
                    current.Left = newNode;
                    return;
                }
                else
                {
                    current.Right = newNode;
                    return;
                }
            }

            if (key.CompareTo(current.Key) < 0)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }
    }

    public bool ContainsKey(TKey key)
    {
        var current = Root;
        while (true)
        {
            // I thought the IEquatable and IComparable would allow me to do key == Key or key < Key??
            if (current.Key.Equals(key))
            {
                return true;
            }
            else if (key.CompareTo(current.Key) < 0)
            {
                current = current.Left;
            }
            else if (key.CompareTo(current.Key) > 0)
            {
                current = current.Right;
            }
            // Why is current.Left alright, but current.Right will always be false?
            else if (current.Left == null && current.Right == null)
            {
                return false;
            }
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var current = Root;
        while (true)
        {
            if (current.Key.Equals(key))
            {
                value = current.Value;
                return true;
            }
            else if (key.CompareTo(current.Key) < 0)
            {
                current = current.Left;
            }
            else if (key.CompareTo(current.Key) > 0)
            {
                current = current.Right;
            }
            else if (current.Left == null && current.Right == null)
            {
                value = default(TValue);
                return false;
            }
        }
    }

    public TValue[] CopyTo(int index)
    {
        throw new NotImplementedException();
    }

    public bool Remove(TKey key)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }
    
    // Non-Interface methods
    public void SetRoot(TKey key, TValue value)
    {
        var root = ZBinaryTreeNode.Create(key, value);
        Root = root;
    }
    
    // Traversals
    public IEnumerable<KeyValuePair<TKey, TValue>> TraverseInOrder()
    {
        return TraverseInOrder(Root);
    }

    private IEnumerable<KeyValuePair<TKey, TValue>> TraverseInOrder(ZBinaryTreeNode current)
    {
        if (current.Left != null)
        {
            foreach (var item in TraverseInOrder(current.Left))
                yield return item;
        }
            
        yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
        Console.WriteLine(current.Value);

        if (current.Right != null)
        {
            foreach (var item in TraverseInOrder(current.Right))
                yield return item;
        }
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> TraversePreOrder()
    {
        return TraversePreOrder(Root);
    }

    private IEnumerable<KeyValuePair<TKey, TValue>> TraversePreOrder(ZBinaryTreeNode current)
    {
        yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
        Console.WriteLine(current.Value);
        
        if (current.Left != null)
        {
            foreach (var item in TraversePreOrder(current.Left))
                yield return item;
        }
        
        if (current.Right != null)
        {
            foreach (var item in TraversePreOrder(current.Right))
                yield return item;
        }
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> TraversePostOrder()
    {
        return TraversePostOrder(Root);
    }
    
    private IEnumerable<KeyValuePair<TKey, TValue>> TraversePostOrder(ZBinaryTreeNode current)
    {
        if (current.Left != null)
        {
            foreach (var item in TraversePostOrder(current.Left))
                yield return item;
        }
        
        if (current.Right != null)
        {
            foreach (var item in TraversePostOrder(current.Right))
                yield return item;
        }
        
        yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
        Console.WriteLine(current.Value);
    }

    
    // Node
    internal class ZBinaryTreeNode
    {
        internal ZBinaryTreeNode? Left { get; set; }
        internal ZBinaryTreeNode? Right { get; set; }
        internal required TKey Key { get; set; }
        internal required TValue Value { get; set; }

        internal static ZBinaryTreeNode Create(TKey key, TValue value)
        {
            return new ZBinaryTreeNode
            {
                Left = null,
                Right = null,
                Key = key,
                Value = value
            };
        }
    }
}