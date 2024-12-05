namespace LearnDelegate
{
    //? what things can be defined under namespace?
    //class(record) - is a reference type
    //struct(record) - is a value type
    //enum - limited choices of options
    //delegate - represents a method signature
    //interface

    public class DelegateDemo
    {
        public List<double> Results=new List<double>();
        public double Multiply(double x, double y) => x * y;  //this method fits the delegate description

        public double Multiply3(double x, double y,int z) => x * y * z;
        public double Divide(double x, double y) => x / y; //this method fits the delegate description

        public void VoidMultiply(double x, double y) => this.Results.Add(x * y);  //this method fits the delegate description
        public void VoidDivide(double x, double y) => this.Results.Add(x / y); //this method fits the delegate description
        public double Bogus(double x) => x;
    }

    //public delegate double MathOps(double a, double b); //this delegate represents method with 2 double inputs and 1 double return
    //public delegate void VoidMathOps(double a, double b);

}
