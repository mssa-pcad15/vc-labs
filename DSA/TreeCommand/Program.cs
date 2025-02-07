// See https://aka.ms/new-console-template for more information
string startingFolder = args.Length==0?Environment.CurrentDirectory: args[0];


PrintTree(startingFolder, 0);

void PrintTree(string path, int level) {

    foreach (var childFolder in Directory.GetDirectories(path))
    {
        Console.WriteLine($" {new string('-', level)} Directory - [{childFolder}]");
        PrintTree(childFolder, level + 1);
        foreach (var fileName in Directory.GetFiles(path))
        {
            Console.WriteLine(new string('*', level) + fileName);
        }
    }

   
}
