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
            
            // No left child
            if (node.Left == null) return node.Right;
            
            // No right child
            if (node.Right == null) return node.Left;
            
            // 2 Children
            var successorParent = node;
            var successor = node.Right;
            
            // Find the inorder successor (leftmost node in right subtree)
            while (successor.Left != null)
            {
                successorParent = successor;
                successor = successor.Left;
            }
            
            // If the successor isn't the direct child of the node being deleted
            if (successorParent != node)
            {
                // Remove successor from its original spot
                successorParent.Left = successor.Right;
                
                // Link successors right to node's right
                successor.Right = node.Right;
            }
            
            // Link successors left to nodes left (always safe)
            successor.Left = node.Left;
            
            // Return successor as the new subtree root
            return successor;
        }
        
        return node;                                                     // Return updated node back up the recursion chain
    }

    private int GetDepth(ZBinaryTreeNode? node)
    {
        if (node == null) return 0;
        
        int leftDepth = GetDepth(node.Left);
        int rightDepth = GetDepth(node.Right);
        
        return 1 + Math.Max(leftDepth, rightDepth);
    }

    private ZBinaryTreeNode Rotate(ZBinaryTreeNode node)    
    {
        if (node.Left != null && node.Right != null)
        {
            // Choose based on depth
            int leftDepth = GetDepth(node.Left);
            int rightDepth = GetDepth(node.Right);

            if (leftDepth > rightDepth)
                return RotateRight(node);
            else
                return RotateLeft(node);
            
        }
        // if node.Right is null...
        else if (node.Left != null)
            return RotateRight(node);
        
        else if  (node.Right != null)
            return RotateLeft(node);

        // If the node has no children
        return null!;
    }
    
    private ZBinaryTreeNode RotateLeft(ZBinaryTreeNode node)
    {
        // Get the in-order successor and its parent
        ZBinaryTreeNode? successorParent;
        var successor = GetMinAndParentNode(node.Right!, out successorParent);

        // Rewire the parent's left pointer if successor is not the immediate right child
        if (successorParent != null)
        {
            successorParent.Left = successor.Right;
            successor.Right = node.Right;
        }

        // Always attach the left child
        successor.Left = node.Left;

        return successor;
    }
    
    private ZBinaryTreeNode RotateRight(ZBinaryTreeNode node)
    {
        // Get the in-order predecessor and its parent
        ZBinaryTreeNode? predecessorParent;
        var predecessor = GetMaxAndParentNode(node.Left!, out predecessorParent);

        // Rewire the parent's right pointer if predecessor is not the immediate left child
        if (predecessorParent != null)
        {
            predecessorParent.Right = predecessor.Left;
            predecessor.Left = node.Left;
        }

        // Always attach the right child
        predecessor.Right = node.Right;

        return predecessor;
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

    private ZBinaryTreeNode GetMinAndParentNode(ZBinaryTreeNode node, out ZBinaryTreeNode? parent)
    {
        parent = null;
        while (node.Left != null)
        {
            parent = node;
            node = node.Left;
        }
        return node;
    }
    
    private ZBinaryTreeNode GetMaxAndParentNode(ZBinaryTreeNode node, out ZBinaryTreeNode? parent)
    {
        parent = null;
        while (node.Right != null)
        {
            parent = node;
            node = node.Right;
        }
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