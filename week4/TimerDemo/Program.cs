namespace TimerDemo
{

    public class SlowCalculator {
        public string Name { get; set; }

        public event EventHandler<int>? ResultDone=null;
        public SlowCalculator(string name)
        {
            Name = name;

        }
        public void Add(int x, int y)
        {
            Thread.Sleep(3000);

            ResultDone?.Invoke(this, x+y);
        }
        
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            string result = new string("Here is a sentence".Where(
                (char aChar,int index) => index%2==0).ToArray());
            Console.WriteLine(result);

           
           
        }

        private static void EventHandlingDemo()
        {
            //TimerDemo();
            SlowCalculator calc = new SlowCalculator("calc 1");
            SlowCalculator calc1 = new SlowCalculator("calc 2");
            SlowCalculator calc2 = new SlowCalculator("calc 3");
            calc.ResultDone += handleEvent;
            calc1.ResultDone += handleEvent;
            calc2.ResultDone += handleEvent;
            calc.Add(5, 6);
            calc1.Add(3, 8);
            calc2.Add(7, 8);
        }

        private static void handleEvent(object? sender, int e)
        {
            SlowCalculator c = sender as SlowCalculator;
            Console.WriteLine($"{c.Name} is finished, answer is {e}");
        }

        private static void TimerDemo()
        {
            System.Timers.Timer timer = new System.Timers.Timer(); //instantiate a timer

            timer.Elapsed += Timer_Elapsed;
            timer.Elapsed += (object? sender, System.Timers.ElapsedEventArgs e) => //this create a method with
            {
                Console.WriteLine("This is from lambda event handler, short hand, eliminate single use private method");
                Console.WriteLine($"Timer rang handler2, {e.SignalTime}");
            }; //this is using lambda shorthand

            timer.Interval = 10000;//set the interval to 10 second
            timer.Start(); // start the timer
            Console.WriteLine("Press enter to end the program.");
            Console.ReadLine();
        }

        private static void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e) // this is fully implmented delegate
        {
            Console.WriteLine($"Timer rang handler1, {e.SignalTime}");
        }
    

    }
}
