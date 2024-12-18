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
        public void EventLLIndexerGetTest()
        {
            EventLL<string> l =
                [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];

            Assert.AreEqual("Hello", l[0].Value);
            Assert.AreEqual("World", l[1].Value);
            Assert.AreEqual("There", l[2].Value);
        }
        [TestMethod]
        public void EventLLIndexerSetTest()
        {
            EventLL<string> l =
                [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];
            l[0] = new Node<string>("HelloV2");
            l[1] = new Node<string>("WorldV2");
            l[2] = new Node<string>("ThereV2");
            Assert.AreEqual("HelloV2", l[0].Value);
            Assert.AreEqual("WorldV2", l[1].Value);
            Assert.AreEqual("ThereV2", l[2].Value);
        }
        [TestMethod]
        public void EventLLIndexerGetExceptionTest()
        {
            EventLL<string> l = new();
            Assert.ThrowsException<InvalidOperationException>(() => l[0]);
            l =
                [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];
            
            Assert.ThrowsException<IndexOutOfRangeException>(()=> l[-1]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => l[3]);
          
        }
        [TestMethod]
        public void EventLLIndexerSetExceptionTest()
        {
            EventLL<string> l = new();
            Assert.ThrowsException<InvalidOperationException>(() => l[0] = new Node<string>("test"));
            l =
                [new Node<string>("Hello"), new Node<string>("World"), new Node<string>("There")];

            Assert.ThrowsException<IndexOutOfRangeException>(() => l[-1] = new Node<string>("test"));
            Assert.ThrowsException<IndexOutOfRangeException>(() => l[3] = new Node<string>("test"));

        }

        [TestMethod]
        public void EventLLIndexOfTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),
                new Node<string>("Four"),
                new Node<string>("Five")];

            Assert.AreEqual(0, l.IndexOf("Zero"));
            Assert.AreEqual(1, l.IndexOf("One"));
            Assert.AreEqual(5, l.IndexOf("Five"));
            Assert.AreEqual(3, l.IndexOf("Three"));
        }
        [TestMethod]
        public void EventLLIndexOfNodeTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),
                new Node<string>("Four"),
                new Node<string>("Five")];

            Assert.AreEqual(0, l.IndexOf(new Node<string>("Zero")));
            Assert.AreEqual(1, l.IndexOf(new Node<string>("One")));
            Assert.AreEqual(3, l.IndexOf(new Node<string>("Three")));
            Assert.AreEqual(5, l.IndexOf(new Node<string>("Five")));
        
        }

        [TestMethod]
        public void EventLLInsertFirstTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),
               ];

            l.Insert(0, new Node<string>("BeforeZero"));
            Assert.IsTrue(l.Contains("BeforeZero"));
            Assert.IsTrue(l.First!.Value == "BeforeZero");
            Assert.IsTrue(l.Last!.Value == "Three");
            Assert.IsTrue(l.Count==5);
        }
        [TestMethod]
        public void EventLLInsertSecondTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),
               ];

            l.Insert(1, new Node<string>("AfterZero"));
            Assert.IsTrue(l.Contains("AfterZero"));
            Assert.IsTrue(l.First!.Value == "Zero");
            Assert.IsTrue(l[1].Value == "AfterZero");
            Assert.IsTrue(l.Last!.Value == "Three");
            Assert.IsTrue(l.Count == 5);
        }
        [TestMethod]
        public void EventLLInsertThirdTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),
               ];

            l.Insert(2, new Node<string>("AfterOne"));
            Assert.IsTrue(l.Contains("AfterOne"));
            Assert.IsTrue(l.First!.Value == "Zero");
            Assert.IsTrue(l[2].Value == "AfterOne");
            Assert.IsTrue(l.Last!.Value == "Three");
            Assert.IsTrue(l.Count == 5);

        }
        [TestMethod]
        public void EventLLInsertBeforeLastTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),//insert here
                new Node<string>("Three"),
               ];
            l.Insert(3, new Node<string>("AfterTwo"));
            Assert.IsTrue(l.Contains("AfterTwo"));
            Assert.IsTrue(l.First!.Value == "Zero");
            Assert.IsTrue(l[3].Value == "AfterTwo");
            Assert.IsTrue(l.Last!.Value == "Three");
            Assert.IsTrue(l.Count == 5);
        }
        [TestMethod]
        public void EventLLInsertLastTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),//insert here
               ];
            l.Insert(4, new Node<string>("AfterThree"));
            Assert.IsTrue(l.Contains("AfterThree"));
            Assert.IsTrue(l.First!.Value == "Zero");
            Assert.IsTrue(l[4].Value == "AfterThree");
            Assert.IsTrue(l.Last!.Value == "AfterThree");
            Assert.IsTrue(l.Count == 5);
        }
        [TestMethod]
        public void EventLLInsertAfterLastTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),//insert here
               ];
            l.Insert(5, new Node<string>("AfterThree"));
            Assert.IsTrue(l.Contains("AfterThree"));
            Assert.IsTrue(l.First!.Value == "Zero");
            Assert.IsTrue(l[4].Value == "AfterThree");
            Assert.IsTrue(l.Last!.Value == "AfterThree");
            Assert.IsTrue(l.Count == 5);
        }

        [TestMethod]
        public void EventLLRemoveAtZeroTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),//insert here
               ];

            l.RemoveAt(0);
            Assert.IsTrue(l.First!.Value == "One");
            Assert.IsTrue(l.Last!.Value == "Three");
            Assert.AreEqual(3, l.Count);
        }

        [TestMethod]
        public void EventLLRemoveAtOneTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),//insert here
               ];
            l.RemoveAt(1);
            Assert.IsTrue(l.First!.Value == "Zero");
            Assert.IsTrue(l[1].Value == "Two");
            Assert.IsTrue(l.Last!.Value == "Three");
            Assert.AreEqual(3, l.Count);
        }

        [TestMethod]
        public void EventLLRemoveAtLastTest()
        {
            EventLL<string> l =
              [new Node<string>("Zero"),
                new Node<string>("One"),
                new Node<string>("Two"),
                new Node<string>("Three"),//insert here
               ];
            l.RemoveAt(3);
            Assert.IsTrue(l.First!.Value == "Zero");
            Assert.IsTrue(l[1].Value == "One");
            Assert.IsTrue(l[2].Value == "Two");
            Assert.IsTrue(l.Last!.Value == "Two");
            Assert.AreEqual(3, l.Count);
        }
    }
}

