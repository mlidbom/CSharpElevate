using NUnit.Framework;

namespace CSharp3._015_PartialMethods
{
    /// <summary>
    /// Partial methods are primarily used as extension points in generated code.
    /// </summary>
    [TestFixture]
    partial class PartialMethods
    {
        partial void After();
        partial void Before(); //A partial method with no definition will be removed by the compiler.

        [Test]
        public void WillWorkEvenThoughOneMethodCalledDoesNotExist()
        {
            Before();//Calls to a partial method with no definition will be removed by the compiler.
            //Dostuff
            After();
        }
    }

    partial class PartialMethods
    {
        partial void After()
        {
            
        }
    }
}