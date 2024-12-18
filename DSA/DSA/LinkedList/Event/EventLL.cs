using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DSA.LinkedList.Event
{
    public class EventLL<T> : 
        IEnumerable<Node<T>>, IEnumerator<Node<T>?>,ICollection<Node<T>>,IList<Node<T>>
    {
        #region List Properties
        public Node<T>? First = null;
        public Node<T>? Last = null;
        public Node<T>? Current = null;
        private int _currentPointer = -1;
        object? IEnumerator.Current => this.Current;
        Node<T>? IEnumerator<Node<T>?>.Current => this.Current;
        internal bool _refreshCount=true;
        private int _cachedCount=0;

        public int Count
        {
            get
            {
                if (_refreshCount)
                {
                    var arg = new NodeEventsArgs<T>() { TypeOfCommand = NodeCommandType.GetCount };
                    OnCommand?.Invoke(this, arg);
                    _cachedCount = arg.CountResult;//raising the event
                    _refreshCount = false;
                    return _cachedCount; // can you make this work better?}
                }
                else
                {
                    return _cachedCount;
                }
            }
        }

        public bool IsReadOnly => false;
        #endregion

        #region Indexer Property IList<T>
        public Node<T> this[int index]
        {
            get => getNodeById(index);
            set => setNodeById(index, value);
        }

        private void setNodeById(int index, Node<T> newNode)
        {
            if (this.First == null) { throw new InvalidOperationException("List is empty."); }
            if (index > Count - 1 || index < 0) { throw new IndexOutOfRangeException(); }
            newNode.Index = index;
            var arg = new NodeEventsArgs<T>()
            {
                TypeOfCommand = NodeCommandType.ReplaceNode,
                Target = newNode
            };

            OnCommand?.Invoke(this, arg);
        }

        private Node<T> getNodeById(int index)
        {
            if (this.First is null) { throw new InvalidOperationException("List is empty."); }
            if (index > Count - 1 || index < 0) { throw new IndexOutOfRangeException(); }
            (bool isFound, Node<T>? foundNode) = nodeExistsByIndex(index);

            return (foundNode is not null) ? foundNode : throw new IndexOutOfRangeException($"Unable to find node at index {index}");

        }

        #endregion

        //Event
        internal event EventHandler<NodeEventsArgs<T>>? OnCommand;


        public void Dispose()
        {
            ;
        }

        #region IEnumerable Members
        public IEnumerator<Node<T>> GetEnumerator()
        {
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (First == null || Last == null) return false; // empty list return
            if (_currentPointer == -1) //_currentPoint in initial position
            {
                _currentPointer = 0;
                this.Current = this.First;
                return true;
            }
            _currentPointer++;
            (bool doesExist, Node<T>? foundNode) = nodeExistsByIndex(_currentPointer);
            if (foundNode is not null)
            {
                this.Current = foundNode;
            }

            return doesExist;
        }

        private (bool, Node<T>?) nodeExistsByIndex(int index)
        {
            var arg = new NodeEventsArgs<T>()
            {
                TypeOfCommand = NodeCommandType.NodeSearchByIndex,
                Target = new Node<T>() { Index = index }
            };
            OnCommand?.Invoke(this, arg);
            return (arg.searchByIndexResult is not null, arg.searchByIndexResult);
        }

        public void Reset()
        {
            _currentPointer = -1;
            this.Current = null;
        }


        #endregion
        #region ICollection Members
        public void Add(Node<T> newNode)
        {
            if (newNode.Owner is null || newNode.Owner != this) newNode.Owner = this;

            OnCommand?.Invoke(this, new NodeEventsArgs<T> { TypeOfCommand = NodeCommandType.NodeAdded, Target = newNode });
            this._refreshCount = true;
        }

        public void Clear()
        {
            this.First = null;
            this.Last = null;
            this._currentPointer = -1;
        }
        //Team1

        public bool Contains(Node<T> item)
        {
            NodeEventsArgs<T> arg = SearchByItem(item);
            return (arg.searchByValueResult is not null);
        }

        private NodeEventsArgs<T> SearchByItem(Node<T> item)
        {
            var arg = new NodeEventsArgs<T>()
            {
                TypeOfCommand = NodeCommandType.NodeSearchByValue,
                Target = new Node<T>() { Value = item.Value }
            };
            OnCommand?.Invoke(this, arg);
            return arg;
        }

        public bool Contains(T item) => this.Contains(new Node<T>(item));
        //Team2

        public void CopyTo(Node<T>[] array, int index)
        {
            if (array == null) throw new ArgumentNullException("array is null");
            if (array.Length == 0) throw new ArgumentException("Zero length array.");
            if (array.Rank != 1) throw new ArgumentException("Can't do multi dimensional arrays");
            if (index < 0 || index > array.Length - 1) throw new ArgumentOutOfRangeException("index is less than zero or out of bound.");
            if (First == null) throw new ArgumentNullException("This is an empty List.");
            if (array.Length - index < this.Count) throw new ArgumentOutOfRangeException("there isn't enough space in your array to copy in to.");
            var arg = new NodeEventsArgs<T>()
            {
                TypeOfCommand = NodeCommandType.CopyToArray,
                DestinationArray = array,
                DestinationArrayStartingIndex = index,
            };
            OnCommand?.Invoke(this, arg);

        }
        public bool Remove(Node<T> item)
        {
            var arg = new NodeEventsArgs<T>()
            {
                TypeOfCommand = NodeCommandType.NodeRemoved,
                Target = item,
                RemoveResult = false
            };
            OnCommand?.Invoke(this, arg);
            this._refreshCount = true;
            return arg.RemoveResult;
        }
        #region IList Implementation

        public int IndexOf(Node<T> item)
        {
            NodeEventsArgs<T> arg = SearchByItem(item);
            if (arg.searchByValueResult != null) return arg.searchByValueResult[0].Index;
            else return - 1;
        }
        //public int[] IndexOfAll(Node<T> item)
        //{
        //    NodeEventsArgs<T> arg = SearchByItem(item);
        //    if (arg.searchByValueResult != null) return arg.searchByValueResult;
        //    else return -1;
        //}
        public int IndexOf(T item)
        {   
            return this.IndexOf(new Node<T>(item));
            
        }

        public void Insert(int index, Node<T> item)
        {
            item.Index = index;
            Add(item);
        }

        public void RemoveAt(int index)
        {
            this.Remove(this[index]);
        }

        #endregion
        //Team3
        #endregion


        #region Constructors
        public EventLL(T nodeValue)
        {
            Node<T> newNode = new Node<T>(nodeValue, this);

            var arg = new NodeEventsArgs<T>() { TypeOfCommand = NodeCommandType.NodeAdded, Target = newNode };
            OnCommand.Invoke(this, arg);
            _refreshCount = true;
        }
        public EventLL()
        {
        } 
        #endregion
    }

}
