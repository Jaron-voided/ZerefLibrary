using System;
using ZerefLibrary.ZCollections;

internal class Program
{
    static void Main()
    {
        var tree = ZBinaryTree<int, string>.Create();

        // Adding nodes (key, value)
        tree.Add(10, "root");
        tree.Add(5, "left");
        tree.Add(15, "right");
        tree.Add(3, "left.left");
        tree.Add(7, "left.right");
        tree.Add(20, "right.right");

        Console.WriteLine("In-Order Traversal:");
        foreach (var pair in tree.TraverseInOrder())
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        Console.WriteLine("\nPre-Order Traversal:");
        foreach (var pair in tree.TraversePreOrder())
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        Console.WriteLine("\nPost-Order Traversal:");
        foreach (var pair in tree.TraversePostOrder())
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }
}