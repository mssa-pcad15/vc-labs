using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using Newtonsoft.Json;


internal class Program
{
    private static readonly string DatabaseId = "samples";
    private static readonly string ContainerId = "serversidejs-samples";
    private static void Main(string[] args)
    {
        var client= new CosmosClient("https://az204cosmosels.documents.azure.com:443/", "nPYo0W1i826iFwEHJgcKeQsMaz8YzY50AY4w1kEjL1vbPIu4g64cZUEXLX8ujS7Kxr5YvFqMiXkdACDbZlDxLg==");
        RunDemoAsync(client).Wait();
    }

    private static async Task RunDemoAsync(
           CosmosClient client)
    {
        Database database = await client.CreateDatabaseIfNotExistsAsync(DatabaseId);

        ContainerProperties containerSettings = new ContainerProperties(ContainerId, "/LastName");

        // Delete the existing container to prevent create item conflicts
        using (await database.GetContainer(ContainerId).DeleteContainerStreamAsync())
        { }

        // Create with a throughput of 1000 RU/s
        Container container = await database.CreateContainerIfNotExistsAsync(
            containerSettings);

        //Run a simple script
        //await Program.RunSimpleScript(container);
        await Program.RunAddTimestampScript(container);
        // Run Bulk Import
        // await Program.RunBulkImport(container);

        // Run OrderBy
        //await Program.RunOrderBy(container);

        //// Uncomment to Cleanup
        //await database.DeleteAsync();
    }
    private static async Task RunSimpleScript(Container container)
    {
        // 1. Create stored procedure for script.
        string scriptFileName = @"js\SimpleScript.js";
        string scriptId = Path.GetFileNameWithoutExtension(scriptFileName);

        await TryDeleteStoredProcedure(container, scriptId);
        Scripts cosmosScripts = container.Scripts;
        StoredProcedureResponse sproc = await cosmosScripts.CreateStoredProcedureAsync(
            new StoredProcedureProperties(
                scriptId,
                File.ReadAllText(scriptFileName)));

        // 2. Create a document.
        SampleDocument doc = new SampleDocument
        {
            Id = Guid.NewGuid().ToString(),
            LastName = "Estel",
            Headquarters = "Russia",
            Locations = new Location[] { new Location { Country = "Russia", City = "Novosibirsk" } },
            Income = 50000
        };

        ItemResponse<SampleDocument> created = await container.CreateItemAsync(doc, new PartitionKey(doc.LastName));

        // 3. Run the script. Pass "Hello, " as parameter. 
        // The script will take the 1st document and echo: Hello, <document as json>.
        StoredProcedureExecuteResponse<string> response = await container.Scripts.ExecuteStoredProcedureAsync<string>(
            scriptId,
            new PartitionKey(doc.LastName),
            new dynamic[] { "Hello" });

        Console.WriteLine("Result from script: {0}\r\n", response.Resource);

        await container.DeleteItemAsync<SampleDocument>(doc.Id, new PartitionKey(doc.LastName));
    }

    private static async Task RunAddTimestampScript(Container container)
    {
        // 1. Create stored procedure for addTimestamp script.
        string scriptFileName = @"js\AddTimestamp.js";
        string scriptId = Path.GetFileNameWithoutExtension(scriptFileName);

        await TryDeleteStoredProcedure(container, scriptId);
        Scripts cosmosScripts = container.Scripts;
        StoredProcedureResponse sproc = await cosmosScripts.CreateStoredProcedureAsync(
            new StoredProcedureProperties(
                scriptId,
                File.ReadAllText(scriptFileName)));

        // 2. Create a document without a timestamp.
        SampleDocument doc = new SampleDocument
        {
            Id = Guid.NewGuid().ToString(),
            LastName = "Estel",
            Headquarters = "Russia",
            Locations = new Location[] { new Location { Country = "Russia", City = "Novosibirsk" } },
            Income = 50000
        };

        // 3. Run the addTimestamp script to add a timestamp to the document and insert it.
        StoredProcedureExecuteResponse<SampleDocument> response = await container.Scripts.ExecuteStoredProcedureAsync<SampleDocument>(
            scriptId,
            new PartitionKey(doc.LastName),
            new dynamic[] { doc });

        // 4. Output the result.
        SampleDocument createdDoc = response.Resource;
        Console.WriteLine("Document created with timestamp: {0}\r\n", JsonConvert.SerializeObject(createdDoc));

        // 5. Clean up by deleting the created document.
        await container.DeleteItemAsync<SampleDocument>(createdDoc.Id, new PartitionKey(createdDoc.LastName));
    }


    private static async Task TryDeleteStoredProcedure(Container container, string sprocId)
    {
        Scripts cosmosScripts = container.Scripts;

        try
        {
            StoredProcedureResponse sproc = await cosmosScripts.ReadStoredProcedureAsync(sprocId);
            await cosmosScripts.DeleteStoredProcedureAsync(sprocId);
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            //Nothing to delete
        }
    }

    public class SampleDocument
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }


        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; set; }
        
        
        public string LastName { get; set; }
        public Location[] Locations { get; set; }
        public string Headquarters { get; set; }
        public int Income { get; set; }
    }
    public class Location
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
    // </RunSimpleScript>
}
