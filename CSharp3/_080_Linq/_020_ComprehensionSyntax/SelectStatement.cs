using System;
using System.Linq;
using NUnit.Framework;
using Void.Linq;

namespace CSharp3._080_Linq._020_ComprehensionSyntax
{
    [TestFixture]
    public class SelectStatement
    {
        [Test]
        public void SelectTransformsData()
        {
            var result = from i in 1.Through(10) 
                         select new decimal(i);

            result.ForEach(Console.WriteLine);
        }
    }
}