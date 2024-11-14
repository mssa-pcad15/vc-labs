using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//int x = 10; can not define variable outside of namespace
namespace Part3
{
    //int y = 10; no variable can be stored in namespace
    // namespace is reserved only for Types
    // class
    // struct
    // delegate
    // interface
    // enum
    internal class Mod1
    {
        //int z = 10; //this is called field 
        
        internal static void Challenge()
        {
            //int l=10; //this is called Local variable, local to the method
            int a = 10;
            int head=0, tail=0;
            for (int i = 0; i < 10; i++)
            {
                //int j=10; //this is called Local variable, local to the For Code Block
                int random = (new Random()).Next(0, 2);
                Console.WriteLine($"{((random == 0) ? $"{++head} Heads" : $"{++tail} Tails")}");
                if (1 == 1) {
                    //int k=10; //this is called Local variable, local to the if Code Blob
                }
            }
        }

        internal static void Challenge2()
        {
            string permission = "Admin";
            int level = 100;

            //If the user is an Admin with a level greater than 55, output the message
            if (permission.Contains("Admin") && level > 55)
            { 
                Console.WriteLine("Welcome, Super Admin user.");
                return;
            }

            //the user is an Admin with a level less than or equal to 55, output the message:
            if (permission.Contains("Admin") && level <= 55) 
            {
                Console.WriteLine("Welcome, Admin user.");
                return;
            }

            //If the user is a Manager with a level 20 or greater, output the message:
            if (permission.Contains("Manager") && level >= 20)
            {
                Console.WriteLine("Contact an Admin for access.");
                return;
            }

            //If the user is a Manager with a level less than 20, output the message:
            if (permission.Contains("Manager") && level < 20)
            {
                Console.WriteLine("You do not have sufficient privileges.");
                return;
            }


            //If the user is not an Admin or a Manager, output the message:
            if (!permission.Contains("Admin") && !permission.Contains("Manager"))
            { 
              Console.WriteLine("You do not have sufficient privileges.");
              return;
            }
        }

        internal static void Unit3()
        {

            int x = 8;

            string s = (x > 10) ? "greater than 10" : "less than or equal to 10";

            Console.WriteLine(s);

            for (int i = 0; i < 10; i++)
            {
                string msg = (i % 2 == 0) ? $"{i} even" : $"{i} odd";
                Console.WriteLine(msg);
            }

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{((i % 7 == 0 && i % 5 == 0) ? $"{i} multiple of 7 and 5" : $"{i} is not")}");
            }

            int saleAmount = 1001;
            int discount = saleAmount > 1000 ? 100 : 50;
            Console.WriteLine($"Discount: {discount}");

        }

        internal static void Unit2()
        {

            Console.WriteLine("a" == "a");
            Console.WriteLine("a" == "A");
            Console.WriteLine(1 == 2);

            string myValue = "a";
            Console.WriteLine(myValue == "a");

            string value1 = " a";
            string value2 = "A ";

            value1.EndsWith("a"); //true
            value1.IndexOf("a"); // 1
            string newValue1 = value1.Insert(1, "wxyz");
            Console.WriteLine($"{newValue1} - {value1} "); // value 1 did not mutate
            Console.WriteLine(value1.Trim());
            Console.WriteLine(value1.ToUpper());
            Console.WriteLine(value1); //string immutable


            value1 = value1 + "hello";
            string value3 = value1;
            Console.WriteLine($"value 3:{value3}"); //
            Console.WriteLine($"value 1:{value1}");

            int a = 5;
            int b = 5;
            Console.WriteLine(a == b);

            Object o1 = new Object();
            Object o2 = new Object();
            Console.WriteLine(o1 == o2);

            string s1 = "Hello";
            string s2 = "Hello";
            Console.WriteLine(s1 == s2);

            for (int i = 0; i < 10; i++)
            {
                s1 += i;
            }
            Console.WriteLine(s1);


            Console.WriteLine("a" != "a");//false
            Console.WriteLine("a" != "A"); //true
            Console.WriteLine(1 != 2); //true

            string myValue2 = "a";
            Console.WriteLine(myValue2 != "a");//false

            Console.WriteLine(1 > 2); //false
            Console.WriteLine(1 < 2); //true
            Console.WriteLine(1 >= 1); //true
            Console.WriteLine(1 <= 1); //true

            string pangram = "The quick brown fox jumps over the lazy dog.";
            Console.WriteLine(pangram.Contains("fox")); //true
            Console.WriteLine(pangram.Contains("cow")); //false

            Console.WriteLine(pangram.Contains("fox") == false); //false
            Console.WriteLine(!pangram.Contains("fox")); //false


            int x = 7;
            int y = 6;
            Console.WriteLine(x != y); // output: True

            string ss1 = "Hello";
            string ss2 = "Hello";
            Console.WriteLine(ss1 != ss2); // output: False

        }

    }

  
}
