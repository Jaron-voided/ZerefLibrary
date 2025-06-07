using System.Collections;
using ZerefLibrary.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZBidsfanaryTree<TKey, TValue> : IZBinaryTree<TKey, TValue>
{
    public TValue this[TKey key]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
    Add

    public IZBinaryTree<TKey, TValue>.IZBinaryTreeNode Root { get; set; } = new  ZBinaryTreeNode();
    public int Count { get; private set; }
    public ICollection<TKey> Keys { get; }
    public ICollection<TValue> Values { get; }
    public void Add(TKey key, TValue value)
    {
        throw new NotImplementedException();
    }

    public TKey AddWithoutKey(TValue value, out TKey key)
    {
        throw new NotImplementedException();
    }

    public bool ContainsKey(TKey key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        throw new NotImplementedException();
    }

    public TKey GetFirstKey(TValue value)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TKey> GetAllKeys(TValue value)
    {
        throw new NotImplementedException();
    }

    public TKey SetRootWithoutKey(TValue item, out TKey key)
    {
        throw new NotImplementedException();
    }

    public void SetRoot(TKey key, TValue item)
    {
        throw new NotImplementedException();
    }

    public TKey InsertWithoutKey(TValue value, out TKey key)
    {
        throw new NotImplementedException();
    }

    public void Insert(TKey key, TValue value)
    {
        throw new NotImplementedException();
    }

    public bool RemoveFirst(TValue item)
    {
        throw new NotImplementedException();
    }

    public bool RemoveAt(TKey key)
    {
        throw new NotImplementedException();
    }

    public void RemoveAll(TValue item)
    {
        throw new NotImplementedException();
    }

    public void SetRoot(T item)
    {
        var root = ZBinaryTreeNode.Create(item);
        Root = root;
    }

    public void InsertLeft(IZBinaryTree<TKey, TValue>.IZBinaryTreeNode parent, T item)
    {
        var leftNode = ZBinaryTreeNode.Create(item);
        parent.Left = leftNode;
        Count++;
    }

    public void InsertRight(IZBinaryTree<TKey, TValue>.IZBinaryTreeNode parent, T item)
    {
        var rightNode = ZBinaryTreeNode.Create(item);
        parent.Right = rightNode;
        Count++;
    }

    public void Remove(T item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAll(T item)
    {
        throw new NotImplementedException();
    }

    public void RemoveLeft(IZBinaryTree<TKey, TValue>.IZBinaryTreeNode parent)
    {
        // Do I need to reorder the whole tree and move something into its spot?
        parent.Left = null;
        Count--;
    }
    // Do these need combined?
    public void RemoveRight(IZBinaryTree<TKey, TValue>.IZBinaryTreeNode parent)
    {
        // Do I need to reorder the whole tree and move something into its spot?
        parent.Right = null;
        Count--;
    }

    public bool Contains(TValue item)
    {
        throw new NotImplementedException();
    }

    IEnumerable<KeyValuePair<TKey, TValue>> IZBinaryTree<TKey, TValue>.TraverseInOrder()
    {
        throw new NotImplementedException();
    }

    IEnumerable<KeyValuePair<TKey, TValue>> IZBinaryTree<TKey, TValue>.TraversePreOrder()
    {
        throw new NotImplementedException();
    }

    IEnumerable<KeyValuePair<TKey, TValue>> IZBinaryTree<TKey, TValue>.TraversePostOrder()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<T> TraverseInOrder()
    {
        return TraverseInOrder(Root);
    }

    public IEnumerable<T> TraverseInOrder(IZBinaryTree<TKey, TValue>.IZBinaryTreeNode current)
    {

            if (current.Left != null)
            {
                foreach (var item in TraverseInOrder(current.Left))
                    yield return item;
            }
            
            yield return current.Value;
            Console.WriteLine(current.Value);

            if (current.Right != null)
            {
                foreach (var item in TraverseInOrder(current.Right))
                    yield return item;
            }
    }

    public IEnumerable<T> TraversePreOrder()
    {
        return TraversePreOrder(Root);
    }

    public IEnumerable<T> TraversePreOrder(IZBinaryTree<TKey, TValue>.IZBinaryTreeNode current)
    {
        yield return current.Value;
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

    public IEnumerable<T> TraversePostOrder()
    {
        return TraversePostOrder(Root);
    }
    
    public IEnumerable<T> TraversePostOrder(IZBinaryTree<TKey, TValue>.IZBinaryTreeNode current)
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
        
        yield return current.Value;
        Console.WriteLine(current.Value);
    }


    public void Clear()
    {
        IZBinaryTree<TKey, TValue>.IZBinaryTreeNode current = Root;

        // Get to the lowest right node and store its parent
        while (Root.Right != null)
        {
            IZBinaryTree<TKey, TValue>.IZBinaryTreeNode currentParent = null;
            while (current.Right != null)
            {
                currentParent = current;
                current = current.Right;
            }
            currentParent.Left = null;
            Count--;
            currentParent.Right = null;
            Count--;
            
            while (current.Left != null)
            {
                currentParent = current;
                current = current.Left;
            }
            currentParent.Left = null;
            Count--;
            currentParent.Right = null;
            Count--;
        }
    }

    private class ZBinaryTreeNode : IZBinaryTree<TKey, TValue>.IZBinaryTreeNode
    {
        public IZBinaryTree<TKey, TValue>.IZBinaryTreeNode? Left { get; set; }
        public IZBinaryTree<TKey, TValue>.IZBinaryTreeNode? Right { get; set; }
        public T Value { get; set; }

        internal static ZBinaryTreeNode Create(T value)
        {
            return new ZBinaryTreeNode() { Value = value, Left = null, Right = null };
        }
        
        // If only one node is passed it would have to be the left node?
        internal static ZBinaryTreeNode Create(ZBinaryTreeNode? left, T value)
        {
            return new ZBinaryTreeNode() { Value = value, Left = left, Right = null };
        } 
        
        internal static ZBinaryTreeNode Create(ZBinaryTreeNode? left, ZBinaryTreeNode? right, T value)
        {
            return new ZBinaryTreeNode() { Value = value, Left = left, Right = right };
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}