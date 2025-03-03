using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Spectre.Console;

internal class Program
{
  
    private static string databaseId = "az204Databasevc";
    private static string containerId = "az204Containervc";
    static CosmosClient client = new CosmosClient("https://az204cosmosels.documents.azure.com:443/", "nPYo0W1i826iFwEHJgcKeQsMaz8YzY50AY4w1kEjL1vbPIu4g64cZUEXLX8ujS7Kxr5YvFqMiXkdACDbZlDxLg==");
    private static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option:")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "Create a database", "Create a container", "Delete a container", "Exit"
                    }));

            switch (selection)
            {
                case "Create a database":
                    createDB().Wait();
                    break;
                case "Create a container":
                    createContainer().Wait();
                    break;
                case "Delete a container":
                    deleteContainer().Wait();
                    break;
                case "Exit":
                    exit = true;
                    break;
            }
        }

        async Task deleteContainer()
        {
            Database db = client.GetDatabase(databaseId);
            ContainerResponse response = await db.GetContainer(containerId).DeleteContainerAsync();
            Console.WriteLine("Deleted Container: {0}\n", response.Container.Id);
        }

        async Task createContainer()
        {
            Database db = client.GetDatabase(databaseId);
            ContainerResponse response = await db.CreateContainerIfNotExistsAsync(containerId, "/LastName");
            Console.WriteLine("Created Container: {0}\n", response.Container.Id);
        }

        async Task createDB()
        {
            DatabaseResponse response = await client.CreateDatabaseIfNotExistsAsync(databaseId);
            Console.WriteLine("Created Database: {0}\n",response.Database.Id);
  
        }
    }
}