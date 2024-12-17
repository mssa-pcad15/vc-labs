using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.LinkedList.Event
{
    public class EventLL<T>: IEnumerable<T>,IEnumerator<T>
    {
        public Node<T>? First = null;
        public Node<T>? Last = null;
        public Node<T>? Current = null;
        private int _currentPointer = -1;
        object IEnumerator.Current => this.Current;

        T IEnumerator<T>.Current => this.Current.Value;

        internal event EventHandler<NodeEventsArgs<T>>? OnCommand;

        public void Dispose()
        {
            ;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (First == null || Last == null) return false;
            if (_currentPointer== -1 )
            {
                _currentPointer = 0;
                return true;
            }
            _currentPointer++;
            return NodeExistsByIndex(_currentPointer);
        }

        private bool NodeExistsByIndex(int currentPointer)
        {
           var arg = new NodeEventsArgs<T>() { TypeOfCommand = NodeCommandType.NodeSearchByIndex,
           new Node<object>(new object()) {  }
           }
        }

        public void Reset()
        {
            _currentPointer = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class Node<T> 
    {
        internal int Index = -1;

        public T Value;
        public Node<T>? Next;

        public Node()
        {
            Value = default;
        }
        public Node(T input)
        {
            Value = input;
        }
        public Node(T input, EventLL<T> owner) : this(input) 
        {
            owner.OnCommand += this.HandleEvent!;
        }
        private void HandleEvent(object sender, NodeEventsArgs<T> arg)
        {
            throw new NotImplementedException();
        }
    }
}
