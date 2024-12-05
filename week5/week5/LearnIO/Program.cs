using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace LearnIO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] data = [1, 2, 3, 4, 67, 68, 69, 70];
            string happy = @"\(^ヮ^)/";
            byte[] happyInBytes = Encoding.UTF8.GetBytes(happy);

            // using the static method : File.Create to create a new file
            //CreateFile(data, happyInBytes);

            FileStream s1 = File.Create("FileWithBinaryWriter.bin");
            BinaryWriter bw = new BinaryWriter(s1); //constructor of BinaryWrite requires a backing stream
            //bw.Write(@"HelloWorld, \(^ヮ^)/");
            bw.Write(true); //01
            bw.Write(false);
            bw.Write(float.MaxValue);
            bw.Write(int.MaxValue); //FF FF FF 7F
            bw.Write(int.MinValue); //00 00 00 80

            bw.Flush();
            bw.Close();


            FileStream s2 = File.Create("FileWithStreamWriter.txt");
            StreamWriter sw = new StreamWriter(s2, Encoding.UTF8);
            sw.Write(@"HelloWorld, \(^ヮ^)/"); ;
            sw.Write(true);
            sw.Write(int.MaxValue);
            sw.Write(int.MinValue);
            sw.WriteLine();

            sw.Flush();
            sw.Close();

            FileStream s3 = File.Create("FileWithJsonSerialization.txt");
            StreamWriter sw2 = new StreamWriter(s3, Encoding.UTF8);

            DemoClass serializeThis = new() { FlagA = true, FlagB = false, StringC = "Hello", IntD = 1000 };
            
            string jsonString = JsonSerializer.Serialize<DemoClass>(serializeThis,
                new JsonSerializerOptions
                {
                    IncludeFields = true,
                    WriteIndented = true
                });

            sw2.Write(jsonString);
            sw2.Flush();
            sw2.Close();

        }

        public class DemoClass
        {
            public bool FlagA;
            public bool FlagB;
            public string StringC;
            public int IntD;
        }


        private static void CreateFile(byte[] data, byte[] happyInBytes)
        {
            FileStream s1 = File.Create("FileCreatedByStatic.txt");
            s1.Write(data, 0, data.Length);
            Console.WriteLine(s1.Position);

            s1.WriteByte(71);
            s1.Position = 0;
            s1.WriteByte(71);
            s1.Position = 9;

            s1.Write(happyInBytes);

            s1.Close();

            // using the instance to create a new file
            FileInfo aNewFile = new FileInfo("FileCreatedByInstance.txt");
            FileStream s2 = aNewFile.Create();
            s2.Write(data, 0, data.Length);
            s2.Close();
        }
    }
}
