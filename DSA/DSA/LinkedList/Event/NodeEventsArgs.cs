using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DSA.LinkedList.Event
{
    internal class NodeEventsArgs<T>
    {
        internal NodeCommandType TypeOfCommand;
        internal Node<T> Target;

        internal int SearchByIndex;
        internal Node<T>? searchByIndexResult;

        internal T? SearchByNodeValue;
        internal List<Node<T>>? searchByValueResult;

        internal int CountResult=0;

        internal Node<T>[]? DestinationArray;
        internal int DestinationArrayStartingIndex;

        internal T? SearchByValueReturnIndex;// replace with something
        internal Node<T>? SearchByValueReturnIndexResult;// replace with something

        public bool RemoveResult { get; internal set; }
    }
    internal enum NodeCommandType
    { 
        NodeAdded,
        NodeRemoved,
        NodeSearchByIndex,
        NodeSearchByValue,
        GetCount,
        CopyToArray,
        NodeSearchByValueReturnIndex // replace with something
    }
}