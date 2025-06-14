using System.Collections;
using ZerefLibrary.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZZBinaryTree<TKey, TValue> 
    : IZKeyValueStore<TKey, TValue>
    where TKey : IComparable<TKey>, IEquatable<TKey>
{
    
    public int Count { get; private set; }
    public ICollection<TKey> Keys => TraverseInOrder().Select(p => p.Key).ToList();
    public ICollection<TValue> Values => TraverseInOrder().Select(p => p.Value).ToList();
    private ZBinaryTreeNode? Root { get; set; }
    
    // This would return the items in order, is this the correct traversal for this method?
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => TraverseInOrder().GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    public TValue this[TKey key]
    {
        get => GetThis(key);
        set => SetThat(key, value);
    }
    
    public static ZZBinaryTree<TKey, TValue> Create()
    {
        return new ZZBinaryTree<TKey, TValue>();
    }
    
    public static ZZBinaryTree<TKey, TValue> Create(TKey key, TValue value)
    {
        var tree = new ZZBinaryTree<TKey, TValue>();
        tree.Root = ZBinaryTreeNode.Create(key, value);
        tree.Count = 1;
        return tree;
    }
    
    private ZBinaryTreeNode TryGetNodeAt(TKey key, out bool exists)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        var current = Root;
        while (current != null)
        {
            // This will be zero if they are equal
            int compare = key.CompareTo(current.Pair.Key);
            
            if (compare == 0)
            {
                exists = true;
                return current;
            }
            current = compare < 0 ? current.Left : current.Right;
        }
        
        exists = false;
        return null;
    }
    
    private TValue GetThis(TKey key)
    {
        var node = TryGetNodeAt(key, out var exists);
        if (!exists) throw new ArgumentOutOfRangeException(nameof(key), "Key not found in the tree.");
        return node.Pair.Value;
    }

    private void SetThat(TKey key, TValue value)
    {
        var node = TryGetNodeAt(key, out var exists);
        if (!exists) throw new ArgumentOutOfRangeException(nameof(key), "Key not found in the tree.");
        node.Pair = new KeyValuePair<TKey, TValue>(key, value);
    }
    
    public void Add(TKey key, TValue value)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        if (Root == null)
        {
            Root = ZBinaryTreeNode.Create(key, value);
            Count++;
            return;
        }

        var current = Root;
        while (true)
        {
            int compare = key.CompareTo(current.Pair.Key);

            if (compare < 0)
            {
                if (current.Left == null)
                {
                    current.Left = ZBinaryTreeNode.Create(key, value);
                    Count++;
                    return;
                }
                current = current.Left;
            }
            else if (compare > 0)
            {
                if (current.Right == null)
                {
                    current.Right = ZBinaryTreeNode.Create(key, value);
                    Count++;
                    return;
                }
                current = current.Right;
            }
            else
            {
                throw new ArgumentException("An element with the same key already exists.", nameof(key));
            }
        }
    }

    public bool ContainsKey(TKey key)
    {
        var node = TryGetNodeAt(key, out var exists);
        return exists;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var node = TryGetNodeAt(key, out var exists);
        if (exists)
        {
            value = node.Pair.Value;
            return exists;
        }

        value = default;
        return false;
    }

    public TValue[] CopyTo()
    {
        TValue[] array = new TValue[Count];
        IEnumerable<KeyValuePair<TKey, TValue>> values = TraverseInOrder();

        var i = 0;
        foreach (var pair in values)
        {
            array[i++] = pair.Value;
        }

        return array;
    }

    // Why did I make this a bool?
    //public bool Remove(TKey key)
    public bool Remove(TKey key)
    {
        // Need to extract a method that traverses through the tree looking for the correct spot, as I'm using it alot
        var current = Root;
        ZBinaryTreeNode currentParent = Root;
        
        while (true)
        {
            if (current.Pair.Key.Equals(key))
            {
                if (key.CompareTo(current.Pair.Key) < 0)
                    currentParent.Right = null;
                else
                    currentParent.Left = null;
                current = null;
                return true;
            }
            
            if (key.CompareTo(current.Pair.Key) < 0)
            {
                currentParent = current;
                current = current.Left;
            }
            else if (key.CompareTo(current.Pair.Key) > 0)
            {
                currentParent = current;
                current = current.Right;
            }
            else if (current.Left == null && current.Right == null)
            {
                return false;
            }
        }

        Count--;
    }

    public void Clear()
    {
        // Would this work?
        Root = null;
        Count = 0;
        /*var current = Root;

        // Get to the lowest right node and store its parent
        while (Root.Right != null)
        {
            ZBinaryTreeNode currentParent = null;
            while (current.Right != null)
            {
                currentParent = current;
                current = current.Right;
            }

            currentParent.Left = null;
            currentParent.Right = null;

            while (current.Left != null)
            {
                currentParent = current;
                current = current.Left;
            }

            currentParent.Left = null;
            currentParent.Right = null;
        }

        Root = null;
        Count = 0;*/
    }

    // Non-Interface methods
    
        // Take this out completely? Or just make it private (not public)
        /*private void SetRoot(TKey key, TValue value)
        {
            var root = ZBinaryTreeNode.Create(key, value);
            Root = root;
        }*/
    
        // Traversals
        private IEnumerable<KeyValuePair<TKey, TValue>> TraverseInOrder() => TraverseInOrder(Root);

        private IEnumerable<KeyValuePair<TKey, TValue>> TraverseInOrder(ZBinaryTreeNode current)
        {
            if (current == null) yield break;

            if (current.Left != null)
            {
                foreach (var pair in TraverseInOrder(current.Left))
                    yield return pair;
            }
            
            yield return current.Pair;

            if (current.Right != null)
            {
                foreach (var pair in TraverseInOrder(current.Right))
                    yield return pair;
            }
        }

        private IEnumerable<KeyValuePair<TKey, TValue>> TraversePreOrder() => TraversePreOrder(Root);

        private IEnumerable<KeyValuePair<TKey, TValue>> TraversePreOrder(ZBinaryTreeNode current)
        {
            if (current == null) yield break;

            yield return current.Pair;
        
            if (current.Left != null)
            {
                foreach (var pair in TraversePreOrder(current.Left))
                    yield return pair;
            }
        
            if (current.Right != null)
            {
                foreach (var pair in TraversePreOrder(current.Right))
                    yield return pair;
            }
        }

        private IEnumerable<KeyValuePair<TKey, TValue>> TraversePostOrder() => TraversePostOrder(Root);
        
        private IEnumerable<KeyValuePair<TKey, TValue>> TraversePostOrder(ZBinaryTreeNode current)
        {
            if (current == null) yield break;

            if (current.Left != null)
            {
                foreach (var pair in TraversePostOrder(current.Left))
                    yield return pair;
            }
        
            if (current.Right != null)
            {
                foreach (var pair in TraversePostOrder(current.Right))
                    yield return pair;
            }
        
            yield return current.Pair;
        }
    
    // Node
    internal class ZBinaryTreeNode
    {
        internal ZBinaryTreeNode? Left { get; set; }
        internal ZBinaryTreeNode? Right { get; set; }
        internal KeyValuePair<TKey, TValue> Pair { get; set; }

        internal static ZBinaryTreeNode Create(TKey key, TValue value)
        {
            return new ZBinaryTreeNode
            {
                Left = null,
                Right = null,
                Pair = new KeyValuePair<TKey, TValue>(key, value)
            };
        }
    }
}