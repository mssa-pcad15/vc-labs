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
      
    }
}

