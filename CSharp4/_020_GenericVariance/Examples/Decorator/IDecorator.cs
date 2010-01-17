namespace CSharp4._020_GenericVariance.Examples.Decorator
{
    public interface IDecorator<out T>
    {
        T Decorated{ get;}    
    }
}