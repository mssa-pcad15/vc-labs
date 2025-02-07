using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.BinaryTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.BinaryTree.Tests
{
    [TestClass()]
    public class MergeSortTests
    {
        [TestMethod()]
        public void SortTest()
        {
            int[] array =Enumerable.Range(1, 10000).Select(x=> new Random().Next(1, 10000)).ToArray();

            //int[] array = new int[] { 5, 4, 3, 2, 1,7,8,9,10,11,12 };
            MergeSort<int> mergeSort = new MergeSort<int>(array);
            int[] result = mergeSort.Sort();
            Array.Sort(array);
            CollectionAssert.AreEqual(array, result);
           
        }
        [TestMethod()]
        public void SortInplaceTest()
        {
            int[] actual = Enumerable.Range(1, 10000).Select(x => new Random().Next(1, 10000)).ToArray();
            int[] expected = new int[actual.Length];
            Array.Copy(actual, expected, actual.Length);
            //int[] array = new int[] { 5, 4, 3, 2, 1,7,8,9,10,11,12 };
            MergeSort<int> mergeSort = new MergeSort<int>(actual);
            mergeSort.SortInPlace(actual, 0, actual.Length-1);
            Array.Sort(expected);
           
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}