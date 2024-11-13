internal class Program
{
    private static void Main(string[] args)
    {
        // initialize variables - graded assignments 
        int currentAssignments = 5;

        int[] sophiaScores = new int[] { 90, 86, 87, 98, 100, 94, 90 };
        int[] andrewScores = new int[] { 92, 89, 81, 96, 90, 89 };
        int[] emmaScores = new int[] { 90, 85, 87, 98, 68, 89, 89, 89 };
        int[] loganScores = new int[] { 90, 95, 87, 88, 96, 96 };
        int[] beckyScores = new int[] { 92, 91, 90, 91, 92, 92, 92 };
        int[] chrisScores = new int[] { 84, 86, 88, 90, 92, 94, 96, 98 };
        int[] ericScores = new int[] { 80, 90, 100, 80, 90, 100, 80, 90 };
        int[] gregorScores = new int[] { 91, 91, 91, 91, 91, 91, 91 };

        // Student names
        string[] studentNames = new string[] { "Sophia", "Andrew", "Emma", "Logan", "Becky", "Chris", "Eric", "Gregor" };
        
        Console.WriteLine("Student\t\tGrade\n");

        foreach (string studentName in studentNames) 
        {
            int[] scores;

            if (studentName == "Sophia") scores = sophiaScores;
            else if (studentName == "Andrew") scores = andrewScores;
            else if (studentName == "Emma") scores = emmaScores;
            else if (studentName == "Becky") scores = beckyScores;
            else if (studentName == "Chris") scores = chrisScores; 
            else if (studentName == "Eric") scores = ericScores;
            else if (studentName == "Gregor") scores = gregorScores;
            else scores = loganScores;

            int scoreSum = 0;
            int gradedAssignments = 0;
            foreach (int score in scores)
            {
                // increment the assignment counter
                gradedAssignments += 1;
                if (gradedAssignments <= currentAssignments)
                {
                    scoreSum += score;
                }
                else 
                {
                    scoreSum += score/10;
                }
            }

            decimal currentStudentGrade = (decimal) scoreSum / currentAssignments;

            string currentStudentLetterGrade = "";

            #region bunch of if else for grade letter
                if (currentStudentGrade >= 97)
                    currentStudentLetterGrade = "A+";

                else if (currentStudentGrade >= 93)
                    currentStudentLetterGrade = "A";

                else if (currentStudentGrade >= 90)
                    currentStudentLetterGrade = "A-";

                else if (currentStudentGrade >= 87)
                    currentStudentLetterGrade = "B+";

                else if (currentStudentGrade >= 83)
                    currentStudentLetterGrade = "B";

                else if (currentStudentGrade >= 80)
                    currentStudentLetterGrade = "B-";

                else if (currentStudentGrade >= 77)
                    currentStudentLetterGrade = "C+";

                else if (currentStudentGrade >= 73)
                    currentStudentLetterGrade = "C";

                else if (currentStudentGrade >= 70)
                    currentStudentLetterGrade = "C-";

                else if (currentStudentGrade >= 67)
                    currentStudentLetterGrade = "D+";

                else if (currentStudentGrade >= 63)
                    currentStudentLetterGrade = "D";

                else if (currentStudentGrade >= 60)
                    currentStudentLetterGrade = "D-";
                else
                    currentStudentLetterGrade = "F";
            #endregion

            Console.WriteLine($"{studentName}:\t\t{currentStudentGrade}\t{currentStudentLetterGrade}");
        }


    
        Console.WriteLine("Press the Enter key to continue");
        Console.ReadLine();
    }
}