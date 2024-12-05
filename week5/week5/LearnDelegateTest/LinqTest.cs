using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LearnDelegateTest
{
    [TestClass]
    public class LinqTest
    {
        [TestMethod]
        public void LinqTestWhere() {
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            var result = names.Where( s =>s.Length == 4);
            Assert.IsTrue(result.Contains("Dick"));
            Assert.IsTrue(result.Contains("Mary"));
            Assert.IsFalse(result.Contains("Jay"));
        }
        [TestMethod]
        public void LinqTestWhereWithIndex()
        {
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            var result = names.Where((s, i) => i < 3);
            Assert.IsTrue(result.Contains("Tom"));
            Assert.IsTrue(result.Contains("Dick"));
            Assert.IsTrue(result.Contains("Harry"));
        }

        [TestMethod]
        public void LinqTestOrderBy()
        {
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
            
            //sort this string array with number of char
            var result = names.OrderBy(s => s.Length).ThenBy(s=>s[0]);
            Assert.IsTrue(result.Last()=="Harry");
            Assert.IsTrue(result.First()=="Jay");
            Assert.AreNotEqual(names[4], result.Last());//this shows the orderby does not change
            // the original array instead create new array, so space complexity is high
            
        }

        [TestMethod]
        public void TestTakeAndSkip() {
            string[] typeNames =
                     (from t in typeof(int).Assembly.GetTypes() select t.Name).ToArray();

            var result = typeNames.Take(5);
            Assert.AreEqual(5, result.Count());

            var resultSkip = typeNames.Skip(5).Take(1);
            var verify = typeNames.Take(6);
            
            Assert.AreEqual(verify.Last(), resultSkip.First());
        }

        [TestMethod]
        public void TestSelectInLinq()
        {
            var fonts = FontFamily.Families;
            var result = fonts.Take(5).Select(f => f.Name);
            foreach (var name in result) { 
                Debug.WriteLine(name);
            }
        }

        [TestMethod]
        public void TestSelectNewInLinq()
        {
            // select initial letter, name, length of name
            // here comes anonymous class

            var fonts = FontFamily.Families;
            var result = fonts.Take(10).Select
            (
              (f, i) =>
              new // this is the keyword to declare a anon class
              {
                  InitialLetter = f.Name[0],
                  Name = f.Name,
                  FontNameLength = f.Name.Length,
                  OrdinalPosition = i
              }
            );
            foreach (var thing in result) // must use var keyword as anonymous class doesn't have a name
            {
                Debug.WriteLine($"{thing.InitialLetter},{thing.Name},{thing.FontNameLength}");
            }
        }

        [TestMethod]
        public void TestGroupingInLinq()
        {
            // select initial letter, name, length of name
            // here comes anonymous class

            var fonts = FontFamily.Families;
            var resultDetail = fonts.Select
            (
              f =>
              new // this is the keyword to declare a anon class
              {
                  InitialLetter = f.Name[0],
                  Name = f.Name,
                  FontNameLength = f.Name.Length
              }
            );

           var resultGrouped =  resultDetail.GroupBy(anon => anon.InitialLetter);
            Debug.WriteLine($"There {resultGrouped.Count()} groups from {resultDetail.Count()} fonts");

            foreach (var thing in resultGrouped) // each thing in resultGrouped represents 1 Group, keyed by initial letter
            {
                Debug.WriteLine($"Letter {thing.Key} has {thing.Count()} fonts. With Average Length {thing.Average(f=>f.FontNameLength)}"); // each thing is also an IEnumerable, so we can call Count()
               //thing.Key is the distinct value with which the group is formed.
                
                foreach (var font in thing) {
                    Debug.WriteLine($"\t{font.Name}"); //each thing is an enumerable of the rows that makes up the group
                }
            }
        }


        [TestMethod]
        public void TestGarage()
        {
            Garage g = new Garage();

            //use generic enumerable
            foreach (Car c in g) { 
                Debug.WriteLine(c.Name);   
            }
            
            //use nongeneric enumerable
            foreach (object obj in g)
            {
                if (obj is Car)
                {
                    Debug.WriteLine(((Car)obj).Name);
                }
            }
        }
    }


    class Garage : IEnumerable<Car>
    {
        internal class GarageEnumerator : IEnumerator<Car>
        {
            private  Car[] cars;
            private int index=-1;

            public Car Current => cars[index];

            object IEnumerator.Current => cars[index];

            public GarageEnumerator(Car[] cars)
            {
                this.cars = cars;
            }
            public bool MoveNext()
            {
                if (index >= cars.Length-1) return false;
                else 
                {
                    index += 1;
                    return true;
                }
            }

            public void Reset()
            {
                index = -1;
            }

            public void Dispose()
            {
                ;
            }
        }
        private Car[] cars= new Car[5];
        private GarageEnumerator _enumerator;
        public Garage()
        {
            cars[0] = new Car("A");
            cars[1] = new Car("B");
            cars[2] = new Car("C");
            cars[3] = new Car("D");
            cars[4] = new Car("E");
            _enumerator = new GarageEnumerator(this.cars);
        }

        public IEnumerator<Car> GetEnumerator() => _enumerator;
       
        IEnumerator IEnumerable.GetEnumerator()=> _enumerator;
    }


    class Car(string name)
    {
        public string Name { get; } = name;
    }
}
