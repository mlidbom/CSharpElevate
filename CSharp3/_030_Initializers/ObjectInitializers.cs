using NUnit.Framework;

namespace CSharp3._030_Initializers
{
    [TestFixture]
    public class ObjectInitializers
    {
        private class Person
        {
            public string ForeName { get; set; }
            public string SurName { get; set; }
            public Person Father{ get; set;}
        }

        [Test]
        public void EnablesEasyAndMoreReadableObjectSetup()
        {
            Person me = new Person
                        {
                            ForeName = "Magnus",
                            SurName = "Lidbom",
                            Father = new Person
                                     {
                                         ForeName = "Lars",
                                         SurName = "Lidbom"
                                     }
                        };
        }
    }
}