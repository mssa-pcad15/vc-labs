using DSA.BinaryTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSATest.BST
{
    [TestClass]
    public class BinaryTreeTests
    {
        private BinaryTree<int> binaryTree;

        [TestInitialize]
        public void SetUp()
        {
            binaryTree = new BinaryTree<int>();
        }

        [TestMethod]
        public void TestArrayToBST_EmptyArray()
        {
            int[] emptyArray = new int[0];
            binaryTree.ArrayToBST(emptyArray);
            Assert.IsNull(binaryTree.Root);
        }

        [TestMethod]
        public void TestArrayToBST_SingleElement()
        {
            int[] singleElementArray = new int[] { 5 };
            binaryTree.ArrayToBST(singleElementArray);
            Assert.IsNotNull(binaryTree.Root);
            Assert.AreEqual(5, binaryTree.Root.Value);
            Assert.IsNull(binaryTree.Root.Left);
            Assert.IsNull(binaryTree.Root.Right);
        }

        [TestMethod]
        public void TestArrayToBST_MultipleElements()
        {
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7 };
        
            binaryTree.ArrayToBST(array);
            binaryTree.PrintTree();
            binaryTree.PrintTreeVertically();
            // Test the root
            Assert.IsNotNull(binaryTree.Root);
            Assert.AreEqual(4, binaryTree.Root.Value);

            // Test left subtree
            Assert.AreEqual(2, binaryTree.Root.Left.Value);
            Assert.AreEqual(1, binaryTree.Root.Left.Left.Value);
            Assert.AreEqual(3, binaryTree.Root.Left.Right.Value);

            // Test right subtree
            Assert.AreEqual(6, binaryTree.Root.Right.Value);
            Assert.AreEqual(5, binaryTree.Root.Right.Left.Value);
            Assert.AreEqual(7, binaryTree.Root.Right.Right.Value);
        }

        [TestMethod]
        public void TestArrayToBST_InOrder() //access all the nodes in sorted order
        {
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            binaryTree.ArrayToBST(array);

            binaryTree.PrintTree();
            binaryTree.PrintTreeVertically();
            // Test the root
            binaryTree.InOrderTraversal(binaryTree.Root,i=> Console.WriteLine(i));
        }

        [TestMethod]
        public void TestArrayToBST_PreOrder()
        {
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            binaryTree.ArrayToBST(array);

            binaryTree.PrintTree();
            binaryTree.PrintTreeVertically();
            // Test the root
            binaryTree.PreOrderTraversal(binaryTree.Root, i => Console.WriteLine(i));
        }

        [TestMethod]
        public void TestArrayToBST_PostOrder()
        {
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            binaryTree.ArrayToBST(array);

            binaryTree.PrintTree();
            binaryTree.PrintTreeVertically();
            // Test the root
            binaryTree.PostOrderTraversal(binaryTree.Root, i => Console.WriteLine(i));
        }


        [TestMethod]
        public void TestBST_BFS_LevelTraversalWithQueue() //access all the nodes in level order using a queue
        {
            //arrange
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            binaryTree.ArrayToBST(array);


            //act by traversing the tree in Level Order
            binaryTree.PrintTree();
            binaryTree.PrintTreeVertically();

            binaryTree.LevelTraversal(binaryTree.Root, i => Console.WriteLine(i));
        }
    }
}
