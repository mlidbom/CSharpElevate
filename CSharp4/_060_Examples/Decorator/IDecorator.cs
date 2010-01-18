namespace CSharp4._060_Examples.Decorator
{
    public interface IDecorator<out T>
    {
        T Decorated{ get;}    
    }
}