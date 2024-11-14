
namespace Part3
{
    internal class Mod4
    {
        internal static void Challenge()
        {
            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0 && i % 5 == 0) //divisible by 3 and 5
                {
                    Console.WriteLine($"{i} - FizzBuzz");
                    continue; //skip to next iteration
                }
                if (i % 3 == 0)
                {
                    Console.WriteLine($"{i} - Fizz");
                    continue;
                }
                if (i % 5 == 0)
                {
                    Console.WriteLine($"{i} - Buzz");
                    continue;
                }
                Console.WriteLine($"{i}");
            }
        }

        internal static void ForLoop()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"basic loop : {i}");
            } // what is the last i? 9
              //for ( ; ; ) {
              //    Console.WriteLine("infinite looping");
              //}
            for (int i = 10; i > 0; i--)
            {
                Console.WriteLine($"backward loop : {i}");
                // what is the last i? 1
            }

            // pre-increment and post increment of i makes no difference. Action is evaluated
            // after the closing of the "for" code block
            //for (int i = 0; i < 10; ++i)
            //{
            //    Console.WriteLine($"basic loop : {i}");
            //} // what is the last i? 9
            ////for ( ; ; ) {
            ////    Console.WriteLine("infinite looping");
            ////}
            //for (int i = 10; i > 0; --i)
            //{
            //    Console.WriteLine($"backward loop : {i}");
            //    // what is the last i? 1
            //}

            for (int i = 0; i < 10; i += 2)
            {
                Console.WriteLine($"Even loop - {i}");
            }

            // print this testArray in reverse order
            int[] testArray = [1, 2, 3, 4, 5, 6, 7, 8, 9];

            for (int i = testArray.Length - 1; i >= 0; i--)
            {
                Console.WriteLine($"reverse testArray - {testArray[i]}");
            }

            //outer for loop will print one row at time
            for (int outerIndex = 0; outerIndex < testArray.Length; outerIndex++)
            {
                //for each row print multiplication result in one row, so Console.Write, not WriteLine
                for (int innerIndex = 0; innerIndex < testArray.Length; innerIndex++)
                {
                    Console.Write($"{testArray[innerIndex] * testArray[outerIndex],3}\t");
                }

                Console.WriteLine();//switch to a new line
            }

            // write a for loop that will roll 2x 6 sided dice, report back on which roll hits 6,6
            Random random = new Random();

            for (int i = 1; i < 100; i++)
            {

                int roll1 = random.Next(1, 7);
                int roll2 = random.Next(1, 7);

                Console.WriteLine($"{roll1},{roll2}");

                if (roll1 + roll2 == 12)
                {
                    Console.WriteLine($"Hit a double 6 in {i} rolls.");
                    break;//exit the for loop here, ignore the for loop condition.
                }
            }

            //
            for (int i = 1; i < int.MaxValue; i++)
            {
                if (i % 3 == 0 && i % 5 == 0 && i % 7 == 0 && i % 9 == 0)
                {
                    Console.WriteLine($"found {i}");
                    break;
                }
            }

            // continue - advance directly to the next loop.

            for (int i = 1; i < 100; i++)
            {

                int roll1 = random.Next(1, 7);
                int roll2 = random.Next(1, 7);

                Console.WriteLine($"{roll1},{roll2}");

                if (roll1 == roll2)
                {
                    Console.WriteLine($"double {roll1}");
                    i += 20;

                    continue;
                }
                //this console.writeline is skipped when roll double
                Console.WriteLine($"After {i} rolls, you still havn't rolled a double.");
            }

            // try to update the array being looped
            //string[] names = { "Alex", "Eddie", "David", "Michael" };
            //foreach (var name in names)
            //{
            //    // Can't do this:
            //    if (name == "David") names[2] = "Sammy";//compiler stops you
            //}

            string[] names = { "Alex", "Eddie", "David", "Michael" };
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == "David") names[i] = "Sammy";
            }
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}