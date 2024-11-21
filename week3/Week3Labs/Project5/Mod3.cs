

namespace Project5
{
    internal class Mod3
    {
        internal static void ChallengeDiceGame()
        {
            Random random = new Random();
            Console.WriteLine("Would you like to play? (Y/N)");
            if (ShouldPlay())
            {
                PlayGame();
            }

            void PlayGame()
            {
                var play = true;

                while (play)
                {
                    var target = random.Next(1, 6);
                    var roll = random.Next(1, 7);

                    Console.WriteLine($"Roll a number greater than {target} to win!");
                    Console.WriteLine($"You rolled a {roll}");
                    Console.WriteLine(WinOrLose(roll,target));
                    Console.WriteLine("\nPlay again? (Y/N)");

                    play = ShouldPlay();
                   
                }
            }
            bool ShouldPlay() => Console.ReadLine()?.ToLower() == "y";
            
            string WinOrLose(int roll, int target) => roll > target? "You win!": "You lose!";
        }

        internal static void Parameters()
        {
            bool flag = false;
            PrintCount(flag, 500,13);
        }

        internal static void PrintCount(bool onlyEven,int upto,int badNumber) {
           
            for (int i = 0; i < upto; i++)
            {
                if (onlyEven)
                { if (int.IsEvenInteger(i) && !IsBadNumber(badNumber,i)) { Console.WriteLine(i); } }
                else
                { Console.WriteLine(i); }
            }

        }

        private static bool IsBadNumber(int badNumber, int i)
        {
            return (i % badNumber == 0);
        }

        internal static void Return()
        {
            Car myNissan = new Car();
            int i = 10;
            string myName = "Victor";
            var result = 100 * 0.5;
            Console.WriteLine(result.GetType().Name);
        }
    }
}