using NUnit.Framework;

namespace CSharp3._015_PartialMethods
{
    /// <summary>
    /// Partial methods are primarily used as extension points in generated code.
    /// </summary>
    [TestFixture]
    public partial class PartialMethods
    {
        partial void After();
        partial void Before(); //removed by the compiler.

        [Test]
        public void WillWorkEvenThoughOneMethodCalledDoesNotExist()
        {
            Before(); //removed by the compiler.
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