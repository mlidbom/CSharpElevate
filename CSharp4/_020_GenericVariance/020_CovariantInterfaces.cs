using NUnit.Framework;

namespace CSharp4._020_GenericVariance
{
    public interface IDecorator<out T>
    {
        T Decorated{ get;}    
    }

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