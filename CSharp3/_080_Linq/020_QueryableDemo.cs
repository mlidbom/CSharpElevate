using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CSharp3._050_ExtensionMethods;
using NUnit.Framework;

namespace CSharp3._080_Linq
{
    [TestFixture]
    public class _20_QueryableDemo
    {
        [Test]
        public void UsesExpressionsNotDelegates()
        {            
            //wrap an IEnumerable<T>
            var oneThroughTenQueryable = 1.Through(10).AsQueryable();

            //extensions work with Expression<T>
            Expression<Func<int, bool>> isGreaterThan5Expression = parm => parm > 5;
            var greaterThan5Queryable = oneThroughTenQueryable.Where(isGreaterThan5Expression);
            
            //Linq to NHibernate, Entities, SQL .....

            //IQueryable<T> implements IEnumerable<T>
            IEnumerable<int> oneThroughTenEnumerable = oneThroughTenQueryable;

            //IQueryable<T> Can be inline retyped as IEnumerable<T>...
            oneThroughTenEnumerable = oneThroughTenQueryable.AsEnumerable();            
        }
    }
}