using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.LinkedList.Event
{
    public class Node<T>
    {
        static object locker = new object();
        public int Index = -1;
        private EventLL<T>? _owner;
        internal EventLL<T>? Owner
        {
            get => this._owner; set
            {
                this._owner = value;
                AddEventHandler();
            }
        }

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
            this._owner = owner;
            AddEventHandler();
        }

        private void AddEventHandler()
        {
            if (this._owner == null) return;
            this._owner.OnCommand += this.HandleEvent!;
        }
        private void RemoveEventHandler()
        {
            if (this._owner == null) return;
            this._owner.OnCommand -= this.HandleEvent!;
        }

        private void HandleEvent(object sender, NodeEventsArgs<T> arg)
        {
            EventLL<T>? owner = sender as EventLL<T>;

            switch (arg.TypeOfCommand)
            {
                case NodeCommandType.NodeAdded:
                    if (owner.Count == 0)
                    { //if this is the first Node ever
                        owner.First = this;
                        owner.Last = this;
                        this.Index = 0;
                        return;
                    }

                    if (this == owner.Last && arg.Target.Index == -1)
                    {
                        // this is a new Node to be add to the end of the list and this is the current Last
                        this.Next = arg.Target;
                        arg.Target.Index = this.Index + 1;
                        owner.Last = arg.Target;
                        return;
                    }

                    if (arg.Target.Index == -1) return;
                    if (this == arg.Target) return;

                    //if we get here, its a node with some index to insert into.
                    //The node before the insertion point is going to responde in the following way
                    if (this.Index == (arg.Target.Index - 1)) { arg.Target.Next = this.Next; this.Next = arg.Target; return; }
                    //the node after the insertion point is goint to increment their index
                    if (this.Index >= (arg.Target.Index)) { this.Index++; return; }
                    break;
                case NodeCommandType.GetCount:
                    lock (locker)
                    {
                        arg.CountResult = Math.Max(arg.CountResult, this.Index + 1);
                    }
                    break;
                case NodeCommandType.NodeRemoved:
                    if (this == _owner.Last) return;//there is nothing for the last node to do.

                    if (this.Value.Equals(arg.Target.Value) && this == _owner.First)
                    {
                        this.RemoveEventHandler();
                        _owner.First = this.Next;
                        Node<T> node = this;
                        lock (locker)
                        {
                            while (node.Next is not null)
                            {
                                node.Next.Index--;
                                node = node.Next;
                            }
                        }
                        arg.RemoveResult = true;
                        return;
                    }

                    if (this.Next.Value.Equals(arg.Target.Value))
                    {
                        this.Next.RemoveEventHandler();
                        this.Next = this.Next.Next;
                        Node<T> node = this;
                        lock (locker)
                        {
                            while (node.Next is not null)
                            {
                                node.Next.Index--;
                                node = node.Next;
                            }
                        }
                        arg.RemoveResult = true;
                        return;
                    }

                    break;
                case NodeCommandType.CopyToArray:
                    arg.DestinationArray![arg.DestinationArrayStartingIndex + this.Index] = this;
                    break;
                case NodeCommandType.NodeSearchByIndex:
                    if (this.Index == arg.Target.Index) { arg.searchByIndexResult = this; }
                    break;
                case NodeCommandType.NodeSearchByValue:

                    if (this.Value!.Equals(arg.Target.Value))
                    {
                        lock (arg)
                        {
                            if (arg.searchByValueResult is null)
                            {
                                arg.searchByValueResult = new List<Node<T>>();
                            }
                            arg.searchByValueResult.Add(this);
                        }
                    }
                    break;
                case NodeCommandType.NodeSearchByValueReturnIndex: // replace with something

                    //if (this.Value!.Equals(arg.Target.Value)) { arg.SearchByValueReturnIndexResult = this.Index }
                    break;
                default:
                    break;
            }
        }
    }
}
