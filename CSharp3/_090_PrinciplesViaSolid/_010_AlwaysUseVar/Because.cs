using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Void.Linq;

namespace CSharp3._090_PrinciplesViaSolid._010_AlwaysUseVar
{
    /// <summary>
    /// When you use var rather than explicit types 
    /// the good design principle of using as narrow
    /// an abstraction as possible becomes automatic.
    /// 
    /// By narrow I'm referring to your knowledge of 
    /// the type in question.  The less you know, 
    /// the better for your code.
    /// 
    /// If you only use the part of a variable corresponding
    /// to an interface then if the type of the variable
    /// changes your code will still work without any changes.
    /// This is a powerful concept.
    /// </summary>
    [TestFixture]
    public class Because
    {
        [Test]
        public void VarMinimizesCoupling()
        {
            //The most specifically typed definition of getPrintObjects.
            //Such specific typing should only be used if you 
            //really guarantee what using the List<int> type 
            //implies:
            //1. your method guarantees ordering as list does.
            //2. Your method returns a List<int> that the callee
            //is perfectly free to modify in any way.
            //3. You will never change the exact type
            //of the returned object. It is List<int>. Forever.
            //
            //If you do not feel confident that you can uphold
            //at least these guarantees do not use such a specific type, 
            //because changing the type later is very likely to 
            //break user code!
            //
            //This is one specific application of the first 
            //of the SOLID principles: The Single responsibility 
            //principle. The responsibility of the getPrintObjects
            //method is to return something that can be enumerated 
            //and printed. If it returns something that can do things
            //completely unrelated to enumerating and printing you 
            //have broken the principle and its apt to cost you.
            //
            Func<List<int>> getPrintObjects = () => new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};

            //Using this implementation breaks myIntList
            //Func<IList<int>> getPrintObjects = () => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Using this implementation breaks myIntList and myIntListInterface
            //Func<IEnumerable<int>> getPrintObjects = () => Enumerable.Range(1,9);

            //This is an implementation that fulfills SRP.
            //Using this implementation breaks myIntList, myIntListInterface and myIntEnumerable
            //Func<IEnumerable<object>> getPrintObjects = () => Enumerable.Range(1,9).Cast<object>();                                  

            //Using this implementation breaks myIntList, myIntListInterface, myIntEnumerable and myUntypedEnumerable
            //Func<ForEachable> getPrintObjects = () => new ForEachable(Enumerable.Range(1,9));   


            //4 of the methods above breaks this
            List<int> myIntList = getPrintObjects();

            //3 of the methods above breaks this
            IList<int> myIntListInterface = getPrintObjects();

            //2 of the methods above breaks this
            IEnumerable<int> myIntEnumerable = getPrintObjects();

            //1 of the definitions above breaks this
            IEnumerable<object> myUntypedEnumerable = getPrintObjects().Cast<object>();

            //As long as you get a type from getPrintObjects
            //that supports calling ForeEach on 
            //This will NEVER BREAK.
            var myObjects = getPrintObjects();

            myIntList.ForEach(Console.WriteLine);
            myIntListInterface.ForEach(Console.WriteLine);
            myIntEnumerable.ForEach(Console.WriteLine);
            myUntypedEnumerable.ForEach(Console.WriteLine);
            myObjects.ForEach(Console.WriteLine);          
        }


        private class ForEachable
        {
            private IEnumerable data;

            public ForEachable(IEnumerable data)
            {
                this.data = data;
            }

            public void ForEach(Action<object> action)
            {
                foreach (var instance in data)
                {
                    action(instance);
                }
            }
        }
    }
}