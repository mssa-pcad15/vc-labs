
using System.Windows.Markup;

namespace Part3
{
    internal class Mod5
    {
        internal static void Challenge()
        {
            /*
             * Here are the rules for the battle game that you need to implement in your code project:

                You must use either the do-while statement or the while statement as an outer game loop.
                The hero and the monster start with 10 health points.
                All attacks are a value between 1 and 10.
                The hero attacks first.
                Print the amount of health the monster lost and their remaining health.
                If the monster's health is greater than 0, it can attack the hero.
                Print the amount of health the hero lost and their remaining health.
                Continue this sequence of attacking until either the monster's health or hero's health is zero or less.
                Print the winner.
            */
            int[] winStat = [0, 0];
            for (int i = 0; i < 100; i++)
            {
                int heroHealth = 10;
                int monsterHealth = 10;
                string winner = string.Empty;
                Random random = new();
                do
                {
                    //hero attacks
                    int damage = random.Next(1, 11);
                    monsterHealth -= damage;
                    Console.Write($"Hero attacks! Inflicts {damage} damage.");
                    if (monsterHealth <= 0)
                    {
                        Console.WriteLine($"Monster is unalived!");
                        winner = "Hero";
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Monster has {monsterHealth} HP left.");
                    }

                    //monster attacks
                    damage = random.Next(1, 11);
                    heroHealth -= damage;
                    Console.Write($"Monster attacks! Inflicts {damage} damage. ");
                    if (heroHealth <= 0)
                    {
                        Console.WriteLine($"Hero is unalived!");
                        winner = "Monster";
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Hero has {heroHealth} HP left.");
                    }
                } while (winner == string.Empty);
                Console.WriteLine($"Game over, {winner} won!");
                if (winner == "Hero") winStat[0]++;
                else winStat[1]++;
            }
            Console.WriteLine($"After 100 battles, hero {winStat[0]} vs monster {winStat[1]}");
        }

        internal static void Challenge2()
        {
            string? readResult;
            
            bool validEntry = false;
            int numericValue = 0;


            Console.WriteLine("Enter a string containing at least three characters:");
            do
            {
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    if (readResult.Length >= 3 && int.TryParse(readResult, out numericValue))
                    {
                        validEntry = true;
                    }
                    else
                    {
                        Console.WriteLine("Your input is invalid, please try again.");
                    }
                }
            } while (validEntry == false);


            Console.WriteLine($"your number times 2 is {numericValue*2}");


        }

        internal static void Challenge3()
        {
            string[] myStrings = new string[2] { "I like pizza. I like roast chicken. I like salad", "I like all three of the menu choices" };
            foreach (string str in myStrings)
            {
                foreach (string str2 in str.Split('.'))
                { Console.WriteLine(str2.Trim()); }
            }
        }

        internal static void DoWhile()
        {
            //generate a random number and stop at lucky 7
            Random random = new Random();
            int current = 0;

            do
            {
                current = random.Next(1, 11);
                Console.WriteLine(current);
            } while (current != 7);

            Console.WriteLine("----------------------");
            //generate a random number and stop at lucky 7 using while
            current = 0;
            while (current != 7)
            {
                current = random.Next(1, 11);
                Console.WriteLine(current);
            }

            Console.WriteLine("----------------------");
            //use continue to skip to next loop iteration
            do
            {
                current = random.Next(1, 11);

                if (current >= 8) continue;

                Console.WriteLine(current);
            } while (current != 7);

        }
    }
}