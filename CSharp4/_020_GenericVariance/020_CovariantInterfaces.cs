using System;
using NUnit.Framework;

namespace CSharp4._020_Variance
{
        interface IDecorator<out T>
        {
            T Decorated{ get;}    
        }

        class Decorator<T> : IDecorator<T>
        {
            public T Decorated { get; private set; }
            public Decorator(T decorated)
            {
                Decorated = decorated;
            }
        }     

    [TestFixture]
    public class CreatedCovariantInterfaces
    {
        [Test]
        public void EnumeratingStringsIsEnumeratingObjects()
        {
            IDecorator<string> decoratedString = new Decorator<string>("oeu");
            IDecorator<object> decoratedObject = decoratedString;
        }        
    }
}