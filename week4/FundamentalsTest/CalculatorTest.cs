using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fundamentals;
namespace FundamentalsTest
{
    //remember to Add Project Reference(dependency)

    [TestClass] //this is called Attribute(annotation), it is used to decribe the class as it contains tests to be run.
    public class CalculatorTest // by convention, this class contains methods to test Calculator class.
                                // Make it public, so the Test Runner can see this class.
    {
        [TestMethod]
        public void AddTwoPositiveIntegerShouldReturnPositiveResult() {
            //Arrange
            int op1 = 1;
            int op2 = 1;
            //Act
            int actual = Calculator.Add(op1, op2); // here is the RED
            //Assert
            Assert.IsTrue(actual>0);
        }

        [TestMethod]
        public void AddTwoIntegersShouldReturnCorrectResult() {
            //Arrange
            int op1 = 1;
            int op2 = 1;
            //Act
            int actual = Calculator.Add(op1, op2); //here is the RED
            //Assert
            int expected = 2;
            Assert.AreEqual(actual, expected);
        }

    }
}
//Red -> Green -> Refactor
// Add Calculator class to Fundamentals
// Make it public class
// Add: Using Fundamentals; (namespace) to the top of CalculatorTest.cs

// Second Red? Under Add
// what does the method signature look like?
// public method returns an int,
// must be called Add
// takes in 2 int
// static
