using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LearnDelegateTest
{
    [TestClass]
    public class DelegateInDotNetTest
    {
        [TestMethod]
        public void PickItemFromCollectionTest() {

            IEnumerable<int> bagOfNumbers = Enumerable.Range(1, 1000);

            // extension methods

            var result = bagOfNumbers.PickItems<int>((i) => false);
            Assert.AreEqual(0,result.Count());


            var result2 = bagOfNumbers.PickItems((i) => i%2==0);
            Assert.AreEqual(500, result2.Count());

            var result3 = bagOfNumbers.PickItems((i) => (i % 7 == 0) && (i % 11 == 0));
            Assert.AreEqual(12, result3.Count());
            foreach (var item in result3) { 
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void PickPersonFromCollectionTest() {
           Person[] people = { 
                new Person("Alice", 35),
               new Person("Bob", 25),
               new Person("Charlie", 45)
            };
            //where age > than 30
            var ageResult = people.PickItems(p => p.Age > 30);
            Assert.AreEqual(2, ageResult.Count());

            //where name starts with A
            var initialResult = people.PickItems(p => p.Name.StartsWith('A'));
            Assert.AreEqual(1, initialResult.Count());
            Assert.AreEqual("Alice", initialResult.First().Name);

            //where name length > 5
            var nameLength = people.PickItems(p => p.Name.Length>5);
            Assert.AreEqual(1, nameLength.Count());
            Assert.AreEqual("Charlie", nameLength.First().Name);
        }


        [TestMethod]
        public void PickPersonFromCollectionUsingLinqTest() //Language INtegerated Query
        {
            Person[] people = {
                new Person("Alice", 35),
               new Person("Bob", 25),
               new Person("Charlie", 45)
            };
            //where age > than 30
            var ageResult =people.Where(p=>p.Age>30);
            Assert.AreEqual(2, ageResult.Count());

            //where name starts with A
            var initialResult = people.Where(p => p.Name.StartsWith('A'));
            Assert.AreEqual(1, initialResult.Count());
            Assert.AreEqual("Alice", initialResult.First().Name);

            //where name length > 5
            var nameLength = people.Where(p => p.Name.Length > 5);
            Assert.AreEqual(1, nameLength.Count());
            Assert.AreEqual("Charlie", nameLength.First().Name);
        }

    }

    class Person(string Name, int Age)
    {
        public string Name { get; } = Name;
        public int Age { get; } = Age;
    }

   //public delegate bool delFilter<T>(T input);

    public static class MyExtensions
    {
        public static IEnumerable<T> PickItems<T>(this IEnumerable<T> values, Func<T,bool> filter) {
            foreach (var item in values) { 
                if (filter(item)) yield return item;
            }
        }
    }

    // Query : 
    // finding answer from collection of things
    // filtering
    // projecting
    // sorting
    // grouping
    // aggregate
    // 
    // secret of life is : a series query
    // SQL, XQuery, CSS Query, JQuery, JSonpath, MDX, DMX, PowerPivot, PowerQuery, GraphQL, OData , MSGraph, DAX , Gusto -- none runs in c#
    // LINQ runs in C#
     
}
