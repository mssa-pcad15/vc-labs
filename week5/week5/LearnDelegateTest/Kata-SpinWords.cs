using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnDelegate;
namespace LearnDelegateTest
{
    [TestClass]
    public class Kata_SpinWords
    {
        [TestMethod]
        public void Test1() {

            string input = "Hey fellow warriors";
            string expected = "Hey wollef sroirraw";

            string actual = Kata.SpinWords(input);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test2()
        {

            string input = "This is a test";
            string expected = "This is a test";

            string actual = Kata.SpinWords(input);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test3()
        {

            string input = "This is another test";
            string expected = "This is rehtona test";

            string actual = Kata.SpinWords(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
