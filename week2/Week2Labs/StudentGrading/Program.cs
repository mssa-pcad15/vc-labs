internal class Program
{
    private static void Main(string[] args)
    {
        // initialize variables - graded assignments 
        int currentAssignments = 5;

        int sophia1 = 93;
        int sophia2 = 87;
        int sophia3 = 98;
        int sophia4 = 95;
        int sophia5 = 100;

        int nicolas1 = 80;
        int nicolas2 = 83;
        int nicolas3 = 82;
        int nicolas4 = 88;
        int nicolas5 = 85;

        int zahirah1 = 84;
        int zahirah2 = 96;
        int zahirah3 = 73;
        int zahirah4 = 85;
        int zahirah5 = 79;

        int jeong1 = 90;
        int jeong2 = 92;
        int jeong3 = 98;
        int jeong4 = 100;
        int jeong5 = 97;
        
        int nicolasSum, sophiaSum, zahirahSum, jeongSum;

        nicolasSum = nicolas1 + nicolas2 + nicolas3 + nicolas4 + nicolas5;
        sophiaSum = sophia1 + sophia2 + sophia3 + sophia4 + sophia5;
        zahirahSum = zahirah1 + zahirah2 + zahirah3 + zahirah4 + zahirah5;
        jeongSum = jeong1 + jeong2 + jeong3 + jeong4 + jeong5;
        Console.WriteLine("Sum:");
        Console.WriteLine($"{"Sophia:",10} {sophiaSum,4}");
        Console.WriteLine($"{"Nicolas:",10} {nicolasSum,4}");
        Console.WriteLine($"{"Zahirah:",10} {zahirahSum,4}");
        Console.WriteLine($"{"Jeong:",10} {jeongSum,4}");

        decimal sophiaScore= (decimal) sophiaSum /  currentAssignments;
        decimal nicolasScore= (decimal) nicolasSum /  currentAssignments;
        decimal zahirahScore= zahirahSum / (decimal) currentAssignments;
        decimal jeongScore= jeongSum / (decimal) currentAssignments;

        Console.WriteLine("Score:");
        Console.WriteLine($"{"Sophia:",10} {sophiaScore,4} {getGrade(sophiaScore)}");
        Console.WriteLine($"{"Nicolas:",10} {nicolasScore,4} {getGrade(nicolasScore)}");
        Console.WriteLine($"{"Zahirah:",10} {zahirahScore,4} {getGrade(zahirahScore)}");
        Console.WriteLine($"{"Jeong:",10} {jeongScore,4} {getGrade(jeongScore)}");
        //local function inside of Main
        string getGrade(decimal score) {
            return score switch
            {
                (>= 97) and (<= 100) => "A+",
                (>= 93) and (<= 96) => "A",
                (>= 90) and (<= 92) => "A-",
                (>= 87) and (<= 89) => "B+",
                (>= 83) and (<= 86) => "B"
            };

        }
    }

   
}