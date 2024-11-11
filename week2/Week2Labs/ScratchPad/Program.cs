namespace ScratchPad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            
            var result = Math.Sin(Math.PI*0.25); 
            //SIN is a "pure function", static - means method can be invoke without
            //instantiation
            Console.WriteLine();

            House my;
            House yours;
            my = new House();
            yours = new House();
            my.Street = "Main St";
            my.Number = 100;

            yours.Street = "Elm St";
            yours.Number = 50;

            Console.WriteLine(my.GetAddress());
            Console.WriteLine(yours.GetAddress());
        }
    }

    class House {

        public int Number { get; set; }
        public string Street { get; set; }

 

        public string GetAddress() {
            return $"{Number} Street";
        }
       
    }
}
