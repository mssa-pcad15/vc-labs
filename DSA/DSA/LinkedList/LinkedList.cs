using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DSA.DataStructure
{
    public class LinkedList : IEnumerable,IEnumerator, ICollection
    {
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public Node? First { get; private set; }
        public Node? Current { get; private set; }
        public Node? Last { get; private set; }
        public int Count { 
            get {
                if (this.First is null) return 0;
                int count = 1;
                Node last = First;
                while (last.Next != null)
                {
                    count++;
                    last = last.Next;
                }
                return count;
            } 
        }

        object IEnumerator.Current => this.Current!;

        public LinkedList()
        {
            
            this.First = null;
            this.Current = null;
       
        }

        public LinkedList(string s):this() //constructor chaining
        {
            this.First = new Node(s);
            this.Last = First;
          
        }

        public bool MoveNext() {
            if (this.Current is null) {
                if (First is not null)
                    { this.Current = First; return true; }
                return false;
            }
            
            if (this.Current.Next==null) return false;

            this.Current = this.Current.Next;

            return true;
        }
        public void Add(Node node)
        {
            if (this.First is null)
            {
                this.First = node;
                this.Last = node;
                return;
            }
            Node last = First;
            while (last.Next != null) { 
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

            Node n = First;
            int dest = index;
            for (int i = 0; i < this.Count; i++)
            {
                array.SetValue(n, dest);
                if (n.Next != null) {n = n.Next;}
                dest++;
            }
        }

        public void Reset() {
            this.Current = null;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }
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
}
