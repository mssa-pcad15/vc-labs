



namespace Part4
{
    internal class Mod1
    {
        internal static void FloatingTypes()
        {
            Console.WriteLine("Here are the floating Type:");

            Console.WriteLine($"float : {float.MinValue} to {float.MaxValue}");
            Console.WriteLine($"double : {double.MinValue} to {double.MaxValue}");
            Console.WriteLine($"decimal : {decimal.MinValue} to {decimal.MaxValue}:{sizeof(decimal)} ");

        }

        internal static void IntegralTypes()
        {
            Console.WriteLine("Here are the SIGNED Integral Type:");

            Console.WriteLine($"sbyte : {sbyte.MinValue} to {sbyte.MaxValue} ");
            Console.WriteLine($"short : {short.MinValue} to {short.MaxValue} ");
            Console.WriteLine($"int : {int.MinValue} to {int.MaxValue} ");
            Console.WriteLine($"long : {long.MinValue} to {long.MaxValue}:{sizeof(long)}");


            Console.WriteLine("Here are the UNSIGNED Integral Type:");

            Console.WriteLine($"byte : {byte.MinValue} to {byte.MaxValue} ");
            Console.WriteLine($"ushort : {ushort.MinValue} to {ushort.MaxValue} ");
            Console.WriteLine($"uint : {uint.MinValue} to {uint.MaxValue} ");
            Console.WriteLine($"ulong : {ulong.MinValue} to {ulong.MaxValue} ");
            ulong l = ulong.MaxValue;

            // it can over flow at run time, if compiler static analysis does not catch it.
            Console.WriteLine(l + 5);
            
        }
        #region ValueType and How Stack works

        internal static void ValueTypeAndStack()
        {
            Console.WriteLine("This is in the method 'ValueTypeAndStack'");
            int a = 1; // method level, local variable, will be destroyed when method ends
            Console.WriteLine("Calling Method2()");

            Console.WriteLine($"'ValueTypeAndStack': a is {a}");

        }
        internal static void Method2(int p1)
        {
            Console.WriteLine("This is in the method 'Method2'");

            int a = 2; // method level, local variable, will be destroyed when method ends
            Console.WriteLine("Calling Method3()");
            Method3();
            Console.WriteLine($"'Method2': a is {a}");

        }
        internal static void Method3()
        {
            Console.WriteLine("This is in the method 'Method3'");
            int a = 3; // method level, local variable, will be destroyed when method ends
            Console.WriteLine($"'Method3': a is {a}");
        }

        #endregion


        #region Reference Type and Heap
        static int[][] holder = new int[3][]; //array of arrays that will hold 3 references to array created in methods
        // holder is static, meaning it belongs to Mod1 class and there is only 1 copy.
        internal static void ReferenceTypeAndHeap()
        {
            Console.WriteLine("In method 'ReferenceTypeAndHeap'");
            int[] a = [1, 2, 3];
            holder[0] = a; //make holder[0] reference a
            Console.WriteLine("Calling Method2Ref");
            Method2Ref();
            foreach (int i in a) Console.Write(i);
            Console.WriteLine();
        }
        internal static void Method2Ref()
        {
            Console.WriteLine("In method 'Method2Ref'");
            int[] a = [3, 4, 5];
            holder[1] = a;
            Console.WriteLine("Calling Method3Ref");
            Method3Ref();
            foreach (int i in a) Console.Write(i);
            Console.WriteLine();
        }
        internal static void Method3Ref()
        {
            Console.WriteLine("In method 'Method3Ref'");
            int[] a = [6, 7, 8];
            holder[2] = a;
            foreach (int i in a) Console.Write(i);
            Console.WriteLine();
        } 
        #endregion
    }
}