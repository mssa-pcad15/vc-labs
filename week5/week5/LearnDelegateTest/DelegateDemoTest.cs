using LearnDelegate; 
namespace LearnDelegateTest
{
    [TestClass]
    public class DelegateDemoTest
    {
        [TestMethod]
        public void InstantiateADelegate()
        {
            DelegateDemo d = new DelegateDemo(); // d is a variable of class, so it points to an instance

            Func<double,double,double> delegateInstance = d.Multiply; // delegateInstance is a variable of delegate so it points to a method

            Assert.IsTrue(delegateInstance.Method.Name == "Multiply"); //delegate.Method represents the runtime method this instance points to.
            Assert.IsTrue(delegateInstance.Method.GetParameters().Length == 2);
        }

        [TestMethod]
        public void InstantiateAGenericDelegate()
        {
            DelegateDemo d = new DelegateDemo(); // d is a variable of class, so it points to an instance

            Func<double, double, int,double> delegateInstance = d.Multiply3; // delegateInstance is a variable of delegate so it points to a method

            Assert.IsTrue(delegateInstance.Method.Name == nameof(d.Multiply3)); //delegate.Method represents the runtime method this instance points to.
            Assert.IsTrue(delegateInstance.Method.GetParameters().Length == 3);
        }
        [TestMethod]
        public void InstantiateAGenericDelegateAndInvoke()
        {
            DelegateDemo d = new DelegateDemo(); // d is a variable of class, so it points to an instance

            Func<double, double, int, double> delegateInstance = d.Multiply3; // delegateInstance is a variable of delegate so it points to a method

            double result = delegateInstance.Invoke(5, 6,10);
            double expected = 300;
            Assert.AreEqual(result, expected);
        }


        [TestMethod]
        public void InstantiateADelegateAndInvoke()
        {
            DelegateDemo d = new DelegateDemo(); // d is a variable of class, so it points to an instance

            Func<double, double, double> delegateInstance = d.Multiply; // delegateInstance is a variable of delegate so it points to a method

            double result = delegateInstance.Invoke(5, 6);
            double expected = 30;
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void PassADelegateAsArgumentToAMethod() {
            DelegateDemo d = new DelegateDemo(); // d is a variable of class, so it points to an instance

            Func<double, double, double> delegateInstance = d.Multiply; // delegateInstance is a variable of delegate so it points to a method

            double result = InvokeThis(delegateInstance); // we have passed data into a method plenty of times, now lets see how we pass instruction
            // into method
            double expected = 30;
            Assert.AreEqual(result, expected);
        }

        public double InvokeThis(Func<double, double, double> instance) // this method invokes the parameter delegate MathOps without knowing the implementing method
        {
            return instance.Invoke(5, 6);
        }
        public void InvokeThis(Action<double,double> instance) // this method invokes the parameter delegate MathOps without knowing the implementing method
        {
            instance.Invoke(5, 6);
        }

        [TestMethod]
        public void DelegateVariableSupportsMulticasting()
        {
            DelegateDemo d = new DelegateDemo(); // d is a variable of class, so it points to an instance

            Func<double, double, double> delegateInstance = d.Multiply; // delegateInstance is a variable of delegate so it points to a method
            delegateInstance += d.Divide;

            double result = InvokeThis(delegateInstance); // we have passed data into a method plenty of times, now lets see how we pass instruction
                                                          // into method

            Assert.AreEqual(result, 5.0 / 6);
            Assert.IsTrue(delegateInstance.GetInvocationList().Length == 2);
            Assert.IsTrue(delegateInstance.GetInvocationList()[0].Method.Name == nameof(d.Multiply));
            Assert.IsTrue(delegateInstance.GetInvocationList()[1].Method.Name == nameof(d.Divide));
        }

        [TestMethod]
        public void MulticastDelegateShouldUseVoidReturnAndAccessResultsAsStateElsewhere()
        {
            DelegateDemo d = new DelegateDemo(); // d is a variable of class, so it points to an instance

            Action<double, double> delegateInstance = d.VoidMultiply; // delegateInstance is a variable of delegate so it points to a method
            delegateInstance += d.VoidDivide;
            

            InvokeThis(delegateInstance); // we have passed data into a method plenty of times, now lets see how we pass instruction
                                                          // into method

            Assert.IsTrue(d.Results.Contains(5.0 / 6));
            Assert.IsTrue(d.Results.Contains(5 * 6));
            Assert.IsTrue(delegateInstance.GetInvocationList().Length == 2);
            Assert.IsTrue(delegateInstance.GetInvocationList()[0].Method.Name == nameof(d.VoidMultiply));
            Assert.IsTrue(delegateInstance.GetInvocationList()[1].Method.Name == nameof(d.VoidDivide));
        }

    }
}