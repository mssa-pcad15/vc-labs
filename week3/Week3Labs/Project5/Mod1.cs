
using System;

namespace Project5
{
    internal class Mod1
    {
        internal static void Challenge()
        {
            Random random = new Random();
            int luck = random.Next(100);

            string[] text = { "You have much to", "Today is a day to", "Whatever work you do", "This is an ideal time to" };
            string[] good = { "look forward to.", "try new things!", "is likely to succeed.", "accomplish your dreams!" };
            string[] bad = { "fear.", "avoid major decisions.", "may have unexpected outcomes.", "re-evaluate your life." };
            string[] neutral = { "appreciate.", "enjoy time with friends.", "should align with your values.", "get in tune with nature." };

            PrintFortune();
            
            void PrintFortune()
            {
                Console.WriteLine("A fortune teller whispers the following words:");
                string[] fortune = (luck > 75 ? good : (luck < 25 ? bad : neutral));
                Console.Write($"{text[luck % 4]} {fortune[luck % 4]}\n");
            }
        }

        internal static void Exercise1()
        {
            Console.WriteLine("Calling DisplayRandomNumber:");
            DisplayRandomNumber();
            static void DisplayRandomNumber()
            {
                Random r = new Random();

                for (int i = 0; i < 5; i++)
                {
                    Console.Write($"{r.Next(1, 100)} ");
                }

                Console.WriteLine();
            }
        }

        internal static void Exercise2()
        {


            int[] times = { 800, 1200, 1600, 2000 };
            int diff = 0;

            Console.WriteLine("Enter current GMT");
            int currentGMT = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Current Medicine Schedule:");

            /* Format and display medicine times */
            FormatAndPrintTimes(times);

            Console.WriteLine();

            Console.WriteLine("Enter new GMT");
            int newGMT = Convert.ToInt32(Console.ReadLine());

            if (Math.Abs(newGMT) > 12 || Math.Abs(currentGMT) > 12)
            {
                Console.WriteLine("Invalid GMT");
            }
            else if (newGMT <= 0 && currentGMT <= 0 || newGMT >= 0 && currentGMT >= 0)
            {
                diff = 100 * (Math.Abs(newGMT) - Math.Abs(currentGMT));

                /* Adjust the times by adding the difference, keeping the value within 24 hours */
                string s = AdjustTime2(5);
            }
            else
            {
                diff = 100 * (Math.Abs(newGMT) + Math.Abs(currentGMT));

                /* Adjust the times by adding the difference, keeping the value within 24 hours */
                _ = AdjustTime2(10);
            }

            Console.WriteLine("New Medicine Schedule:");

            /* Format and display medicine times */
            FormatAndPrintTimes(times);

            Console.WriteLine();

            //this is a Local Function, it is define withIN a method, rather than a class
            string AdjustTime2(int x)
            {
                for (int i = 0; i < times.Length; i++)
                {
                    times[i] = ((times[i] + diff)) % 2400;
                }
                return "hello" + x;
            }
            static void FormatAndPrintTimes(int[] times)
            {
                foreach (int val in times)
                {
                    string time = val.ToString();
                    int len = time.Length;

                    if (len >= 3)
                    {
                        time = time.Insert(len - 2, ":");
                    }
                    else if (len == 2)
                    {
                        time = time.Insert(0, "0:");
                    }
                    else
                    {
                        time = time.Insert(0, "0:0");
                    }

                    Console.Write($"{time} ");
                }
            }


        }

        internal static void Exercise3()
        {
            /*
                if ipAddress consists of 4 numbers
                and
                if each ipAddress number has no leading zeroes
                and
                if each ipAddress number is in range 0 - 255

                then ipAddress is valid

                else ipAddress is invalid
            */
            //string ipv4Input = "107.31.1.5";
            string[] ipv4Inputs = { "107.31.1.5", "255.0.0.255", "555..0.555", "255...255" };

            foreach (string ipv4Input in ipv4Inputs)
            {
                bool validLength = false;
                bool validZeroes = false;
                bool validRange = false;

                ValidateLength();
                ValidateZeroes();
                ValidateRange();


                if (validLength && validZeroes && validRange)
                {
                    Console.WriteLine($"ip {ipv4Input} is a valid IPv4 address");
                }
                else
                {
                    Console.WriteLine($"ip {ipv4Input} is an invalid IPv4 address");
                }

                void ValidateLength()
                {
                    if (ipv4Input.Split('.').Length == 4)
                    {
                        validLength = true;
                    }
                    else
                    {
                        validLength = false;
                    }
                }

                void ValidateZeroes()
                {
                    foreach (string octet in ipv4Input.Split('.')) //loop through 107 , 31 , 1 , 5
                    {
                        if (octet.Length > 1 && octet.StartsWith("0"))
                        {
                            validZeroes = false;
                            return;
                        }
                        else
                        {
                            validZeroes = true;
                        }
                    }
                }

                void ValidateRange()
                {
                    foreach (string octet in ipv4Input.Split('.')) //loop through 107 , 31 , 1 , 5
                    {
                        if (int.TryParse(octet, out int value) && value >= 0 && value <= 255)
                        {
                            validRange = true;
                        }
                        else
                        {
                            validRange = false;
                            return;
                        }
                    }
                }
            }
        }
    }

}

