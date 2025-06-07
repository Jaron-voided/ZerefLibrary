using System.Collections;
using ZerefLibrary.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZBinaryTree<TKey, TValue> : IZBinaryTree<TKey, TValue> where TKey : IComparable<TKey>
{
    public IZBinaryTree<TKey, TValue>.IZBinaryTreeNode Root { get; set; }
    public int Count { get; }

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

    public TValue GetThis(TKey key)
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
    public void SetThat(TKey key, TValue value)
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
    
    // I'm unsure if I need to reorder the whole tree or just add to a leaf, doing it the easy way for now
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
                }
                else
                {
                    current.Right = newNode;
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

    // I'm unsure how to handle making a new key, or if I even should
    public TKey AddWithoutKey(TValue value, out TKey key)
    {
        throw new NotImplementedException();
    }

    public bool ContainsKey(TKey key)
    {
        var current = Root;
        while (true)
        {
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
            // Why is current.Left alright, but current.Right will always be false?
            else if (current.Left == null && current.Right == null)
            {
                return false;
            }
        }
    }

    public TKey GetFirstKey(TValue value)
    {
        // This will have to be done by traversing, since I can't decide with comparisons < >
        /*var current = Root;
        while (true)
        {
            if (current.Value.Equals(key))
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
            // Why is current.Left alright, but current.Right will always be false?
            else if (current.Left == null && current.Right == null)
            {
                return false;
            }
        }*/
    }

    public IEnumerable<TKey> GetAllKeys(TValue value)
    {
        throw new NotImplementedException();
    }

    public TKey SetRootWithoutKey(TValue item, out TKey key)
    {
        throw new NotImplementedException();
    }

    public void SetRoot(TKey key, TValue value)
    {
        var root = ZBinaryTreeNode.Create(key, value);
        Root = root;
    }

    public TKey InsertWithoutKey(TValue value, out TKey key)
    {
        throw new NotImplementedException();
    }

    // Do I need this and Add?
    public void Insert(TKey key, TValue value)
    { 
        var newNode = ZBinaryTreeNode.Create(key, value);
        var current = Root;
        while (true)
        {
            if (key.CompareTo(current.Key) < 0)
            { 
                current = current.Left;
            }
            else if (key.CompareTo(current.Key) > 0)
            { 
                current = current.Right;
            }
            // Why is current.Left alright, but current.Right will always be false?
        }
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

    public void RemoveLeft(IZBinaryTree<TKey, TValue>.IZBinaryTreeNode parent)
    {
        throw new NotImplementedException();
    }

    public void RemoveRight(IZBinaryTree<TKey, TValue>.IZBinaryTreeNode parent)
    {
        throw new NotImplementedException();
    }

    public bool Contains(TValue item)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> TraverseInOrder()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> TraversePreOrder()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> TraversePostOrder()
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }
    public class ZBinaryTreeNode : IZBinaryTree<TKey, TValue>.IZBinaryTreeNode
    {
        public IZBinaryTree<TKey, TValue>.IZBinaryTreeNode Left { get; set; }

        public IZBinaryTree<TKey, TValue>.IZBinaryTreeNode Right { get; set; }

        // Value last since it might be different sizes
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        internal static ZBinaryTree<TKey, TValue>.ZBinaryTreeNode Create(TKey key, TValue value)
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