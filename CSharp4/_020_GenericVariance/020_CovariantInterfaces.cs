using CSharp4._060_Examples.Decorator;
using NUnit.Framework;

namespace CSharp4._020_GenericVariance
{
    class Decorator<T> : IDecorator<T>
    {
        public T Decorated { get; private set; }
    }     

    [TestFixture]
    public class CreatedCovariantInterfaces
    {
        [Test]
        public void EnumeratingStringsIsEnumeratingObjects()
        {
            IDecorator<string> decoratedString = new Decorator<string>();
            IDecorator<object> decoratedObject = decoratedString;
        }        
    }
}