using NUnit.Framework;

namespace CSharp3._020_AutoImplementedProperties
{
    [TestFixture]
    public class AutoImplementedProperties
    {
        private string Private { get; set; }
        protected string MixedModifiers { get; private set; }

        [Test]
        public void WorkExaclyAsNormalProperties()
        {
            Private = "hi";
            Assert.That(Private,
                        Is.EqualTo("hi"));
        }
    }
}