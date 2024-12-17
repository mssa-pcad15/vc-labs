using DSA.DataStructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSATest.LinkedListTest
{
    [TestClass]
    public class ArrayListTest
    {
        [TestMethod]
        public void DemoArrayList() {

            ArrayList al = new();

            al.Add(1);
            al.Add("Hello");
            al.Add(new DateTime(2000,10,10));
            Assert.IsInstanceOfType( al[0], typeof(int));
        }
        [TestMethod]
        public void Demoist()
        {

            List<int> intList = new();
            intList.Add(1);
            intList.Add(2);
            intList.Add(3);
            //intList.Add("Hello");
            //intList.Add(new DateTime(2000, 10, 10));

            foreach (int i in intList)
            {
                Assert.IsInstanceOfType(i, typeof(int));
            }

            List<Car> garage = new();
            garage[0]
        }
    }
}

