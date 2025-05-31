using ZerefLibrary.ZCollections.ZInterfaces;

namespace ZerefLibrary.ZCollections;

internal class ZBinaryTree<T> : IZBinaryTree<T>
{
    public IZBinaryTree<T>.IZBinaryTreeNode<T> Root { get; set; } = new  ZBinaryTreeNode<T>();
    public int Count { get; private set; }
    public void SetRoot(T item)
    {
        ZBinaryTreeNode<T> root = ZBinaryTreeNode<T>.Create(item);
        Root = root;
    }

    public void InsertLeft(IZBinaryTree<T>.IZBinaryTreeNode<T> parent, T item)
    {
        ZBinaryTreeNode<T> leftNode = ZBinaryTreeNode<T>.Create(item);
        parent.Left = leftNode;
    }

    public void InsertRight(IZBinaryTree<T>.IZBinaryTreeNode<T> parent, T item)
    {
        ZBinaryTreeNode<T> rightNode = ZBinaryTreeNode<T>.Create(item);
        parent.Right = rightNode;
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
    }

    public void RemoveRight(IZBinaryTree<T>.IZBinaryTreeNode<T> parent)
    {
        // Do I need to reorder the whole tree and move something into its spot?
        parent.Right = null;
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void TraverseInOrder(Action<T> action)
    {
        throw new NotImplementedException();
    }

    public void TraversePreOrder(Action<T> action)
    {
        throw new NotImplementedException();
    }

    public void TraversePostOrder(Action<T> action)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        IZBinaryTree<T>.IZBinaryTreeNode<T> current = Root;

        // Get to the lowest right node and store its parent
        while (Root.Right != null)
        {
            while (current.Right != null)
            {
                IZBinaryTree<T>.IZBinaryTreeNode<T> currentParent = current;
                current = current.Right;
                current = null;
            }
            
            
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