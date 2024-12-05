using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace DeserializeJson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Get a File Stream Instance using the Json File
            //FileStream fs = File.OpenRead("FileWithJsonSerialization.txt");
            //StreamReader sr = new StreamReader(fs);

            StreamReader sr = File.OpenText("FileWithJsonSerialization.txt");
            //Use a StreamReader to read the entirety of the file
            // into a string variable named jsonString
            // hint: there should be a method : ReadToEnd
            string jsonString = sr.ReadToEnd();

            //this is quick and dirty, tell the compiler to stop type checking variable
            //obj, you can deserialize in to dynamic. 
            //JsonConvert.Deserialize
            //Console.WriteLine($"FlagA: {obj.FlagA}, FlagB: {obj.FlagB}");
            //Console.WriteLine($"StringC: {obj.StringC}, IntD: {obj.IntD}");

            //this is the c# way
            //paste as class and deserialize into instance of class.
            DemoClass obj = JsonSerializer.Deserialize<DemoClass>(jsonString);
            Console.WriteLine($"FlagA: {obj.FlagA}, FlagB: {obj.FlagB}");
            Console.WriteLine($"StringC: {obj.StringC}, IntD: {obj.IntD}");
        }
    }


    public class DemoClass
    {
        public bool FlagA { get; set; }
        public bool FlagB { get; set; }
        public string StringC { get; set; }
        public int IntD { get; set; }
    }

}
