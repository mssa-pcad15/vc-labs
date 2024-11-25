using Fundamentals.LearnTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalsTest
{
    [TestClass]
    public class LearnClassTest
    {
        [TestMethod]
        public void InstanceMemberExistsForEachInstance() { 
        
            //Arrange
        LearnClass obj1 = new LearnClass(DateTime.Now);
        LearnClass obj2 = new LearnClass(DateTime.Now);
        
            //Act
        obj1._instanceMember = 200;
        obj2._instanceMember = 300;
        
            //Assert
        Assert.IsTrue(obj1._instanceMember != obj2._instanceMember);
        Assert.AreNotSame(obj1,obj2);

        }

        [TestMethod]
        public void EachInstanceHasTheirOwnData() {
            LearnClass victor = new LearnClass(new DateTime(1990, 5, 5), "password");
            LearnClass bob = new LearnClass(new DateTime(1970, 3, 5), "password2");
            LearnClass tom = new LearnClass(new DateTime(2012, 3, 5), "password3");

            LearnClass anotherVictor = victor;            
            Assert.AreNotSame(victor,bob);
            Assert.AreNotSame(bob,tom);
            Assert.AreSame(anotherVictor, victor);
        }

        [TestMethod]
        public void ShowCorrectAgeGivenADob()
        {
            LearnClass victor = new LearnClass(new DateTime(1990, 5, 5), "password");
            int actual = victor.Age;
            int expected = 34;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ShowPasswordCanChange()
        {
            LearnClass victor = new LearnClass(new DateTime(1990, 5, 5), "password");
            victor.Password = "newPassword";
            Assert.IsTrue(victor.VerifyPassword("newPassword"));
        }
        [TestMethod]
        public void ShowCorrectChineseAgeGivenADob()
        {
            LearnClass victor = new LearnClass(new DateTime(1990, 5, 5), "password");
            int actual = victor.ChineseAge;
            int expected = 35;
            Assert.AreEqual(expected, actual);
        }

    }
}
