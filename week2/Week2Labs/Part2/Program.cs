using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;

namespace Part2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // DemoCode();
            //CallMethodChallenge5();
            //Exercise If and Else
            //DoubleTripleDiceRoll();

            Random random = new Random();
            int daysUntilExpiration = random.Next(12);
            int discountPercentage = 0;

            if (daysUntilExpiration <= 10) //Rule 2: If the user's subscription will expire in 10 days or less, display the message:
            {
                if (daysUntilExpiration <= 5)
                {
                    if (daysUntilExpiration == 1)//
                    {
                        discountPercentage = 20;
                        Console.WriteLine($"Your subscription expires within a day!\r\nRenew now and save {discountPercentage}%!");
                    }
                    else
                    if (daysUntilExpiration == 0)//Rule 5: If the user's subscription has expired, display the message:
                    {
                        Console.WriteLine("Your subscription has expired.");
                    }
                    else
                    {
                        //Rule 3: If the user's subscription will expire in five days or less, display the messages:
                        discountPercentage = 10;
                        Console.WriteLine($"Your subscription expires in {daysUntilExpiration} days.\r\nRenew now and save {discountPercentage}%!");
                    }
                }
                else
                {
                    Console.WriteLine("Your subscription will expire soon. Renew now!");
                }
            }
            Debug.Print($"{daysUntilExpiration} days to expiration.");
            // Your code goes here

        }

        private static void DoubleTripleDiceRoll()
        {
            Random dice = new Random();
            bool isTriple = false;
            do
            {
                int roll1 = dice.Next(1, 7);
                int roll2 = dice.Next(1, 7);
                int roll3 = dice.Next(1, 7);
                int total = roll1 + roll2 + roll3;
                Console.WriteLine($"Dice roll: {roll1} + {roll2} + {roll3} = {total}");

                //isTriple = (roll1 == roll2 && roll2 == roll3);
                //if (isTriple)
                //{ //is it a triple? 
                //    Console.WriteLine("You rolled a triple, +6!");//yes!
                //    total += 6;
                //}
                //else if ((roll1 == roll2) || (roll2 == roll3) || (roll3 == roll1))//its NOT a triple, but is it a Double?
                //{
                //    Console.WriteLine("You rolled a double, +2!"); // yes it is a double
                //    total += 2;
                //}
                isTriple = (roll1 == roll2 && roll2 == roll3);
                if (isTriple)
                { //its a double
                    if (roll1 == roll2 && roll2 == roll3)//its also a triple
                    {
                        Console.WriteLine("You rolled a triple, +6!");
                        total += 6;
                    }
                    else//its just double
                    {
                        Console.WriteLine("You rolled a double, +2!");
                        total += 2;
                    }
                }

                if (total >= 16)
                {
                    Console.WriteLine("You win a new car!");
                }
                else if (total >= 10)
                {
                    Console.WriteLine("You win a new laptop!");
                }
                else if (total == 7)
                {
                    Console.WriteLine("You win a trip for two!");
                }
                else
                {
                    Console.WriteLine("You win a kitten!");
                }


            } while (!isTriple);
        }

        private static void CallMethodChallenge5()
        {
            int firstValue = 500;
            int secondValue = 600;
            int largerValue = Math.Max(firstValue, secondValue);

            Console.WriteLine(largerValue);
        }

        private static void DemoCode()
        {
            Random dice1, dice2;//declare a variable named dice, its a type of Random 
            dice1 = new();
            dice2 = new Random(); // instantiate a Random instance and use dice to refer to it.
            int roll1 = dice1.Next(1, 7);
            int roll2 = dice2.Next(1, 7);

            // ask dice to perform Next method
            // A 32-bit signed integer greater than or equal to 1 and less than 7; that is, the range of return values includes
            // 1 but not 7.
            // If min equals max, min is returned.
            Console.WriteLine();
            Console.WriteLine($"{roll1} , {roll2}");




            string myComputerName = Environment.GetEnvironmentVariable("COMPUTERNAME");

            Console.WriteLine(myComputerName);

            DirectoryInfo windowsFolder = new DirectoryInfo(@"C:\Windows");
            DirectoryInfo programFolder = new DirectoryInfo(@"C:\Program Files");
            DirectoryInfo userFolder = new DirectoryInfo(@"C:\users");

            Console.WriteLine($"{windowsFolder.CreationTime} {windowsFolder.Name} {windowsFolder.Parent}");
            Console.WriteLine($"{programFolder.CreationTime} {programFolder.Name} {programFolder.Parent}");
            Console.WriteLine($"{userFolder.CreationTime} {userFolder.Name} {userFolder.Parent}");
        }
    }
}
