using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LearnAzFunction
{
    public class MyTableData : Azure.Data.Tables.ITableEntity
    {
        public string Text { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
    public class TableOutputFunction
    {
        private readonly ILogger<TableOutputFunction> _logger;

        public TableOutputFunction(ILogger<TableOutputFunction> logger)
        {
            _logger = logger;
        }

        [Function("TableOutputFunction")]
        [TableOutput("OutputTable", Connection = "AzureWebJobsStorage")]
        public MyTableData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {//how do I make use of data that is posted? ie. what's in HttpRequest req?
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            if (req.ContentType== "plain/text")
            {
                var sr= new StreamReader(req.BodyReader.AsStream());
                return new MyTableData()
                {
                    PartitionKey = "victor",
                    RowKey = Guid.NewGuid().ToString(),
                    Text = $"{sr.ReadLine()}"
                };
            }
            else
            {
                return new MyTableData()
                {
                    PartitionKey = "victor",
                    RowKey = Guid.NewGuid().ToString(),
                    Text = "No data"
                };
            }
        }
    }
}

