using NUnit.Framework;

namespace CSharp4._030_Parameters
{
    [TestFixture]
    public class _020_Optional
    {
        //Helps reduce duplication. 
        //No more do you need 5 methods or constructors that call 
        //one another just to simplify common usages
        private class Configuration
        {
            public Configuration(
                string value1 = "default",
                string value2 = "default",
                string value3 = "default",
                string value4 = "default",
                string value5 = "default",
                string value6 = "default",
                string value7 = "default",
                string value8 = "default",
                string value9 = "default")
            {
            }
        }

        [Test]
        public void ShouldName()
        {
            Configuration configWithValue4And9 =
                new Configuration(value9: "SomeValue",
                                  value4: "SomeOtherValue");
        }
    }
}