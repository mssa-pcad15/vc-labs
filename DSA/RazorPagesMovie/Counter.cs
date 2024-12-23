using Azure;
using Azure.Data.Tables;
using System.Text.Json.Serialization;

namespace RazorPagesMovie
{
    public class Counter
    {
        public virtual int Count { get; set; } //overridable
    }

    public class DistributedCounter : Counter
    {

        public DistributedCounter(TableClient table)
        {
            //retreive a row using partition and row key - point query - fastest
            Response<CounterRow> result = table.GetEntity<CounterRow>("0", "0");

            CounterRow updatedCounter = result.Value; //result.Value is an instance of CounterRow 
            updatedCounter.Count = updatedCounter.Count + 1; //modify the updatedCounter.Count with +1

            //update that row
            table.UpsertEntity<CounterRow>(updatedCounter, TableUpdateMode.Replace);
            this.Count = updatedCounter.Count;
        }

    }

    //Step 1: Create another inherit class from Counter
    public class SingletonDistributedCounter : Counter {
        private readonly TableClient table;

        public SingletonDistributedCounter(TableClient table) //this will be used as singleton, so ctor only called once.
            // use a private readonly field to keeep reference.
        {
            this.table = table;
            //Step2: move constructor logic to Getter and Setter of Count Property
            // marking Counter.Count prop as virtual see line 9

        }

        public override int Count
        {
            get { //step 3: on Get of Count Prop, use TableClient table (see line32) to retreive the Count from Azure Table
                Response<CounterRow> result = table.GetEntity<CounterRow>("0", "0");
                return result.Value.Count;
            }
            set { //step 4: on Set of Count Prop, update Azure Table Record

                CounterRow updatedRow = new CounterRow { PartitionKey = "0", RowKey = "0", Count = value };
                table.UpsertEntity<CounterRow>(updatedRow, TableUpdateMode.Replace);
            }
        
        }

    }

    public class CounterRow : ITableEntity
    {
        public int Count { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
