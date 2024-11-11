namespace FtoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int fahrenheit = 94;
            var celsius = 5 * (fahrenheit - 32) / 9f;
            Console.WriteLine($"{fahrenheit}°F is {celsius:n1}°C.");
        }
    }
}
