
namespace Part6
{
    internal class Mod2
    {
        internal static void Challenge1()
        {
            try
            {
                Process1();
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine($"An exception has occurred. {ex.InnerException.Message}");
                }
                Console.WriteLine("Exit program");
            }
            static void Process1()
            {
                try
                {
                    WriteMessage();
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"Exception caught in Process1 : {ex.Message}");
                    throw new MyException(ex);
                }

            }

            static void WriteMessage()
            {
                double float1 = 3000.0;
                double float2 = 0.0;
                int number1 = 3000;
                int number2 = 0;

                Console.WriteLine(float1 / float2);
                Console.WriteLine(number1 / number2);
            }
        }

        internal static void Challenge2()
        {
            try
            {
                checked
                {
                    int num1 = int.MaxValue;
                    int num2 = int.MaxValue;
                    int result = num1 + num2;
                    Console.WriteLine("Result: " + result);
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Error: The number is too large to be represented as an integer." + ex.Message);
            }
            try
            {
                string str = null;
                int length = str.Length;
                Console.WriteLine("String Length: " + length);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Error: The reference is null." + ex.Message);
            }

            try
            {
                int[] numbers = new int[5];
                numbers[5] = 10;
                Console.WriteLine("Number at index 5: " + numbers[5]);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Error: Index out of range." + ex.Message);
            }

            try
            {
                int num3 = 10;
                int num4 = 0;
                int result2 = num3 / num4;
                Console.WriteLine("Result: " + result2);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: Cannot divide by zero." + ex.Message);
            }
            Console.WriteLine("Exiting program.");
        }

        internal static void ExceptionSample()
        {

            int[] input = null;
            Console.WriteLine("Before");
            try
            {
                Array.Sort(input);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Array is null, we can't sort null array.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("an unknown exception has occurred.");
            }
            finally
            {
                Console.WriteLine("Finally here.");
            }


        }

        internal static void TryCatchPractice()
        {
            try
            {
                string[] names = { "Dog", "Cat", "Fish" };
                Object[] objs = (Object[])names;

                Object obj = (Object)13;
                //objs[2] = obj; // ArrayTypeMismatchException occurs

                int number1 = 3000;
                int number2 = 0;
                Console.WriteLine(number1 / number2); // DivideByZeroException occurs

                int valueEntered;
                string userValue = "two";
                valueEntered = int.Parse(userValue); // FormatException occurs

                int[] values1 = { 3, 6, 9, 12, 15, 18, 21 };
                int[] values2 = new int[6];

                values2[values1.Length - 1] = values1[values1.Length - 1]; // IndexOutOfRangeException occurs

                object obj2 = "This is a string";
                int num = (int)obj2;

                int[] values = null;
                for (int i = 0; i <= 9; i++)
                    values[i] = i * 2;


                decimal x = 400;
                byte j;

                j = (byte)x; // OverflowException occurs
                Console.WriteLine(j);
            }
            catch (ArrayTypeMismatchException ex)
            {
                Console.WriteLine($"you are trying to put a 13 into array of Object made up by strings");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"you are trying to divide by 0.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Can not parse 'two' into an int");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"values2 array has 6 elements with index 0 to 5, values1.Length is 7");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"Can not cast string into int");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Looks like you forgot to initialize the array.");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Byte has max value of 255, casting 400 to byte triggers exception.");
            }

        }
    }
}