
namespace Part6
{
    [Serializable]
    internal class MyException : Exception
    {
        private DivideByZeroException ex;

        public MyException()
        {
        }

        public MyException(DivideByZeroException ex):this("Demo",ex)
        {
            
        }

        public MyException(string? message) : base(message)
        {
        }

        public MyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}