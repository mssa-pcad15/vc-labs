using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DSA.DataStructure
{
    public  class LinkedList<T> :
        IEnumerable<Node<T>>, IEnumerator<Node<T>>, ICollection<Node<T>>,IList<Node<T>>
    {
   
        #region IEnumerator<T> Properties

        public Node<T>? First { get; private set; }
        object IEnumerator.Current => this.Current!;
        public Node<T>? Current { get; private set; }
        public Node<T>? Last { get; private set; }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        #endregion

        #region ICollection<T> Properties
        public int Count
        {
            get
            {
                if (this.First is null) return 0;
                int count = 1;
                Node<T> last = First;
                while (last.Next != null)
                {
                    count++;
                    last = last.Next;
                }
                return count;
            }
        }
      
        public bool IsReadOnly => false;

        #endregion

        #region IList<T> Properties
        public Node<T> this[int index]
        {
            get //  Node x = aInstance[6];
            {
                if (index < 0 || index >= this.Count) throw new IndexOutOfRangeException();
                if (index == 0) return First!;
                if (index == Count - 1) return Last!;
                Node<T> n = First!;
                for (int i = 0; i < index; i++)
                {
                    if (n.Next != null) { n = n.Next; }
                }
                return n;
            }
            set   // aInstance[3] = new Node("....");
                  //should replace item at index location, not insert
            {
                if (index < 0 || index > Count - 1) { throw new IndexOutOfRangeException(); }

                if (index == 0)
                {
                    value.Next = First.Next;
                    First = value!;
                    return;
                }//first node
                if (index == Count - 1) { Last = value!; return; }//last node

                //nodes in between
                Node<T>? n = First!;
                for (int i = 0; i < index; i++)
                {
                    if (i == index - 1)
                    {
                        value.Next = n.Next!.Next;
                        n.Next = value;
                        return;
                    }
                    n = n.Next!;
                }
            }
        }

        #endregion

        #region Constructors

        public LinkedList()
        {

            this.First = null;
            this.Current = null;

        }

        public LinkedList(T input) : this() //constructor chaining
        {
            this.First = new Node<T>(input);
            this.Last = First;

        }



        #endregion

        #region IEnumerator<T> and IEnumerable Methods
        IEnumerator<DataStructure.Node<T>> IEnumerable<DataStructure.Node<T>>.GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (this.Current is null)
            {
                if (First is not null)
                { this.Current = First; return true; }
                return false;
            }

            if (this.Current.Next == null) return false;

            this.Current = this.Current.Next;

            return true;
        }
        public void Reset()
        {
            this.Current = null;
        }

        #endregion

        #region ICollection<T> Methods
        public void Add(Node<T> node)
        {
            if (this.First is null)
            {
                this.First = node;
                this.Last = node;
                return;
            }
            Node<T> last = First;
            while (last.Next != null)
            {
                last = last.Next;
            }
            last.Next = node;
            this.Last = node;
        }
        public void Clear()
        {
            this.First = null;
            this.Last = null;
            this.Current = null;
        }
        public bool Contains(Node<T> item)
        {
            if (First is null) return false; //empty list

            if (item is Node<T> nodeToFind)
            { //is it a node object?
                if (First.Value!.Equals(nodeToFind.Value)) return true;

                Node<T> listNode = First;
                while (listNode.Next != null)
                {
                    listNode = listNode.Next;
                    if (listNode.Value!.Equals(nodeToFind.Value))
                        return true;
                }
                return false;
            }
            else
            { //the list will not contain non-node object
                return false;
            }
        }
        public void CopyTo(Node<T>[] array, int index)
        {
            if (array == null) throw new ArgumentNullException("array is null");
            if (array.Length == 0) throw new ArgumentException("Zero length array.");
            if (array.Rank != 1) throw new ArgumentException("Can't do multi dimensional arrays");
            if (index < 0 || index > array.Length - 1) throw new ArgumentOutOfRangeException("index is less than zero or out of bound.");
            if (First == null) throw new ArgumentNullException("This is an empty List.");



            if (array.Length - index < this.Count) throw new ArgumentException(
                $"Array is not big enough to store {this.Count} elements starting at index {index}");

            Node<T> n = First;
            int dest = index;
            for (int i = 0; i < this.Count; i++)
            {
                array.SetValue(n, dest);
                if (n.Next != null) { n = n.Next; }
                dest++;
            }
        }
        public void CopyTo(T[] array, int index)
        {
            if (array == null) throw new ArgumentNullException("array is null");
            if (array.Length == 0) throw new ArgumentException("Zero length array.");
            if (array.Rank != 1) throw new ArgumentException("Can't do multi dimensional arrays");
            if (index < 0 || index > array.Length - 1) throw new ArgumentOutOfRangeException("index is less than zero or out of bound.");
            if (First == null) throw new ArgumentNullException("This is an empty List.");



            if (array.Length - index < this.Count) throw new ArgumentException(
                $"Array is not big enough to store {this.Count} elements starting at index {index}");

            T val = First.Value;
            Node<T> n = First;
            int dest = index;
            for (int i = 0; i < this.Count; i++)
            {
                array.SetValue(val, dest);
                if (n.Next != null)
                {
                    n = n.Next;
                    val = n.Value;
                }
                dest++;
            }
        }
        public void CopyTo(Array array, int index)
        {
            if (array == null) throw new ArgumentNullException("array is null");
            if (array.Length == 0) throw new ArgumentException("Zero length array.");
            if (array.Rank != 1) throw new ArgumentException("Can't do multi dimensional arrays");
            if (index < 0 || index > array.Length - 1) throw new ArgumentOutOfRangeException("index is less than zero or out of bound.");
            if (First == null) throw new ArgumentNullException("This is an empty List.");



            if (array.Length - index < this.Count) throw new ArgumentException(
                $"Array is not big enough to store {this.Count} elements starting at index {index}");

            Node<T> n = First;
            int dest = index;
            for (int i = 0; i < this.Count; i++)
            {
                array.SetValue(n, dest);
                if (n.Next != null) { n = n.Next; }
                dest++;
            }
        }
        public bool Remove(Node<T> item)
        {
            bool removed = false;
            if (this.First is null || this.Last is null) return false;

            var node = First;

            Node<T>? previouseNode = null;
            while (node is not null)
            {
                if (node.Value!.Equals(item.Value))
                {
                    if (previouseNode == null) { this.First = node.Next; }//this is the first node
                    else  //this NOT the first node
                    {
                        previouseNode.Next = node.Next;
                        if (node.Next is null) this.Last = previouseNode;
                    }
                    removed = true;
                    return removed;
                }
                previouseNode = node;
                node = node.Next;
            }
            return removed;
        }

        #endregion

        #region IList<T> Methods
        public int IndexOf(Node<T> item)
        {
            int counter = 0;
            foreach (Node<T> n in this)
            {
                if (n.Value!.Equals(item.Value))
                {
                    return counter;
                }
                counter++;
            }
            return -1;
        }
        public void Insert(int index, Node<T> item)
        {
            if (index <= 0)
            {
                item.Next = this.First;
                this.First = item;
                return;
            }
            if (index >= Count)
            {
                this.Last.Next = item;
                this.Last = item;
                return;
            }

            if (index > 0 && index < Count)
            {
                var nodeToInsert = item;
                var nodeAtIndex = this[index];
                var nodeAtBeforIndex = this[index - 1];

                nodeAtBeforIndex.Next = nodeToInsert;
                nodeToInsert.Next = nodeAtIndex;

            } 
        }
        public void RemoveAt(int index)
        {
            if (index == 0) First = First.Next;
            if ((index > 0) && (index < Count))
            {
                if ((index == Count - 1))
                {
                    //we want to remove the last node because index is at the last item
                    this.Last = this[index - 1]!;
                    this.Last.Next = null;
                }
                else
                {
                    (this[index - 1]!).Next = this[index + 1]!;
                }

            }
        }
        #endregion
       
        #region IDisposable
        public void Dispose()
        {
            ;
        } 
        #endregion
    }
   
    public class Node<T>
    {
        public Node<T>? Next { get; internal set; }
        public Node(T s)
        {
            this.Value = s;
            Next = null;
        }

        public T Value { get; set; }
    }
  
}
