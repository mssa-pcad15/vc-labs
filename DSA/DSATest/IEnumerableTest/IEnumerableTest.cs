using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSATest.IEnumerableTest
{
    [TestClass]
    public class IEnumerableTest
    {
        [TestMethod]
        public void HowDoesIenumerableWorks() {
            string s = "Hello World";

            CharEnumerator charPointer = s.GetEnumerator();
            int count = 0;
            char[] outcome = new char[s.Length];
            while (charPointer.MoveNext())
            {
                outcome[count] = charPointer.Current;
                count++;
            }
            Assert.AreEqual(11,count);
            Assert.AreEqual('H', outcome[0]);
            Assert.AreEqual('e', outcome[1]);

          
        }
    }
}
