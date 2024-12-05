using Spectre.Console;

namespace SpectraDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var calendar = new Calendar(2020, 10);
            AnsiConsole.Write(calendar);

            var name = AnsiConsole.Prompt(
                new TextPrompt<string>("What's your name?"));

            var age = AnsiConsole.Prompt(
                new TextPrompt<int>("What's your age?"));
            var fruit = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What's your [green]favorite fruit[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] {
                        "Apple", "Apricot", "Avocado",
                        "Banana", "Blackcurrant", "Blueberry",
                        "Cherry", "Cloudberry", "Cocunut",
                    }));

        }
    }
}
