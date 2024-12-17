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
    public partial class LinkedList<T> : IEnumerable, IEnumerator, ICollection, IList,
        ICollection<Node<T>>, IEnumerable<Node<T>>, IEnumerator<Node<T>>
    {
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public Node<T>? First { get; private set; }
        public Node<T>? Current { get; private set; }
        public Node<T>? Last { get; private set; }
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

        public bool IsFixedSize => false;
        public bool IsReadOnly => false;


        object IEnumerator.Current => this.Current!;

        Node<T> IEnumerator<Node<T>>.Current => this.Current!;

        public LinkedList()
        {

            this.First = null;
            this.Current = null;

        }

        public LinkedList(T s) : this() //constructor chaining
        {
            this.First = new Node<T>(s);
            this.Last = First;

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

        public void Reset()
        {
            this.Current = null;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }


        IEnumerator<DataStructure.Node<T>> IEnumerable<DataStructure.Node<T>>.GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {
            ;
        }


        #region ICollection
        //public void Add(Node<T> item)
        //{

        //}

        public void Clear() //Removes all items from the ICollection<T>.
        {
            First = Last = null;
        }

        public bool Contains(Node<T> item) //Determines whether the ICollection<T> contains a specific value.
        {
            while (First?.Next != null)
            {
                if (First.Value.Equals(item.Value))
                   return true;

                First = First.Next;
            }
            return false;
        }

        public void CopyTo(Node<T>[] array, int arrayIndex)//Copies the elements of the ICollection<T> to an Array
        {
            if (array == null) throw new ArgumentNullException("array is null");
            if (array.Length == 0) throw new ArgumentException("Zero length array.");
            if (array.Rank != 1) throw new ArgumentException("Can't do multi dimensional arrays");
            if (arrayIndex < 0 || arrayIndex > array.Length - 1) throw new ArgumentOutOfRangeException("index is less than zero or out of bound.");
            if (First == null) throw new ArgumentNullException("This is an empty List.");



            if (array.Length - arrayIndex < this.Count) throw new ArgumentException(
                $"Array is not big enough to store {this.Count} elements starting at index {arrayIndex}");

            Node<T> n = First;
            int dest = arrayIndex;
            for (int i = 0; i < this.Count; i++)
            {
                array.SetValue(n, dest);
                if (n.Next != null) { n = n.Next; }
                dest++;
            }
        }

        public bool Remove(Node<T> item)//Removes first occurrence of a specific object from the ICollection<T>.
        {
            while (First?.Next != null)
            {
                if (First.Value.Equals(item.Value))
                {
                    First.Next = First.Next.Next;
                    return true;
                }
            }
            return false;
        }

        #endregion ICollection

    }

    public class Node {
        public Node? Next { get; internal set; }
        public Node(string s)
        {
            this.Value = s;
            Next=null;
        }

        public string Value { get; set; }
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
    public class IceCream
    {
        public string Flavor { get; set; }
    }
}
