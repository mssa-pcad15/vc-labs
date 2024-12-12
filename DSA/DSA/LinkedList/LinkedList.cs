using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.DataStructure
{
    public class LinkedList 
    {
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
        public LinkedList()
        {
            
            First = null;
            Current = null;
        }

        public LinkedList(string s):this()
        {
            First = new Node(s);
            Current = First;
            
        }

        public bool MoveNext() {
            if (this.Current is null) return false;
            if (this.Current.Next is null) return false;
            this.Current = this.Current.Next;
            return true;
        }
        public void Add(Node node)
        {
            if (this.First is null) this.First = node;
            Node last = First;
            while (last.Next != null) { 
                last = last.Next;
            }
            last.Next = node;
            this.Last = node;
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
