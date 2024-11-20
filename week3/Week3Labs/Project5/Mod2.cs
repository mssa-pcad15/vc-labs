
namespace Project5
{

    class Car { public string Name { get; set; } }

    internal class Mod2
    {
        static double pi = 3.1415;

        internal static void ByValueByRefWithRefType()
        {
            Car a = new Car() { Name = "Car A" };
            Car b = new Car() { Name = "Car B" };
            PassByValue(a, b); // the value of a reference type is the address
            Console.WriteLine($"After Call : Car A: {a.Name}, Car B: {b.Name}"); // Car A: ?? Car A   Car B: Car B

            PassByRef(ref a, b); // the value of a reference type is the address
            Console.WriteLine($"After Call PassbyRef : Car A: {a.Name}, Car B: {b.Name}"); // Car A: ?? Car B   Car B: Car B

            void PassByValue(Car x, Car y)
            {
                x = y;
                Console.WriteLine($"in method : Car X: {x.Name}, Car Y: {y.Name}"); // ?? x.Name is "Car B" y.Name is "Car B"
            }

            void PassByRef(ref Car x, Car y)
            {
                x = y;
                Console.WriteLine($"in method : Car X: {x.Name}, Car Y: {y.Name}"); // ?? x.Name is "Car B" y.Name is "Car B"
            }


        }

        internal static void ByValueByRefWithValueType()
        {
            int a = 10;
            int b = 20;
            PassByValue(a, b);
            Console.WriteLine($"a: {a}, b: {b}");

            PassByRef(ref a, b);
            Console.WriteLine($"a: {a}, b: {b}");

            void PassByValue(int x, int y)
            {
                x *= 2; y *= 2;
                Console.WriteLine($"inside method PassByValue : a: {a}, b: {b}"); // me think its 10,20
            }
            void PassByRef(ref int x, int y)
            {
                x *= 2; y *= 2;
                Console.WriteLine($"inside method PassByRef : a: {a}, b: {b}"); // me think its 20,20
            }
        }

        internal static void ChallengeEmail()
        {
            string[,] corporate =
             {
               {"Robert", "Bavin"}, {"Simon", "Bright"},
               {"Kim", "Sinclair"}, {"Aashrita", "Kamath"},
               {"Sarah", "Delucchi"}, {"Sinan", "Ali"}
             };

            string[,] external =
            {
                {"Vinnie", "Ashton"}, {"Cody", "Dysart"},
                {"Shay", "Lawrence"}, {"Daren", "Valdes"}
            };

            string externalDomain = "hayworth.com";

            for (int i = 0; i < corporate.GetLength(0); i++)
            {
                // display internal email addresses
                DisplayEmailAddress(firstName: corporate[i,0], lastName: corporate[i, 1]);
            }

            for (int i = 0; i < external.GetLength(0); i++)
            {
                // display external email addresses
                DisplayEmailAddress( firstName: external[i,0] , lastName: external[i, 1], externalDomain);
            }

            void DisplayEmailAddress(string firstName,string lastName, string domain = "contoso.com")
            {
                Console.WriteLine(firstName[0..2] + lastName + "@" + domain);
            }

            void DisplayInternalEmailAddress(params string[] EmployeeName)
            {
                string emailAccount = EmployeeName[0][0..2] + EmployeeName[1];
                Console.WriteLine($"{(emailAccount + "@contoso.com")}");
            }
            void DisplayExternalEmailAddress(string[] externalAccount,string externalDomain = "")
            {
                string externalEmail = externalAccount[0][0..2] + externalAccount[1] + "@" + externalDomain;
                Console.WriteLine(externalEmail);
            }

        }

        internal static void CubeWithArrayParam()
        {
            DoCube(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            DoCube(1, 2, 3, 4, 5);

            void DoCube(params int[] ints)
            {

                Console.WriteLine($"Received {ints.Length} values.");
                foreach (int i in ints) Console.WriteLine($"{i}: {Pow(baseNumber: i, toThePower: 3)}"); //explicity named parameter
            }

            double Pow(double baseNumber, double toThePower) => Math.Pow(baseNumber, toThePower);
        }

        internal static void Exercise1()
        {
            int[] schedule = { 800, 1200, 1600, 2000 };
            DisplayAdjustedTimes(schedule, 6, -6);
            //DisplayAdjustedTimes(schedule, 9, 6);

            void DisplayAdjustedTimes(int[] times, int currentGMT, int newGMT)
            {
                int diff = 0;
                if (Math.Abs(newGMT) > 12 || Math.Abs(currentGMT) > 12)
                {
                    Console.WriteLine("Invalid GMT");
                }
                else if (newGMT <= 0 && currentGMT <= 0 || newGMT >= 0 && currentGMT >= 0)
                {
                    diff = 100 * (Math.Abs(newGMT) - Math.Abs(currentGMT));
                }
                else
                {
                    diff = 100 * (Math.Abs(newGMT) + Math.Abs(currentGMT));
                }

                for (int i = 0; i < times.Length; i++)
                {
                    int newTime = ((times[i] + diff)) % 2400;
                    Console.WriteLine($"{times[i]} -> {newTime}");
                }
            }
        }

        internal static void Exercise2()
        {
            string[] students = { "Jenna", "Ayesha", "Carlos", "Viktor" };

            DisplayStudents(students);
            DisplayStudents(new string[] { "Robert", "Vanya" });

            void DisplayStudents(string[] students) // this is a local method
            {
                foreach (string student in students)
                {
                    Console.Write($"{student}, ");
                }
                Console.WriteLine();
            }



            PrintCircleArea(12);
            PrintCircleCircumference(12);

            void PrintCircleArea(int radius)
            {
                double area = pi * (radius * radius);
                Console.WriteLine($"Area = {area}");
            }

            void PrintCircleCircumference(int radius)
            {
                double circumference = 2 * pi * radius;
                Console.WriteLine($"Circumference = {circumference}");
            }

        }

        internal static void OptionalParam()
        {
            DemoParam(p1: "Bob");
            DemoParam(p1: "Bob", p2: "Goodbye");

            DemoParamArray("Bob", 1, 2, 3, 4, 5);

            void DemoParam(string p1, string p2 = "Hello")
            {
                Console.WriteLine($"{p2} {p1}");
            }

            void DemoParamArray(string p1, params int[] numbers)
            {
                int sum = numbers.Sum();
                Console.WriteLine($"{p1} : sum: {sum}");
            }
        }

        internal static void StringDemo()
        {
            string status = "Healthy";

            Console.WriteLine($"Start: {status}");
            string result = SetHealth(status, false);
            Console.WriteLine($"{result}");
            Console.WriteLine($"End: {status}");

            string SetHealth(string _status, bool _isHealthy)
            {
                _status = (_isHealthy ? "Healthy" : "Unhealthy");
                Console.WriteLine($"Middle: {_status}");
                return _status;
            }
        }
    }
}