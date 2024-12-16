using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DSA.DataStructure
{
    public partial class LinkedList
    {

        public object? this[int index] {
            get //  Node x = aInstance[6];
            {
                if (index < 0 || index >= this.Count) throw new IndexOutOfRangeException();
                if (index == 0) return First;
                if (index == Count - 1) return Last;
                Node n = First;
                for (int i = 0; i < index; i++)
                {
                    if (n.Next != null) { n = n.Next; }
                }
                return n;
            }
            set   // aInstance[3] = new Node("....");
                  //should replace item at index location, not insert
            {
                if (index < 0 || index> Count - 1) { throw new IndexOutOfRangeException(); }

                if (index == 0) {
                    ((Node)value).Next = First.Next;
                    First = (Node)value!;
                
                }
                if (index == Count-1) { Last = (Node)value!;return; }
               
                Node n = First;
                for (int i = 0; i < index; i++)
                    {
                        if (i == index - 1) {
                            ((Node)value!).Next = n!.Next.Next;
                            n.Next = ((Node)value!);
                            return;
                        }
                        n = n.Next;
                    }
                } 
        }
                

        public int Add(object? value)
        {
           
            if (value is Node node)
            {
                this.Add(node);
                return Count - 1;
            }
            else
            {
                throw new InvalidOperationException("You can only add Node object to this list.");
            }
        }

        public void Clear()
        {
            this.First = null;
            this.Last = null;
        }

        public bool Contains(object? value)
        {
            if (First == null) return false; //empty list

            if (value is Node nodeToFind) { //is it a node object?
                if (First.Value.Equals(nodeToFind.Value)) return true;

                Node listNode = First;
                while (listNode.Next != null)
                {
                    listNode = listNode.Next;
                    if (listNode.Value.Equals(nodeToFind.Value))
                        return true;
                }
                return false;
            } 
            else { //the list will not contain non-node object
                return false;
            }
           
            
        }

        public int IndexOf(object? value)
        {
            int counter = 0;
            if (value is Node nodeToFind)
            {
                foreach (Node n in this) {
                    if (n.Value.Equals(nodeToFind.Value)) {
                        return counter;
                    }
                    counter++;
                }
                return -1;
            }
            else
            {
                throw new InvalidOperationException("You can only find Node object on this list.");
            }
        }

    }
}
