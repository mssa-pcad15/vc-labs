internal class Program
{
    private static void Main(string[] args)
    {
        // initialize variables - graded assignments 
        int currentAssignments = 5;

        int[] sophiaScores = [90, 86, 87, 98, 100];
        int[] andrewScores = [92, 89, 81, 96, 90];
        int[] emmaScores = [90, 85, 87, 98, 68];
        int[] loganScores = [90, 95, 87, 88, 96];

        string[] studentNames = ["Sophia", "Andrew", "Emma", "Logan"];

        int[] scores;

        Console.WriteLine("Student\t\tGrade\n");

        foreach (string studentName in studentNames) 
        {
            if (studentName == "Sophia") scores = sophiaScores;
            else if (studentName == "Andrew") scores = andrewScores;
            else if (studentName == "Emma") scores = emmaScores;
            else scores = loganScores;

            int scoreSum = 0;
            foreach (int score in scores)
            {
                scoreSum += score;
            }
            decimal studentScore=(decimal) scoreSum / currentAssignments;
            Console.WriteLine($"{studentName}:\t\t{studentScore}\t?");
        }


    
        Console.WriteLine("Press the Enter key to continue");
        Console.ReadLine();
    }
}