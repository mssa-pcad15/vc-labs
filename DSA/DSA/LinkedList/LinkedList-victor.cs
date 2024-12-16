using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.DataStructure
{
    public partial class LinkedList 
    {

        public void Insert(int index, object? value)
        {
            if (index<=0 && value is Node firstNode)
            {
                firstNode.Next = this.First;
                this.First = firstNode;
                return;
            }
            if (index >= Count && value is Node lastNode)
            {
                this.Last.Next = lastNode;
                this.Last = lastNode;
                return;
            }

            if (index>0 && index < Count)
            {
                if (value is Node nodeToInsert && this[index] is Node nodeAtIndex && this[index-1] is Node nodeAtBeforIndex) {
                    nodeAtBeforIndex.Next = nodeToInsert;
                    nodeToInsert.Next = nodeAtIndex;
                }
            }
            
        }

        public void Remove(object value)
        {
            RemoveAt(IndexOf(value));
        }


        public void RemoveAt(int index)
        {
            if (index == 0) First = First.Next;
            if ((index > 0) && (index < Count))
            {
                if ((index == Count - 1))
                {
                    //we want to remove the last node because index is at the last item
                    this.Last = (Node)this[index - 1]!;
                    this.Last.Next = null;
                }
                else
                {
                   ((Node)this[index - 1]!).Next= (Node)this[index+1]!;
                }

            }
        }
    }
}
