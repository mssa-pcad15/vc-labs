
namespace Part4
{
    internal class Mod2
    {
        internal static void Challenge1()
        {
            string[] values = { "12.3", "45", "ABC", "11", "DEF" };
            string message = string.Empty;
            decimal total = 0;

            foreach (string value in values)
            {
                if (decimal.TryParse(value, out decimal val))
                {
                    total += val;
                }
                else
                {
                    message += value;
                }
            }

            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"Total: {total}");
        }

        internal static void Challenge2()
        {
            int value1 = 11;
            decimal value2 = 6.2m;
            float value3 = 4.3f;

            // Your code here to set result1
            // Hint: You need to round the result to nearest integer (don't just truncate)
            int result1 = Convert.ToInt32(value1 / value2);
            Console.WriteLine($"Divide value1 by value2, display the result as an int: {result1}");

            decimal result2 = value2 / (decimal)value3;
            // Your code here to set result2
            Console.WriteLine($"Divide value2 by value3, display the result as a decimal: {result2}");

            float result3 = value3 / (float)value1;
            // Your code here to set result3
            Console.WriteLine($"Divide value3 by value1, display the result as a float: {result3}");
        }

        internal static void ExerciseDataConversion()
        {
            int first = 2;
            string second = "33";
            var result = first + second; // compiler knows its safe to convert anything to string, so var is determined to be string
            Console.WriteLine(result);


            int myInt = 3;
            Console.WriteLine($"int: {myInt}");

            decimal myDecimal = myInt;
            Console.WriteLine($"decimal: {myDecimal}");


            int myInt2 = 3;
            Console.WriteLine($"int: {myInt}");

            decimal myDecimal2 = 2.345m;
            Console.WriteLine($"decimal: {myDecimal2}");
            myInt = (int)myDecimal2;
            Console.WriteLine($"myInt: {myInt}");

            decimal myDecimal3 = 1.23456742444m;
            float myFloat = (float)myDecimal3;

            Console.WriteLine($"Decimal: {myDecimal3}");
            Console.WriteLine($"Float  : {myFloat}");

            int first2 = 500;
            int second2 = 700;
            string message = first2.ToString("X") + ","+ second2.ToString("X");
            Console.WriteLine(message);


            string first3 = "5";
            string second3 = "7";
            int sum = int.Parse(first3) + int.Parse(second3);
            Console.WriteLine(sum);

            string value1 = "5";
            string value2 = "7";
            int result2 = Convert.ToInt32(value1) * Convert.ToInt32(value2);
            Console.WriteLine(result2);

            string binString = "1100110";
            int result3 = Convert.ToInt32(binString, 2) ;
            Console.WriteLine(result3);

            //can you print 555 in binary string and Hex-Decimal
            int value = 555;
            Console.WriteLine($"Binary of {value} : {Convert.ToString(value, 2)}");
            Console.WriteLine($"Hexdecimal of {value} : {Convert.ToString(value, 16)}");

            int valueCast = (int)1.5m; // casting truncates
            Console.WriteLine(valueCast);

            int valueConvert = Convert.ToInt32(1.5m); // converting rounds up
            Console.WriteLine(valueConvert);
            
        }

        internal static void TryParseSample()
        { 
            // Older and original parse method
            try
            {
                int resultTryCatch = int.Parse("5abcd");
                Console.WriteLine($"The string is converted to {resultTryCatch}");
            }
            catch (Exception e) {
                Console.WriteLine("The string can not be converted to int.");
            }
            // newer TryParse returns bool, when return true, result contains the int
            if (int.TryParse("5dsdadsa", out int resultParsed))
            { 
                Console.WriteLine($"The string is converted to {resultParsed}" ); 
            }
            else
            {
                Console.WriteLine("The string can not be converted to int.");
            }

            // also works
            int resultParsed2 = 0;
            // newer TryParse returns bool, when return true, result contains the int
            if (int.TryParse("5dsdadsa", out resultParsed2))
            {
                Console.WriteLine($"The string is converted to {resultParsed2}");
            }
            else
            {
                Console.WriteLine("The string can not be converted to int.");
            }



        }
    }
}