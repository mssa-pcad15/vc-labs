using System.Diagnostics;

namespace DSA.LinkedList.Event
{
    internal class NodeEventsArgs<T>
    {
        internal NodeCommandType TypeOfCommand;
        internal Node<T> Target;

        internal int SearchByIndex;
        internal Node<T>? searchByIndexResult;

        internal T SearchByNodeValue;
        internal List<Node<T>>? searchByValueResult;
    }
    internal enum NodeCommandType
    { 
        NodeAdded,
        NodeRemoved,
        NodeSearchByIndex,
        NodeSearchByValue
    }
}