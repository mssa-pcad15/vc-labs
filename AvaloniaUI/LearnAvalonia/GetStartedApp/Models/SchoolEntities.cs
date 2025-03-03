using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetStartedApp.Models
{
    public enum Sex
    {
        Diverse,
        Female,
        Male
    }

    public class Person
    {
        /// <summary>
        /// Gets or sets the first name of the person. You can only set this property on init.
        /// </summary>
        public string? FirstName { get; init; }

        /// <summary>
        /// Gets or sets the last name of the person. You can only set this property on init.
        /// </summary>
        public string? LastName { get; init; }

        /// <summary>
        /// Gets or sets the age of the person. You can only set this property on init.
        /// </summary>
        public int Age { get; init; }

        /// <summary>
        /// Gets or sets the sex of the person. You can only set this property on init.
        /// </summary>
        public Sex Sex { get; init; }
    }

    public class Student : Person
    {
        /// <summary>
        /// Gets or sets the grade of the student. You can only set this property on init.
        /// </summary>
        public int Grade { get; init; }
        public override string ToString() => $"{FirstName} {LastName} is a {Grade}th grade student.";
    }
    public class Teacher : Person
    {
        /// <summary>
        /// Gets or sets the subject that the teacher is teaching. You can only set this property on init.
        /// </summary>
        public string? Subject { get; init; }
        public override string ToString() => $"{FirstName} {LastName} is teaching {Subject}.";
    }
}
