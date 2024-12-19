using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSATest.Dictionary
{
    [TestClass]
    public class DictionaryExercises
    {
        [TestMethod]
        public void TestDictionary() {

            //declare a dictionary of string string
            //
            SortedDictionary<string, string> myDictionary = new();
            myDictionary["Krakra"] = "Something really crazy";
            myDictionary.Add("Chinzilla", "Someone with humoungous chin");

            Assert.AreEqual(2,myDictionary.Count);
            Assert.IsTrue(myDictionary.ContainsKey("Krakra"));
            Assert.IsTrue(myDictionary.ContainsKey("Chinzilla"));
            Assert.ThrowsException<ArgumentException>(()=>myDictionary.Add("Chinzilla", "Victor Chin"));

            if (!myDictionary.TryAdd("Chinzilla", "Victor Chin")) {
                myDictionary["Chinzilla"] = "Victor Chin"; //do an update instead
            }
            Assert.AreEqual("Victor Chin", myDictionary["Chinzilla"]);
            if (!myDictionary.TryAdd("NewKey", "Victor Chin"))
            {
                Assert.Fail();
            }
            Assert.IsTrue(myDictionary.ContainsKey("NewKey"));

            Assert.IsTrue(myDictionary.Remove("NewKey"));
            Assert.IsFalse(myDictionary.ContainsKey("NewKey"));

            int expected = 2;
            int counter = 0;
            foreach (var key in myDictionary.Keys)
            {
                counter++;
                Debug.WriteLine(key);
            }
            Assert.AreEqual(expected, counter);
            counter = 0;
            foreach (var aValue in myDictionary.Values)
            {
                counter++;
                Debug.WriteLine(aValue);
            }
            Assert.AreEqual(expected, counter);

            Assert.AreEqual(2,myDictionary.Count);

        }


    }
}
