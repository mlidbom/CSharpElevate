using NUnit.Framework;

namespace CSharp4._020_GenericVariance
{
    internal interface ILogger<in T>
    {
        void Log(T logValue);
    }

    internal class Logger<T> : ILogger<T>
    {
        public void Log(T logValue)
        {}
    }

    [TestFixture]
    public class ContraVariantInterfaces
    {
        [Test]
        public void IfYouCanLogAnyObjectYouCanLogAString()
        {
            ILogger<string> objectLogger = new Logger<object>();
        }
    }
}