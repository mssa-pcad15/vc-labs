namespace Part4
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Value Type Variables
            //Mod1.IntegralTypes();//use intellisense Ctrl+. to generate Type and generate Method
            //Mod1.FloatingTypes();
            //Mod1.ValueTypeAndStack();
            //Mod1.ReferenceTypeAndHeap();
            //Mod1.Exercise();
            //SampleValueVsReference();
            //Mod2.ExerciseDataConversion();
            //Mod2.TryParseSample();
            //Mod2.Challenge1();
            Mod2.Challenge2();

        }

        private static void SampleValueVsReference()
        {
            int a = 0;
            int b = a;
            a += 5;

            Console.WriteLine($"a: {a}, b:{b}");

            // Reference Type Variables
            int[] c = [0, 1];
            int[] d = c;
            c[0] = +5;

            Console.WriteLine($"c: {c[0]}, d:{d[0]}");
        }
    }
}
