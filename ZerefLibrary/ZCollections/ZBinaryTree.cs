using System.Collections;
using ZerefLibrary.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZBinaryTree<TKey, TValue> 
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
    
    public static ZBinaryTree<TKey, TValue> Create()
    {
        return new ZBinaryTree<TKey, TValue>();
    }
    
    public static ZBinaryTree<TKey, TValue> Create(TKey key, TValue value)
    {
        var tree = new ZBinaryTree<TKey, TValue>();
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

    // Entry point to remove
    public void Remove(TKey key)
    {
        Root = Remove(Root, key);
        Count--;
    }
    
    private ZBinaryTreeNode? Remove(ZBinaryTreeNode? node, TKey key)
    {
        // key not found
        if (node == null) return null;

        // compares current node key to target key
        int cmp = key.CompareTo(node.Pair.Key);

        if (cmp < 0)
        {
            // recursively moves left until it finds the cmp == 0
            node.Left = Remove(node.Left, key);
        }
        else if (cmp > 0)
        {
            // moves right until cmp == 0
            node.Right = Remove(node.Right, key);
        }
        else
        {
            // found the node to delete, cmp == 0
            // Case: 0 or 1 child
            // returns either child to be put in place of the deleted node
            if (node.Left == null) return node.Right;
            if (node.Right == null) return node.Left;

            // Case: 2 children → restructure
            return Rotate(node);
        }
        
        return node;
    }

    private ZBinaryTreeNode Rotate(ZBinaryTreeNode node)    {
        if (node.Left.IsFull())
            return RotateRight(node);
        
        if (node.Right.IsFull())
            return RotateLeft(node);
        
        // If they're both full I'm going to have to rotate lots more stuff I feel...
        return RotateLeft(node);
        
    }
    
    private ZBinaryTreeNode RotateLeft(ZBinaryTreeNode node)
    {
        // Find the successor — smallest node in the right subtree
        var successor = GetMinNode(node.Right);

        // Step 2: Copy successor's key/value into the current node (overwrite it)
        node.Pair = successor.Pair;

        // Step 3: Remove the original successor node from the right subtree
        node.Right = Remove(node.Right, successor.Pair.Key);

        // Step 4: Return the updated current node
        return node;
    }
    
    private ZBinaryTreeNode RotateRight(ZBinaryTreeNode node)
    {
        // Step 1: Find the in-order predecessor — the largest node in the left subtree
        var predecessor = GetMaxNode(node.Left);

        // Step 2: Copy predecessor's key/value into the current node (overwrite it)
        node.Pair = predecessor.Pair;

        // Step 3: Remove the original predecessor node from the left subtree
        node.Left = Remove(node.Left, predecessor.Pair.Key);

        // Step 4: Return the updated current node
        return node;
    }

    public void Clear()
    {
        Clear(Root);
        Root = null;
        Count = 0;
    }

    private void Clear(ZBinaryTreeNode node)
    {
        if (node == null) return;

        Clear(node.Left);
        Clear(node.Right);
        
        node.Left = null;
        node.Right = null;
        node.Pair = default;
    }

    private ZBinaryTreeNode GetMinNode(ZBinaryTreeNode node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }
    
    private ZBinaryTreeNode GetMaxNode(ZBinaryTreeNode node)
    {
        while (node.Right != null)
            node = node.Right;
        return node;
    }

    // Traversals
    public IEnumerable<KeyValuePair<TKey, TValue>> TraverseInOrder() => TraverseInOrder(Root);

    public IEnumerable<KeyValuePair<TKey, TValue>> TraverseInOrder(ZBinaryTreeNode current)
    {
        if (current == null) yield break;
        
        foreach (var pair in TraverseInOrder(current.Left))
            yield return pair;
        
        yield return current.Pair;
        
        foreach (var pair in TraverseInOrder(current.Right))
            yield return pair;
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> TraversePreOrder() => TraversePreOrder(Root);

    public IEnumerable<KeyValuePair<TKey, TValue>> TraversePreOrder(ZBinaryTreeNode current)
    {
        if (current == null) yield break;

        yield return current.Pair;
        
        foreach (var pair in TraversePreOrder(current.Left))
            yield return pair;
        
        foreach (var pair in TraversePreOrder(current.Right))
            yield return pair;
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> TraversePostOrder() => TraversePostOrder(Root);
    
    public IEnumerable<KeyValuePair<TKey, TValue>> TraversePostOrder(ZBinaryTreeNode current)
    {
        if (current == null) yield break;
        
        foreach (var pair in TraversePostOrder(current.Left))
            yield return pair;
        
        foreach (var pair in TraversePostOrder(current.Right))
            yield return pair;
    
        yield return current.Pair;
    }
    
    public class ZBinaryTreeNode
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

        internal static void DeleteNode(ZBinaryTreeNode? node)
        {
            // Don't think I need this, maybe
        }

        internal bool IsNotFull()
        {
            return (Left != null || Right != null);
        }
        
        internal bool IsFull()
        {
            return Left != null && Right != null;
        }
    }
}