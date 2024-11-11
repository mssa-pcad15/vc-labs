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
            Random dice1, dice2;//declare a variable named dice, its a type of Random 
            dice1 = new Random();
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
