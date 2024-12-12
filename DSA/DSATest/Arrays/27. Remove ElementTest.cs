using DSA.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSATest.Arrays
{
    [TestClass]
    public class RemoveElementTest
    {
        [TestMethod]
        public void TestCase1() {

            int[] nums =
                [0, 1, 2, 2, 3, 0, 4, 2];

            int val = 2;
          

            int k = LeetCode27.RemoveElement(nums,val);
            int[] expected = [0, 1, 4, 0, 3];
            
            Assert.AreEqual(expected.Length, k);

        }
    }
}
