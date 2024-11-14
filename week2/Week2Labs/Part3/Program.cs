namespace Part3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Mod1.Unit2();
            //Mod1.Unit3();
            //Mod1.Challenge();
            //Mod1.Challenge2();
            //How do I access static variable?
            //StaticInstanceDemo();
            Mod2.VariableScope();
        }

        private static void StaticInstanceDemo()
        {
            Console.WriteLine($"Access static member : {VariableTest.StaticVar}");

            // To access instance member, remember why they ARE instance members?
            Human victor = new Human() { Name = "Victor", Dob = new DateTime(1999, 01, 01) };
            Human LM = new Human() { Name = "Levaughn", Dob = new DateTime(1985, 01, 01) };

            Human[] PCAD15 = new Human[] { victor, LM };
            Console.WriteLine($"All humans have {Human.PairsOfGenes} pairs of genes");


            foreach (Human h in PCAD15)
            {
                Console.WriteLine($"{h.Name} is born on {h.Dob}");
            }
        }
    }
    class Human {
        internal static int PairsOfGenes = 23;
        internal string Name;
        internal DateTime Dob;
    }


}
