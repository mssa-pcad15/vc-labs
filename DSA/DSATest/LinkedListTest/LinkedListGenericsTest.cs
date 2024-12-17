using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LinkedList = DSA.DataStructure.LinkedList<string>;
using Node = DSA.DataStructure.Node<string>;
namespace DSATest.LinkedListTest
{
    [TestClass]
    public class LinkedListTest
    {
        #region Basic Linked List Test - Constructor and movement.

        [TestMethod]
        public void LinkListConstructionTest()
        {

            LinkedList l = new ();
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
            Assert.AreSame(l.First, l.Current);
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

        #endregion


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

        #region IListTests
   
        [TestMethod]
        public void LinkedListIListIsReadOnlyIsFalse()
        {
            var l = new LinkedList();
            Assert.IsFalse(l.IsReadOnly);
        }
        [TestMethod]
        public void LinkedListIListIndexerTest()
        {
            LinkedList l = [new Node("Apple"),new Node("Banana"), new Node("Cherry") ];
            
            Assert.AreEqual("Apple", ((Node) l[0]!).Value);
            Assert.AreEqual("Banana", ((Node) l[1]!).Value);
            Assert.AreEqual("Cherry", ((Node) l[2]!).Value);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(3)]
        public void LinkedListIListIndexerOOB(int position)
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
       
            Assert.ThrowsException<IndexOutOfRangeException>(()=> ((Node)l[position]!).Value);
        }

        [TestMethod]
        public void LinkedListIListIndexerSetterFirst()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            l[0]= new Node("Pie");
            Assert.AreEqual("Pie", l.First!.Value);
            Assert.AreEqual("Cherry", l.Last!.Value);
        }

        [TestMethod]
        public void LinkedListIListIndexerSetterLast()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            l[2] = new Node("Pie");
            Assert.AreEqual("Pie", l.Last.Value);
            Assert.AreEqual("Apple", l.First.Value);
        }
        [TestMethod]
        public void LinkedListIListIndexerSetterAny()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            l[1] = new Node("Pie");
            Assert.AreEqual("Apple", l.First.Value);
            Assert.AreEqual("Pie", l.First.Next.Value);
            Assert.AreEqual("Cherry", l.First.Next.Next.Value);
        }

        [TestMethod]
        public void LinkedListIListIndexerSetterAny2()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry"), new Node("Date")];
            l[2] = new Node("Pie");
            Assert.AreEqual("Apple", l.First.Value);
            Assert.AreEqual("Banana", l.First.Next.Value);
            Assert.AreEqual("Pie", l.First.Next.Next.Value);
            Assert.AreEqual("Date", l.First.Next.Next.Next.Value);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(3)]
        [DataRow(4)]
        public void LinkedListIListIndexerSetterException(int pos)
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];

            Assert.ThrowsException<IndexOutOfRangeException>(() => l[pos] = new Node("Pie"));
        }
        [TestMethod]
        public void LinkedListIListAddTest()
        {
            var n = new Node("Cherry");

			LinkedList l = [new Node("Apple"), new Node("Banana"), n];
            var o = new Node("Hello");
           
            l.Add(o);
        
            
            Assert.AreEqual("Hello",l.Last.Value);
			Assert.AreEqual("Hello", n.Next.Value);
		}
     

        [TestMethod]
        public void LinkedListIListClearTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            l.Clear();
            Assert.IsTrue(l.First == null,"First is not null after clear method.");
            Assert.IsTrue(l.Last == null, "Last is not null after clear method.");
			Assert.IsTrue(l.Current == null, "Last is not null after clear method.");
			Assert.IsTrue(l.Count == 0, "Count is not 0 after clear method.");
        }

        [TestMethod]
        public void LinkedListIListContainsTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var actual = l.Contains(new Node("Apple"));
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void LinkedListIListContainsNegativeTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var actual = l.Contains(new Node("Apple2"));
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void LinkedListIListIndexOfTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var actual = l.IndexOf(new Node("Apple"));
            var expected = 0;
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void LinkedListIListIndexOfTest2()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var actual = l.IndexOf(new Node("Cherry"));
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LinkedListIListIndexOfNegativeTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var actual = l.IndexOf(new Node("Pear"));
            var expected = -1;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LinkedListIListInsertFirstTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var node =new Node("Pear");
            l.Insert(0, node);
            Assert.AreSame(l.First,node);
            Assert.AreEqual("Apple", ((Node)l[1]!).Value);
            Assert.AreEqual("Cherry", ((Node)l[3]!).Value);
            Assert.AreEqual("Cherry", l.Last!.Value);
            Assert.AreEqual(4, l.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void LinkedListIListInsertSecondTest(int position)
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var node = new Node("Pear");
            l.Insert(position, node);
            Assert.AreNotSame(l.First, node);
            Assert.AreEqual("Pear", ((Node)l[position]!).Value);
            Assert.AreEqual("Apple", ((Node)l[0]!).Value);
            Assert.AreEqual("Cherry", ((Node)l[3]!).Value);
            Assert.AreEqual("Cherry", l.Last!.Value);
            Assert.AreEqual(4, l.Count);
        }
        [TestMethod]
        [DataRow(3)] 
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        public void LinkedListIListInsertLastTest(int pos)
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var node = new Node("Pear");
            l.Insert(pos, node);
            Assert.AreNotSame(l.First, node);
            Assert.AreEqual("Pear", ((Node)l[3]!).Value);
            Assert.AreEqual("Pear", l.Last!.Value);

            Assert.AreEqual("Apple", ((Node)l[0]!).Value);
            Assert.AreEqual("Banana", ((Node)l[1]!).Value);
            Assert.AreEqual("Cherry", ((Node)l[2]!).Value);
            Assert.AreEqual(4, l.Count);
        }

        [TestMethod]
        public void LinkedListIListRemoveFirstNodeTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var node = new Node("Apple");
            l.Remove(node);
            Assert.IsFalse(l.Contains(node));
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual("Banana", l.First.Value);
            Assert.AreEqual("Cherry", l.Last.Value);
        }

        [TestMethod]
        public void LinkedListIListRemoveLastNodeTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var node = new Node("Cherry");
            l.Remove(node);
            Assert.IsFalse(l.Contains(node));
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual("Apple", l.First.Value);
            Assert.AreEqual("Banana", l.Last.Value);
        }

        [TestMethod]
        public void LinkedListIListRemoveSecondNodeTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
            var node = new Node("Banana");
            l.Remove(node);
            Assert.IsFalse(l.Contains(node));
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual("Apple", l.First.Value);
            Assert.AreEqual("Cherry", l.Last.Value);
        }


        [TestMethod]
        public void LinkedListIListRemoveAtFirstNodeTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];
   
            l.RemoveAt(0);
            Assert.IsFalse(l.Contains(new Node("Apple")));
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual("Banana", l.First.Value);
            Assert.AreEqual("Cherry", l.Last.Value);
        }
        [TestMethod]
        public void LinkedListIListRemoveAtLastNodeTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];

            l.RemoveAt(2);
            Assert.IsFalse(l.Contains(new Node("Cherry")));
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual("Apple", l.First.Value);
            Assert.AreEqual("Banana", l.Last.Value);
        }
        [TestMethod]
        public void LinkedListIListRemoveAtMiddleNodeTest()
        {
            LinkedList l = [new Node("Apple"), new Node("Banana"), new Node("Cherry")];

            l.RemoveAt(1);
            Assert.IsFalse(l.Contains(new Node("Banana")));
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual("Apple", l.First.Value);
            Assert.AreEqual("Cherry", l.Last.Value);
        }
        #endregion

    }
}

