using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

public class ZBinaryTree<T> : IZBinaryTree<T>
{
    public IZBinaryTree<T>.IZBinaryTreeNode<T> Root { get; set; } = new  ZBinaryTreeNode<T>();
    public int Count { get; private set; }
    public void SetRoot(T item)
    {
        var root = ZBinaryTreeNode<T>.Create(item);
        Root = root;
    }

    public void InsertLeft(IZBinaryTree<T>.IZBinaryTreeNode<T> parent, T item)
    {
        var leftNode = ZBinaryTreeNode<T>.Create(item);
        parent.Left = leftNode;
        Count++;
    }

    public void InsertRight(IZBinaryTree<T>.IZBinaryTreeNode<T> parent, T item)
    {
        var rightNode = ZBinaryTreeNode<T>.Create(item);
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

    public void RemoveLeft(IZBinaryTree<T>.IZBinaryTreeNode<T> parent)
    {
        // Do I need to reorder the whole tree and move something into its spot?
        parent.Left = null;
        Count--;
    }
    // Do these need combined?
    public void RemoveRight(IZBinaryTree<T>.IZBinaryTreeNode<T> parent)
    {
        // Do I need to reorder the whole tree and move something into its spot?
        parent.Right = null;
        Count--;
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<T> TraverseInOrder()
    {
        return TraverseInOrder(Root);
    }

    public IEnumerable<T> TraverseInOrder(IZBinaryTree<T>.IZBinaryTreeNode<T> current)
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

    public IEnumerable<T> TraversePreOrder(IZBinaryTree<T>.IZBinaryTreeNode<T> current)
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
    
    public IEnumerable<T> TraversePostOrder(IZBinaryTree<T>.IZBinaryTreeNode<T> current)
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
        IZBinaryTree<T>.IZBinaryTreeNode<T> current = Root;

        // Get to the lowest right node and store its parent
        while (Root.Right != null)
        {
            IZBinaryTree<T>.IZBinaryTreeNode<T> currentParent = null;
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

    private class ZBinaryTreeNode<T> : IZBinaryTree<T>.IZBinaryTreeNode<T>
    {
        public IZBinaryTree<T>.IZBinaryTreeNode<T>? Left { get; set; }
        public IZBinaryTree<T>.IZBinaryTreeNode<T>? Right { get; set; }
        public T Value { get; set; }

        internal static ZBinaryTreeNode<T> Create(T value)
        {
            return new ZBinaryTreeNode<T>() { Value = value, Left = null, Right = null };
        }
        
        // If only one node is passed it would have to be the left node?
        internal static ZBinaryTreeNode<T> Create(ZBinaryTreeNode<T>? left, T value)
        {
            return new ZBinaryTreeNode<T>() { Value = value, Left = left, Right = null };
        } 
        
        internal static ZBinaryTreeNode<T> Create(ZBinaryTreeNode<T>? left, ZBinaryTreeNode<T>? right, T value)
        {
            return new ZBinaryTreeNode<T>() { Value = value, Left = left, Right = right };
        }
    }
}