using Azure;
using Azure.Data.Tables;
using System.Collections;
using System.Text.Json.Serialization;
using System.Transactions;
using System.Xml;

namespace CloudLinkedListLib
{


    public class Node<T> : ITableEntity
    {
        private Random _random = new Random();
      

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public T Content { get; set; }

      
        public string? NextPartitionKey { get; set; }
        public string? NextRowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public Node(T content)
        {
            this.PartitionKey = _random.NextInt64().ToString();
            this.RowKey = _random.NextInt64().ToString();
            Content = content;
        }
        public Node()
        {
            
        }

        public Node<T>? GetNextNode(TableClient client)=> client.GetEntity<Node<T>>(NextPartitionKey!, NextRowKey!).Value;
        
    }
  
    public class CloudLinkedList<T>:IEnumerable<Node<T>>,IEnumerator<Node<T>>,ICollection<Node<T>>
    {
        private readonly TableClient tableClient;
        public Node<T>? First=default;
        public Node<T>? Last=default;
        public Node<T>? _current = default;
        private int _position = -1;
        private Random _random=new Random();
      
        public Node<T> Current => _current;

        object IEnumerator.Current => Current;

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public CloudLinkedList(TableClient tableClient,T? content=default):this(tableClient)
        {
            if (content is null) return;
            var newNode = new Node<T>(content);
            tableClient.AddEntity<Node<T>>(newNode);
            Count++;
            this.First = newNode;
            this.Last = newNode;
        }
        public CloudLinkedList(TableClient tableClient)
        {
            this.tableClient = tableClient;
        }

        public void Add(Node<T> n)
        {
            if (this.First == null) {
                tableClient.AddEntity<Node<T>>(n);
                Count++;
                this.First = n;
                this.Last = n;
                return;
            }
            tableClient.AddEntity<Node<T>>(n);
            Count++;
            this.Last.NextPartitionKey = n.PartitionKey;
            this.Last.NextRowKey = n.RowKey;
            tableClient.UpsertEntity<Node<T>>(this.Last);
            this.Last = n;
           

        }

        public IEnumerator<Node<T>> GetEnumerator() => this;
        
        IEnumerator IEnumerable.GetEnumerator() => this;
       

        public bool MoveNext()
        {
            if (this.First is null) return false;
          

            if (_position < 0 && this.First is not null) {
                _position = 0;
                this._current = this.First;
                return true;
            }

            if (this._current.NextPartitionKey is null && this._current.NextRowKey is null) return false;

            _position += 1;
            this._current = this._current.GetNextNode(tableClient);
            return true;
        }

        public void Reset()
        {
            _position = -1;
            this._current = null;
        }

        public void Dispose()
        {
            ;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Node<T> item) { 
            var result   = tableClient.GetEntityIfExists<Node<T>>(item.PartitionKey,item.RowKey);
            return result.HasValue;
        }

        public void CopyTo(Node<T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Node<T> item)
        {
            var result = tableClient.GetEntityIfExists<Node<T>>(item.PartitionKey, item.RowKey);
            
            if (result.HasValue) 
                { 
                Node<T> nodeToRemove = result.Value!;

                string queryNodePrior = $"NextPartitionKey eq {item.PartitionKey} and NextRowKey eq {item.RowKey}"; // find the node prior
                var resultNodePrior = tableClient.Query<Node<T>>(queryNodePrior);

                if (resultNodePrior.GetEnumerator().MoveNext())
                {
                    Node<T> priorNode = resultNodePrior.GetEnumerator().Current;
                    priorNode.NextPartitionKey = nodeToRemove.NextPartitionKey;
                    priorNode.NextRowKey = nodeToRemove.NextRowKey;
                    tableClient.UpsertEntity(priorNode);
                    Count--;
                    tableClient.DeleteEntity(nodeToRemove);
                    return true;
                }
                else
                {
                    this.First = tableClient.GetEntity<Node<T>>(nodeToRemove.NextPartitionKey, nodeToRemove.NextRowKey);

                    Count--;

                    tableClient.DeleteEntity(nodeToRemove);
                    return true;
                }

            }
            else //can't find the item to remove in the table
                { return false; }


        }
    }
}
