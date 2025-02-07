using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.BinaryTree
{
 

    public class TreeNode<T>
    {
        public T Value { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public TreeNode(T value)
        {
            this.Value = value;
            this.Left = null;
            this.Right = null;
        }
    }

    public class BinaryTree<T>
    {
        public TreeNode<T> Root { get; set; }

        public BinaryTree()
        {
            this.Root = null;
        }

        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new TreeNode<T>(value);
            }
            else
            {
                InsertRecursively(Root, value);
            }
        }

        private void InsertRecursively(TreeNode<T> node, T value)
        {
            if (Comparer<T>.Default.Compare(value, node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new TreeNode<T>(value);
                }
                else
                {
                    InsertRecursively(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new TreeNode<T>(value);
                }
                else
                {
                    InsertRecursively(node.Right, value);
                }
            }
        }

        public void InOrderTraversal(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, action);
                action(node.Value);
                InOrderTraversal(node.Right, action);
            }
        }
        public void PreOrderTraversal(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Value);
                InOrderTraversal(node.Left, action);
                InOrderTraversal(node.Right, action);
            }
        }
        public void PostOrderTraversal(TreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, action);
                InOrderTraversal(node.Right, action);
                action(node.Value);
            }
        }

        // Method to convert a sorted array to a binary search tree
        public void ArrayToBST(T[] array)
        {
            Root = ArrayToBSTRecursively(array, 0, array.Length - 1);
        }

        private TreeNode<T> ArrayToBSTRecursively(T[] array, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            int mid = (start + end) / 2;
            TreeNode<T> node = new TreeNode<T>(array[mid]);

            node.Left = ArrayToBSTRecursively(array, start, mid - 1);
            node.Right = ArrayToBSTRecursively(array, mid + 1, end);

            return node;
        }
        public void PrintTree()
        {
            PrintTree(Root, "", true);
        }

        private void PrintTree(TreeNode<T> node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("R----");
                    indent += "     ";
                }
                else
                {
                    Console.Write("L----");
                    indent += "|    ";
                }
                Console.WriteLine(node.Value);
                PrintTree(node.Left, indent, false);
                PrintTree(node.Right, indent, true);
            }
        }

        public void PrintTreeVertically()
        {
            if (Root == null)
                return;

            Dictionary<int, List<string>> levels = new Dictionary<int, List<string>>();
            TraverseTree(Root, 0, levels);

            foreach (var level in levels)
            {
                Console.WriteLine(string.Join(" ", level.Value));
            }
        }

        private void TraverseTree(TreeNode<T> node, int level, Dictionary<int, List<string>> levels)
        {
            if (node == null)
            {
                if (!levels.ContainsKey(level))
                    levels[level] = new List<string>();
                levels[level].Add(" ");
                return;
            }

            if (!levels.ContainsKey(level))
                levels[level] = new List<string>();

            TraverseTree(node.Left, level + 1, levels);
            levels[level].Add(node.Value.ToString());
            TraverseTree(node.Right, level + 1, levels);
        }

        public void LevelTraversal(TreeNode<T> root, Action<T> action)
        {
           Queue<TreeNode<T>> levelQueue = new();
           levelQueue.Enqueue(root);
        
           while (levelQueue.Count > 0)
            {
             var currentLevel = levelQueue.ToList();
             levelQueue.Clear();
             foreach (var node in currentLevel)
                {
                    action(node.Value);//print the node value 
                    if (node.Left != null)
                    {
                        levelQueue.Enqueue(node.Left);
                    }
                    if (node.Right != null)
                    {
                        levelQueue.Enqueue(node.Right);
                    }
                }
            }

        }
    }


}
