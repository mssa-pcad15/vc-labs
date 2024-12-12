using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DSA.DataStructure;
namespace DSATest.LinkedListTest
{
    [TestClass]
    public class LinkedListTest
    {
        [TestMethod]
        public void LinkListConstructionTest() {

            LinkedList l = new LinkedList();
            Assert.IsNull(l.First);
        }
        [TestMethod]
        public void LinkListConstructionWithNodeTest()
        {

            LinkedList l = new LinkedList("Hello");

            Assert.IsNotNull(l.First);
            Assert.IsNull(l.Current);
            Assert.AreEqual("Hello", l.First.Value);
        }

        [TestMethod]
        public void LinkListConstructorAddTest()
        {
            LinkedList l = new LinkedList();
            var n = new Node("Hello");
            l.Add(n);
            Assert.IsNotNull(l.First);
            Assert.AreSame(l.First, n);
            Assert.AreSame(l.Last, n);
        }
        [TestMethod]
        public void LinkListMoveNextTest()
        {
            LinkedList l = new LinkedList("Hello");
            bool hasNext = l.MoveNext();
            Assert.IsTrue(hasNext);
            Assert.AreSame(l.First,l.Current);
            Assert.AreSame(l.Last, l.Current);

            Node n = new Node("World");
            Assert.IsNotNull(l.Current);
            l.Add(n);
            hasNext = l.MoveNext();
            Assert.IsTrue(hasNext);
         
            Assert.AreSame(l.Current, n);
            Assert.AreNotSame(l.First, n);
            Assert.AreSame(l.Last, n);
        }

        [TestMethod]
        public void LinkListNodeAddedBecomesLastNodeTest()
        {
            // Add always append to the list
            LinkedList l = new LinkedList("Hello");
            Node n = new Node("World");

            Assert.IsNull(l.Current);
            l.Add(n);
            Assert.AreSame(n, l.Last);
        }

       

        #region IEnumerable Tests
        [TestMethod]
        public void LinkedListIEnumerableTest()
        {

            LinkedList l = new LinkedList();
            l = new LinkedList("Hello"); //1
            Node n = new Node("World");
            l.Add(n);
            n = new Node("This");
            l.Add(n);
            n = new Node("is");
            l.Add(n);
            n = new Node("fun");
            l.Add(n);
            int count = 0;
            foreach (var item in l)
            {
                count++;
            }
            Assert.AreEqual(5, count);
        }

        [TestMethod]
        public void LinkedListIEnumerableResetTest()
        {

            LinkedList l = new LinkedList();
            l = new LinkedList("Hello"); //1
            Node n = new Node("World");
            l.Add(n);
            n = new Node("This");
            l.Add(n);
            n = new Node("is");
            l.Add(n);
            n = new Node("fun");
            l.Add(n);
            int count = 0;
            foreach (var item in l)
            {
                count++;
            }
            l.Reset();
            Assert.IsNull(l.Current);

            l.MoveNext();
            Assert.AreSame(l.First, l.Current);
        }

        [TestMethod]
        public void LinkedListIEnumerableCurrentTest()
        {

            LinkedList l = new LinkedList();
            Assert.IsNull(l.Current);
            l = new LinkedList("Hello");
            Assert.IsNull(l.Current);
        }

        [TestMethod]
        public void LinkedListIEnumerableMoveNextOnEmptyList()
        {

            LinkedList l = new LinkedList();
            Assert.IsNull(l.Current);
            Assert.IsFalse(l.MoveNext());
            
        }

        #endregion


        #region ICollectionTests
        [TestMethod]
        public void LinkedListICollectionPropertyTest() { 
        
            var l = new LinkedList();
            Assert.IsFalse(l.IsSynchronized);
            Assert.AreSame(l.SyncRoot, l);
        }

        [TestMethod]
        public void LinkedListNodeCountTest()
        {
            // Add always append to the list
            LinkedList l = new LinkedList();
            Assert.AreEqual(0, l.Count);

            l = new LinkedList("Hello"); //1
            Assert.AreEqual(1, l.Count);

            Node n = new Node("World");
            l.Add(n);
            Assert.AreEqual(2, l.Count);

            n = new Node("This");
            l.Add(n);
            Assert.AreEqual(3, l.Count);

            n = new Node("is");
            l.Add(n);
            Assert.AreEqual(4, l.Count);

            n = new Node("fun");
            l.Add(n);
            Assert.AreEqual(5, l.Count);

            Assert.AreSame(n, l.Last);
        }

        [TestMethod]
        public void LinkedListICollectionCopyToTest()
        { 
            var l = new LinkedList("Apple");
            l.Add(new Node("Banana"));
            l.Add(new Node("Cherry"));

            Node[] input = [new ("Zebra"), new("Yellow"), new("Xray"), new("none"), new("none"), new("none")];
            l.CopyTo(input, 3);

            Assert.AreEqual(input[3].Value, "Apple");
            Assert.AreEqual(input[4].Value, "Banana");
            Assert.AreEqual(input[5].Value, "Cherry");
        }

        [TestMethod]
        public void LinkedListICollectionCopyToArrayThatsTooSmall()
        {
            var l = new LinkedList("Apple");
            l.Add(new Node("Banana"));
            l.Add(new Node("Cherry"));

            Node[] input = [new("Zebra"), new("Yellow"), new("Xray"), new("none")];
            Assert.ThrowsException<ArgumentException>(()=>l.CopyTo(input, 3));
        }

        [TestMethod]
        public void LinkedListICollectionCopyToArrayThatsNull()
        {
            var l = new LinkedList("Apple");
            l.Add(new Node("Banana"));
            l.Add(new Node("Cherry"));

            Node[] input = null;
            Assert.ThrowsException<ArgumentNullException>(() => l.CopyTo(input, 3));
        }
        [TestMethod]
        public void LinkedListICollectionCopyToArrayThatsEmpty()
        {
            var l = new LinkedList("Apple");
            l.Add(new Node("Banana"));
            l.Add(new Node("Cherry"));

            Node[] input = [];
            Assert.ThrowsException<ArgumentException>(() => l.CopyTo(input, 3));
        }

        [TestMethod]
        public void LinkedListICollectionCopyToArrayIndexOOB()
        {
            var l = new LinkedList("Apple");
            l.Add(new Node("Banana"));
            l.Add(new Node("Cherry"));

            Node[] input = [new("Zebra"), new("Yellow"), new("Xray"), new("none")];
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => l.CopyTo(input, 4));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => l.CopyTo(input, -1));
        }
        [TestMethod]
        public void LinkedListICollectionCopyToMultiDimensionalArray()
        {
            var l = new LinkedList("Apple");
            l.Add(new Node("Banana"));
            l.Add(new Node("Cherry"));

            Node[,] input = {{ new("Zebra"), new("Yellow")},{ new("Xray"), new("none")} };
            Assert.ThrowsException<ArgumentException>(() => l.CopyTo(input, 4));
        }

        [TestMethod]
        public void LinkedListICollectionCopyToFromEmptyList()
        {
            var l = new LinkedList();
          

            Node[] input = [new("Zebra"), new("Yellow"), new("Xray"), new("none")];
            Assert.ThrowsException<ArgumentNullException>(() => l.CopyTo(input, 1));
        }
        #endregion
    }
}

