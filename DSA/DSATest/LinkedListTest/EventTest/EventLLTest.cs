using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSA.LinkedList.Event;
namespace DSATest.LinkedListTest.EventTest
{
    [TestClass]
    public class EventLLTest
    {
        [TestMethod]
        public void EventLLConstructorTest() {
            EventLL<string> l = new EventLL<string>("Hello");
            Assert.AreEqual("Hello", l.First.Value);
            Assert.AreEqual("Hello", l.Last.Value);
            Assert.AreEqual(1, l.Count);
        }
        [TestMethod]
        public void EventLLCountTest()
        {
            EventLL<string> l = new EventLL<string>();
            Assert.AreEqual(0, l.Count);
            Assert.IsNull(l.First);
            Assert.IsNull(l.Last);
            Assert.IsNull(l.Current);
        }
        [TestMethod]
        public void EventLLEmptyListMoveNextTest()
        {
            EventLL<string> l = new EventLL<string>();
            Assert.IsFalse(l.MoveNext());
            Assert.IsNull(l.First);
            Assert.IsNull(l.Last);
            Assert.IsNull(l.Current);
        }

        [TestMethod]
        public void EventLLOneNodeListMoveNextTest()
        {
            EventLL<string> l = new EventLL<string>("Hello");
            Assert.IsTrue(l.MoveNext());
            Assert.AreEqual("Hello", l.Current.Value);
            Assert.AreEqual("Hello", l.First.Value);
            Assert.AreEqual("Hello", l.Last.Value);
            Assert.AreEqual(1, l.Count);
        }

        [TestMethod]
        public void EventLLOneNodeListMoveNextTwiceTest()
        {
            EventLL<string> l = new EventLL<string>("Hello");
            Assert.IsTrue(l.MoveNext());
            Assert.IsFalse(l.MoveNext());
            Assert.AreEqual(1, l.Count);
        }

        [TestMethod]
        public void EventLLOneNodeListMoveNextResetTest()
        {
            EventLL<string> l = new EventLL<string>("Hello");
            Assert.IsTrue(l.MoveNext());
            l.Reset();
            Assert.IsNull(l.Current);
            Assert.AreEqual("Hello",l.First.Value);
            Assert.AreEqual("Hello", l.Last.Value);
            Assert.IsTrue(l.MoveNext());
            Assert.AreEqual(1, l.Count);
        }

        [TestMethod]
        public void EventLLTwoNodeListAddTest()
        {
            EventLL<string> l = new EventLL<string>("Hello");
            Node<string> newNode = new Node<string>("World");
            l.Add(newNode);
            Assert.IsTrue(l.MoveNext());
            Assert.AreEqual("Hello", l.Current.Value);
            Assert.AreEqual("Hello", l.First.Value);
            Assert.AreEqual("World", l.Last.Value);
            Assert.IsTrue(l.MoveNext());
            Assert.AreEqual(2, l.Count);
        }
        [TestMethod]
        public void EventLLThreeNodeListAddTest()
        {
            EventLL<string> l = new EventLL<string>("Hello");
            Node<string> newNode = new Node<string>("World");
            l.Add(newNode);
            Node<string> newNode2 = new Node<string>("There");
            l.Add(newNode2);

            Assert.AreEqual(3, l.Count);
            Assert.IsTrue(l.MoveNext());
            Assert.AreEqual("Hello", l.Current.Value);
            Assert.AreEqual("Hello", l.First.Value);
            Assert.AreEqual("There", l.Last.Value);

            Assert.IsTrue(l.MoveNext());
            Assert.AreEqual("World", l.Current.Value);
            Assert.AreEqual("Hello", l.First.Value);
            Assert.AreEqual("There", l.Last.Value);
            Assert.AreEqual(3, l.Count);

            Assert.IsTrue(l.MoveNext());
            Assert.AreEqual("There", l.Current.Value);
            Assert.AreEqual("Hello", l.First.Value);
            Assert.AreEqual("There", l.Last.Value);
            Assert.AreEqual(3, l.Count);
        }

        [TestMethod]
        public void EventLLThreeNodeListCollectionInitializerTest()
        {
            EventLL<string> l = [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];
            Assert.AreEqual("Hello", l.First.Value);
            Assert.AreEqual("World", l.First.Next.Value);
            Assert.AreEqual("There", l.First.Next.Next.Value);

        }
        
        
        [TestMethod]
        public void EventLLThreeNodeListContainsValueTest()
        {
            EventLL<string> l = [new Node<string>("Hello"),new Node<string>("World"), new Node<string>("There")];
            Assert.IsTrue(l.Contains("Hello"));
            Assert.IsFalse(l.Contains("NotThere"));
        }
        [TestMethod]
        public void EventLLThreeNodeListContainsNodeTest()
        {
            EventLL<string> l = [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];
            Assert.IsTrue(l.Contains(new Node<string>("Hello")));
            Assert.IsFalse(l.Contains(new Node<string>("NotThere")));
        }

        [TestMethod]
        public void EventLLArrayCopyTest()
        {
            EventLL<string> l = 
                [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];

            Node<String>[] array = new Node<string>[5];
            l.CopyTo(array, 0);
            Assert.AreEqual(5, array.Length);
            Assert.AreEqual("Hello", array[0].Value);
            Assert.AreEqual("World", array[1].Value);
            Assert.AreEqual("There", array[2].Value);
        }

        [TestMethod]
        public void EventLLArrayCopyToEndTest()
        {
            EventLL<string> l =
                [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];

            Node<String>[] array = new Node<string>[5];
            l.CopyTo(array, 2);
            Assert.AreEqual(5, array.Length);
            Assert.AreEqual("Hello", array[2].Value);
            Assert.AreEqual("World", array[3].Value);
            Assert.AreEqual("There", array[4].Value);
        }


        [TestMethod]
        public void EventLLRemoveFirstTest()
        {
            EventLL<string> l =
                [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];

            Assert.IsTrue(l.Remove(new Node<string>("Hello")));
            Assert.AreEqual(2, l.Count);
        }
        [TestMethod]
        public void EventLLRemoveMiddleTest()
        {
            EventLL<string> l =
                [new Node<string>("Zero"), 
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),
                new Node<string>("Four"),
                new Node<string>("Five")];

           Assert.IsTrue(l.Remove(new Node<string>("Two")));
            Assert.AreEqual(5, l.Count);
            Assert.AreEqual("Zero", l.First.Value);
            Assert.AreEqual(0, l.First.Index);
            var three = l.First.Next.Next;
            Assert.AreEqual(2, three.Index);
        }
        [TestMethod]
        public void EventLLRemoveEndTest()
        {
            EventLL<string> l =
                [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];

            Assert.IsTrue(l.Remove(new Node<string>("There")));
            Assert.AreEqual(2,l.Count);
        }

        [TestMethod]
        public void EventLLIndexOfTest()
        {
            EventLL<string> l = [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];
            Assert.AreEqual("Hello", l.First.Value);
            Assert.AreEqual("World", l.First.Next.Value);
            Assert.AreEqual("There", l.First.Next.Next.Value);
            Assert.AreEqual(0, l[0].Index);
            int result = l.IndexOf(new Node<string>("Hello")) + 1;
            int result2 = l.IndexOf(new Node<string>("World")) + 1;
            Assert.AreEqual(0, result);
            Assert.AreEqual(1, result2);
        }
    }
}
