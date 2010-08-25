using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace CSharp3._070_LambdaExpressions
{
    [TestFixture]
    public class ExpressionTrees
    {
        [Test]
        public void LambdasMayBeCompiledToExpressionTrees()
        {
            Expression<Func<int, int>> doubleExpression = param => param*2;

            Assert.That(doubleExpression.Parameters.Single().Type,
                        Is.EqualTo(typeof (Int32)));

            Assert.That(doubleExpression.Body.NodeType,
                        Is.EqualTo(ExpressionType.Multiply));

            Assert.That(doubleExpression.Body.Type,
                        Is.EqualTo(typeof (Int32)));

            Assert.That(doubleExpression.ToString(),
                        Is.EqualTo("param => (param * 2)"));
        }

        [Test]
        public void CanBeExaminedAtRuntime()
        {
            Expression<Func<int, int>> doubleExpression = param => param*2;

            Assert.That(doubleExpression.Parameters.Single().Type,
                        Is.EqualTo(typeof (Int32)));

            Assert.That(doubleExpression.Body.NodeType,
                        Is.EqualTo(ExpressionType.Multiply));

            Assert.That(doubleExpression.Body.Type,
                        Is.EqualTo(typeof (Int32)));

            Assert.That(doubleExpression.ToString(),
                        Is.EqualTo("param => (param * 2)"));
        }

        [Test]
        public void CanBeCompiledAndExecuted()
        {
            Expression<Func<int, int>> doubleExpression = param => param*2;
            var four = doubleExpression.Compile()(2);
            Assert.That(four, Is.EqualTo(4));

            four = doubleExpression.Compile().Invoke(2);

            Assert.That(four, Is.EqualTo(4));
        }

        //Here we are! Utvecklarforum 1
    
        //Queryable....
    }
}