

namespace Project5
{
    internal class Mod3
    {
        internal static void ChallengeDiceGame()
        {
            Random random = new Random();
            Console.WriteLine("Would you like to play? (Y/N)");
            while (ShouldPlay())
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
            bool ShouldPlay()
            {
            
                return Console.ReadLine()?.ToLower() == "y";
            }
            string WinOrLose(int roll, int target)
            {
                return roll > target? "You win!": "You lose!";
            }
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