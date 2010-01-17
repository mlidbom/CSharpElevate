using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using CSharp3._050_ExtensionMethods;

namespace CSharp3._080_Linq
{
    [TestFixture]
    public class _20_QueryableDemo
    {
        [Test]
        public void UsesExpressionsNotDelegates()
        {
            Expression<Func<int, bool>> isGreaterThan5Expression = parm => parm > 5;
            Func<int, bool> isGreaterThan5Delegate = parm => parm > 5;

            //You can create an IQueryable<T> from an IEnumerable<T>
            IQueryable<int> oneThroughTenQueryable = 1.Through(10).AsQueryable();
            
            //IQueryable<T> implements IEnumerable<T>
            IEnumerable<int> oneThroughTenQueryableTypedAsEnumerable = oneThroughTenQueryable;
            Assert.That(oneThroughTenQueryableTypedAsEnumerable, Is.InstanceOf(typeof(IQueryable<int>)));

            //IQueryable<T> Can be statically retyped as IEnumerable<T>...
            IEnumerable<int> oneThroughTenEnumerable = oneThroughTenQueryable.AsEnumerable();
            Assert.That(oneThroughTenEnumerable, Is.InstanceOf(typeof(IQueryable<int>)));

            //Has it's own extension methods that work with Expression<T> rather than delegates
            IQueryable<int> greaterThan5Queryable = oneThroughTenQueryable.Where(isGreaterThan5Expression);

            IEnumerable<int> greaterThan5Enumerable = oneThroughTenQueryableTypedAsEnumerable.Where(isGreaterThan5Delegate);

            Assert.That(greaterThan5Enumerable, Is.EqualTo(6.Through(10)));
            Assert.That(greaterThan5Queryable, Is.EqualTo(6.Through(10)));

        }
    }
}