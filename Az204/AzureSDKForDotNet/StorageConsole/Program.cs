using Azure;
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.DataMovement;
using Azure.Storage.DataMovement.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using System.Reflection;


// Run the examples asynchronously, wait for the results before proceeding
// this will use connection string
//ProcessWithConnectionStringAsync().GetAwaiter().GetResult();
//ProcessWithAzureIdentityAsync().GetAwaiter().GetResult();
//ProcessWithMSIAsync().GetAwaiter().GetResult();
await UploadWithDataMovementAsync();

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();

static async Task ProcessWithMSIAsync() {
    // Create a BlobServiceClient that will authenticate through Managed Identity
    Uri accountUri = new Uri("https://vc0316.blob.core.windows.net/");
    BlobServiceClient client = new BlobServiceClient(accountUri, new ManagedIdentityCredential());

    string containerName = "wtblob" + Guid.NewGuid().ToString();

    // Create the container and return a container client object
    BlobContainerClient containerClient = await client.CreateBlobContainerAsync(containerName);
    Console.WriteLine("A container named '" + containerName + "' has been created. " +
        "\nTake a minute and verify in the portal." +
        "\nNext a file will be created and uploaded to the container.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();
}

static async Task ProcessWithAzureIdentityAsync()
{
    var builder = new ConfigurationBuilder().AddUserSecrets<Program>();

    var configuration = builder.Build();

    ClientSecretCredential clientCredential = new ClientSecretCredential(
        configuration["TenantId"],
        configuration["ClientId"],
        configuration["ClientSecret"]
        );
    // Create a BlobServiceClient that will authenticate through Active Directory
    Uri accountUri = new Uri("https://vc0316.blob.core.windows.net/");
    BlobServiceClient client = new BlobServiceClient(accountUri, clientCredential);

    string containerName = "wtblob" + Guid.NewGuid().ToString();

    // Create the container and return a container client object
    BlobContainerClient containerClient = await client.CreateBlobContainerAsync(containerName);
    Console.WriteLine("A container named '" + containerName + "' has been created. " +
        "\nTake a minute and verify in the portal." +
        "\nNext a file will be created and uploaded to the container.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

}


static async Task ProcessWithConnectionStringAsync()
{
    var builder = new ConfigurationBuilder()
              .AddUserSecrets<Program>();

    var configuration = builder.Build();

    string storageConnectionString = configuration["ConnectionString"];

    // Create a client that can authenticate with a connection string
    BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);

    // COPY EXAMPLE CODE BELOW HERE
    string containerName = "wtblob" + Guid.NewGuid().ToString();

    // Create the container and return a container client object
    BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
    Console.WriteLine("A container named '" + containerName + "' has been created. " +
        "\nTake a minute and verify in the portal." +
        "\nNext a file will be created and uploaded to the container.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

    // Create a local file in the ./data/ directory for uploading and downloading
    string localPath = "./data/";
    string fileName = "wtfile" + Guid.NewGuid().ToString() + ".txt";
    string localFilePath = Path.Combine(localPath, fileName);

    // Write text to the file
    await File.WriteAllTextAsync(localFilePath, "Hello, World!");

    // Get a reference to the blob
    BlobClient blobClient = containerClient.GetBlobClient(fileName);

    Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

    // Open the file and upload its data
    using (FileStream uploadFileStream = File.OpenRead(localFilePath))
    {
        await blobClient.UploadAsync(uploadFileStream);
        uploadFileStream.Close();
    }

    Console.WriteLine("\nThe file was uploaded. We'll verify by listing" +
            " the blobs next.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

    // List blobs in the container
    Console.WriteLine("Listing blobs...");
    await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
    {
        Console.WriteLine("\t" + blobItem.Name);
    }

    Console.WriteLine("\nYou can also verify by looking inside the " +
            "container in the portal." +
            "\nNext the blob will be downloaded with an altered file name.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

    // Download the blob to a local file
    // Append the string "DOWNLOADED" before the .txt extension 
    string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

    Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

    // Download the blob's contents and save it to a file
    BlobDownloadInfo download = await blobClient.DownloadAsync();

    using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
    {
        await download.Content.CopyToAsync(downloadFileStream);
    }
    Console.WriteLine("\nLocate the local file in the data directory created earlier to verify it was downloaded.");
    Console.WriteLine("The next step is to delete the container and local files.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

    Uri blobSas = blobClient.GenerateSasUri(
        BlobSasPermissions.Read | 
        BlobSasPermissions.Write | 
        BlobSasPermissions.Delete,
        DateTimeOffset.UtcNow.AddHours(1));

    Console.WriteLine("Here is a SAS url to download this file:");


    Console.WriteLine($"{blobSas.AbsoluteUri}");
    Console.ReadLine();

    BlobSasBuilder sasBuilder = new BlobSasBuilder(
         BlobSasPermissions.Read,
         DateTimeOffset.Now.AddDays(1)
        );

    sasBuilder.BlobContainerName = containerName;
    sasBuilder.BlobName = fileName;
    var sastoken = sasBuilder.ToSasQueryParameters(
        new Azure.Storage.StorageSharedKeyCredential(configuration["Account"], configuration["Key"]));
    Console.WriteLine(sastoken.ToString());
}

static async Task UploadWithDataMovementAsync()
{
    var builder = new ConfigurationBuilder()
             .AddUserSecrets<Program>();

    var configuration = builder.Build();
    string containerName = "ai900";
    
    BlobServiceClient service = new BlobServiceClient(configuration["ConnectionString"]);
    BlobContainerClient container = service.GetBlobContainerClient(containerName);

    await container.CreateIfNotExistsAsync();
    try
    {
        
        string directoryPath = @"C:\TempDir"!;

        //TransferOperation transfer = await container.UploadDirectoryAsync(WaitUntil.Completed, directoryPath);
        //TransferOperation transfer = await container.UploadDirectoryAsync(WaitUntil.Completed, directoryPath,"virtual/directory");


        BlobContainerClientTransferOptions options = new BlobContainerClientTransferOptions
        {
            BlobContainerOptions = new BlobStorageResourceContainerOptions
            {
                BlobPrefix = "virtual/directory"
            },
            TransferOptions = new TransferOptions()
            {
                CreationMode = StorageResourceCreationMode.OverwriteIfExists,
            }
        };
        TransferOperation transfer = await container.UploadDirectoryAsync(WaitUntil.Completed, directoryPath,
            options
            );
        await transfer.WaitForCompletionAsync();
        Console.WriteLine("Transfer Completed");

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}

