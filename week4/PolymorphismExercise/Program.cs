using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using Fundamentals.LearnTypes;
namespace PolymorphismExercise;

class Program
{
    static void Main(string[] args)
    {
        
        string originalText = "Hello World";
        byte[] dataArray = Encoding.UTF8.GetBytes(originalText);

        HashAlgorithm sha = SHA512.Create();
        byte[] result = sha.ComputeHash(dataArray); //polymorphic call to ComputeHash.


        var base64EncodedResult = Convert.ToBase64String(result);
        Console.WriteLine(base64EncodedResult);
        //PolymorphicAnimal();

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
                    animals.Add(new Cat() { Im = "Cat" });
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

abstract class Animal {
    internal abstract void MakeNoise();
    internal string Im { get;  set; }
    public virtual void Move() { Console.WriteLine("Animal Moves"); }
}

class Cat : Animal {

    internal override void MakeNoise()
    {
        Console.WriteLine("Cat says Meow.");
    }
    public override void Move() {
        Console.WriteLine("Cat Move.");
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
