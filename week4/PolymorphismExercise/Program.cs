using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Channels;
using Fundamentals.LearnTypes;
namespace PolymorphismExercise;

class Program
{
    static void Main(string[] args)
    {

        //PolymorphicAnimal();
        //HashAlgoUsesInheritanceDemo();
        //FunctionalProgrammingWithExtensionMethod();
        //DemoToHashExtensionMethod();

        DateTime today = DateTime.Now;
        int howManyDays = today.HowManyDaysToMyNextBirthday();
        if (howManyDays > 100) Console.WriteLine($"So far away...{howManyDays} days to go.");
        else Console.WriteLine($"Almost there, {howManyDays} days to go.");

        static void FunctionalProgrammingWithExtensionMethod()
        {
            String s = "Hello World, this is a good day.";
            char[] resultString = s.ThenCaptializeTheString().ThenTakeEvenLetters().ThenPutSpaceBetweenLetters().Reverse().ToArray();
            Console.WriteLine(new string(resultString));
            //pipe and filter pattern
        }

        static void DemoToHashExtensionMethod()
        {
            String s2 = "Hello World, this is a good day.";
            Console.WriteLine($"{s2.ToHash()} using default algo");
            Console.WriteLine($"{s2.ToHash(SHA512.Create())} using SHA512 algo");
            Console.WriteLine($"{s2.ToHash(SHA256.Create())} using SHA256 algo");
        }
        //create an extension method named HowManyDaysToMyNexBirthday which extends DateTime data type and return number of days from this date to your next bd
    }

    private static void HashAlgoUsesInheritanceDemo()
    {
        string originalText = "Hello World";
        byte[] dataArray = Encoding.UTF8.GetBytes(originalText);

        HashAlgorithm sha = SHA512.Create();
        byte[] result = sha.ComputeHash(dataArray); //polymorphic call to ComputeHash.


        var base64EncodedResult = Convert.ToBase64String(result);
        Console.WriteLine(base64EncodedResult);
    }

    private static void PolymorphicAnimal()
    {
        System.Random rand = new Random();
        List<Animal> animals = new List<Animal>(); //List<Animal> store reference to Animal and its derived instances
        for (int i = 0; i < 5; i++)
        {
            switch (rand.Next(100) % 3)//this generate random animal instance
            {
                case 0:
                    animals.Add(new Cat() { Im = "Cat",ThisCanBeSetByCtor=5 }); // setting this property in Object Initializer
                    break;
                case 1:
                    animals.Add(new Tiger() { Im = "Tiger" });
                    break;
                case 2:
                    animals.Add(new Bird() { Im = "Bird" });
                    break;
                default:
                    break;
            }
        }
        foreach (Animal animal in animals)
        {
            animal.MakeNoise();//polymorphic call that will not fail, and most derived version will execute.
            animal.Move();
        }
    }
}

static class MyExtensionMethods { //extension methods must be in a static class, the class name is not used anywhere.
    //we use extension method when the class doesn't do what we want it to do, so we tack on add'l functionality.
    public static int HowManyDaysToMyNextBirthday(this DateTime theDate, int month=10,int day=11) {
        DateTime thisYearBD = new DateTime(DateTime.Now.Year, month, day);
        if (theDate > thisYearBD)
        { //theDate is after this year's bd
            DateTime nextBirthDay = new DateTime(thisYearBD.Year + 1, month, day);
            return nextBirthDay.Subtract(theDate).Days;
        }
        else
        {
            return thisYearBD.Subtract(theDate).Days;
        }
    }
    public static IEnumerable<char> ThenTakeEvenLetters(this IEnumerable<char> input) //extension method must also be staic
                                                                //^^^ the first parameter of extension use "this" keyword infront of the type we want to extend
                                                                //^^^ input is the object we are extending the functionality
    {
        int counter = 0; //initialize a counter
        foreach (char c in input) //go through each letter 1 by 1
        {
            if (counter % 2 == 0) yield return c;
            counter++;
        }
    }
    public static IEnumerable<char> ThenPutSpaceBetweenLetters(this IEnumerable<char> input)
    {
        foreach (char c in input) //go through each letter 1 by 1
        {
          yield return c;
          yield return ' ';
        }
    }
    public static IEnumerable<char> ThenCaptializeTheString(this IEnumerable<char> input)
    {
        foreach (char c in input) //go through each letter 1 by 1
        {
            yield return new String(c,1).ToUpper()[0];
        }
    }

    public static string ToHash(this string s, HashAlgorithm? algo=null )
    {
        algo ??= MD5.Create();
        byte[] hashBytes= algo.ComputeHash(Encoding.UTF8.GetBytes(s));
        return Convert.ToBase64String(hashBytes);
    }
}
abstract class Animal {

    //fully implemented property
    private int numberOfWheelTurns; //private backing field

    public float MileageInKm //public property, get and set 
    {
        get { return numberOfWheelTurns*0.001f; }
       
    }
    public float MileageInMile //public property, get and set 
    {
        get { return numberOfWheelTurns* 0.0006f; }

    }
    
    //auto-implemented property
    public int ThisHasNoBackingField { get; set; }

    //init property, can be set by constructor or object initializer
    public int ThisCanBeSetByCtor { get; init; }



    internal abstract void MakeNoise();
    internal string Im { get;  set; }
    public virtual void Move() {
      
        Console.WriteLine("Animal Moves"); }
}

class Cat : Animal {

    internal override void MakeNoise()
    {
        Console.WriteLine("Cat says Meow.");
    }
    public override void Move() {
        Console.WriteLine("Cat Move.");
    }

    public Cat() { 
        this.ThisCanBeSetByCtor = 5; // see how I can set this in ctor?
    }
}

sealed class Tiger : Cat
{
   
    public override void Move()
    {
        Console.WriteLine("Cat Move.");
    }
}
class Dog : Animal
{
    internal override void MakeNoise()
    {
        Console.WriteLine("Dog says Woof.");
    }
    public override void Move()
    {
        Console.WriteLine("Dog Move.");
    }
}
class Bird : Animal
{
    internal override void MakeNoise()
    {
        Console.WriteLine("Bird says pio pio.");
    }
    public override void Move()
    {
        Console.WriteLine("Bird Fly.");
    }
}


// Create a base class Animal, that will abstract
// Animal Contains abstract method MakeNoise

// create 3 derive child class from Animal: Cat, Dog, Bird
// implement abstract method to print their noise.
