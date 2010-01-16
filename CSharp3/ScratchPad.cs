using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace CSharp3
{
    [TestFixture]
    public class ScratchPad
    {
        [Test]
        public void DontBreakPresentation()
        {
            //class and collection initializers
            var magnus = new Person {Name = "Magnus Lidbom"};
            IEnumerable<string> names = new[] {"Calle", "Oscar"};

            //extension methods
            var first = names.First();

            //inf
            var alsoNames = names;

            //Anonymous types
            var address = new {Street = "AStreet", Zip = "111111"};

            //lambda
            var upperCaseNames = names.Select(name => name.ToUpper());

            //expr
            Expression<Func<string, bool>> isUpperCased = test => test == test.ToUpper();

            //comprehension syntax
            var lowerCaseNames = from name in names
                                 select name.ToLower();
        }

        private static IEnumerable<string> GetNames()
        {
            return new[] {"Calle", "Oscar"};
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}