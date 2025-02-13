// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

Console.WriteLine("Hello, World!");

v1Client client = new v1Client(new HttpClient());
await client.TodoitemsPOSTAsync(new Todo { Name = "Test", IsComplete=false });
