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
            Assert.IsNotNull(l.Current);
            Assert.AreSame(l.Current,l.First);
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
        }
        [TestMethod]
        public void LinkListNodeAddTest()
        {
            LinkedList l = new LinkedList("Hello");
            Node n = new Node("World");
            Assert.IsNotNull(l.Current);
            l.Add(n);
            bool hasNext = l.MoveNext();
            Assert.IsTrue(hasNext);
         
            Assert.AreSame(l.Current, n);
            Assert.AreNotSame(l.First, n);
        }

        [TestMethod]
        public void LinkListNodeAddedBecomesLastNodeTest()
        {
            // Add always append to the list
            LinkedList l = new LinkedList("Hello");
            Node n = new Node("World");

            Assert.IsNotNull(l.Current);
            l.Add(n);
            Assert.AreSame(n, l.Last);
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
    }
}
