using System.Collections.Generic;
using NUnit.Framework;

namespace CSharp3._030_Initializers
{
    [TestFixture]
    public class Initializers
    {
        private class Person
        {
            public string ForeName { get; set; }
            public string SurName { get; set; }
            public Person Father { get; set; }
        }

        [Test]
        public void ObjectInitialization()
        {
            var me = new Person
                     {
                         ForeName = "Magnus", SurName = "Lidbom",
                         Father = new Person
                                  {
                                      ForeName = "Lars", SurName = "Lidbom",
                                      Father = new Person
                                               {
                                                   ForeName = "Tage", SurName = "Lidbom"
                                               }
                                  }
                     };
        }

        [Test]
        public void Arrayinitialization()
        {
            var arr = new int[] {1, 2, 3, 4};
            var youCanOmmitTheType = new[] {1, 2, 3, 4};
        }

        [Test]
        public void CollectionInitialization()
        {
            var ints = new List<List<int>>
                       {
                           new List<int> {1, 2, 3, 4, 5},
                           new List<int> {1, 2, 3, 4, 5}
                       };

            var people = new List<Person>
                         {
                             new Person {ForeName = "1"},
                             new Person {ForeName = "2"}
                         };
        }

        [Test]
        public void DictionaryInitialization()
        {
            var squares = new Dictionary<string, int>
                          {
                              {"1", 1},
                              {"2", 4},
                              {"4", 8}
                          };
        }
    }
}