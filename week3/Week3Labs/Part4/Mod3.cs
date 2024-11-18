
using System.ComponentModel;

namespace Part4
{
    internal class Mod3
    {
        internal static void ArrayMethods()
        {
            //var whatAmI = System.Array.CreateInstance(typeof(int),5);
            int[] arrayVar = [7,5,8,11,3,4,5,6,7,8,9];
            
            foreach (int i in arrayVar[2..7])//8,11,3,4,5
            {
                Console.Write(i + ",");
            }
            Console.WriteLine();

            foreach (int i in arrayVar[5..]) //4,5,6,7,8,9,
            {
                Console.Write(i + ",");
            }
            Console.WriteLine();

            foreach (int i in arrayVar[..4]) //7,5,8,11,
            {
                Console.Write(i+",");
            }
            Console.WriteLine();

            foreach (int i in arrayVar[..^0]) //7,5,8,11,3,4,5,6,7,8,9
            {
                Console.Write(i + ",");
            }
            Console.WriteLine();

            foreach (int i in arrayVar[..^1]) //7,5,8,11,3,4,5,6,7,8,
            {
                Console.Write(i + ",");
            }
            Console.WriteLine();

            foreach (int i in arrayVar[..^2]) //7,5,8,11,3,4,5,6,7,
            {
                Console.Write(i + ",");
            }
            Console.WriteLine();
            //from is inclusive, to is exclusive
            foreach (int i in arrayVar[^2..]) //8,9,
            {
                Console.Write(i + ",");
            }
            Console.WriteLine();

            int[] subArray = arrayVar[3..6]; //return a NEW ARRAY - NOT A reference
            int[] arrayVarDeepCopy = arrayVar[..]; //for value type array like int,double,float,decimal etc.
            int[] arrayVarShallowCopy = arrayVar;
            //  array[from..to] is called array slicer, it produce a new array

            Array.Sort(arrayVar);

            foreach (int i in arrayVar) {
                Console.Write($"{i},");
            }
            Console.WriteLine();

            Array.Reverse(arrayVar);

            foreach (int i in arrayVar)
            {
                Console.Write($"{i},");
            }
            Console.WriteLine();


            string[] pallets = ["B14", "A11", "B12", "A13"];

            Console.WriteLine("Sorted...");
            Array.Sort(pallets);
            foreach (var pallet in pallets)
            {
                Console.WriteLine($"-- {pallet}");
            }


            Console.WriteLine("");
            Console.WriteLine("Reversed...");
            Array.Reverse(pallets);
            foreach (var pallet in pallets)
            {
                Console.WriteLine($"-- {pallet}");
            }
         
        }

        internal static void ClearAndResize()
        {
            string[] pallets = ["B14", "A11", "B12", "A13"]; //4 element
            Console.WriteLine("");

            Console.WriteLine($"Before: {pallets[0]}");
            Array.Clear(pallets, 0, 2);
            Console.WriteLine($"After: {pallets[0]}");//string interpolation handler will treat null as empty string
                                                      //Console.WriteLine($"After: {pallets[0].ToLower()}"); this throws null reference exception
            Console.WriteLine($"After: {pallets[0]?.ToLower()}");//pallets[0] is null so empty string
            Console.WriteLine($"After: {pallets[3]?.ToLower()}");//A13=>a13
            //?. test if pallets[0] is null, if not then invoke ToLower() on it. If it IS null, then return null
            Console.WriteLine($"Clearing 2 ... count: {pallets.Length}"); // should still have 4 elements
            foreach (var pallet in pallets)
            {
                Console.WriteLine($"-- {pallet}");
            }

            Console.WriteLine("");
            Array.Resize(ref pallets, 6);
            Console.WriteLine($"Resizing 6 ... count: {pallets.Length}");

            pallets[4] = "C01";
            pallets[5] = "C02";

            foreach (var pallet in pallets)
            {
                Console.WriteLine($"-- {pallet}");
            }

            Console.WriteLine("");
            Array.Resize(ref pallets, 3);
            Console.WriteLine($"Resizing 3 ... count: {pallets.Length}");

            foreach (var pallet in pallets)
            {
                Console.WriteLine($"-- {pallet}");
            }

            int x = 10;
            Add(ref x);
            Console.WriteLine(x);
            Console.WriteLine($"in Main x={x}");
        }

        private static void Add(ref int x)
        {
            x = x + 10;
            Console.WriteLine($"in Add x={x}" );
        }
    }
} 