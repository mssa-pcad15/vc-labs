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
    public class ListEquality // by convention, this class contains methods to test Calculator class.
                                // Make it public, so the Test Runner can see this class.
    {
        [TestMethod]
        public void TwoListOfIdenticalElementsShouldBeEqual() {
            //Arrange
            List<int> op1 = new() { 1, 2, 3, 4, 5, };
            List<int> op2 = new() { 1, 2, 3, 4, 5, };
            //Assert.AreEqual(op1, op2,new ListEqualityComparer());
        }
        [TestMethod]
        public void TwoListOfDifferentLengthShouldNotBeEqual()
        {
            //Arrange
            List<int> op1 = new() { 1, 2, 3, 4, 5, };
            List<int> op2 = new() { 1, 2, 3, };
            //Assert.AreNotEqual(op1, op2, new ListEqualityComparer());
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
