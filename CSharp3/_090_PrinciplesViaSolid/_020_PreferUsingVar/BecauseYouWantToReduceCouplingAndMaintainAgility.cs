using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using NUnit.Framework;

namespace CSharp3._090_PrinciplesViaSolid._020_PreferUsingVar
{
    [TestFixture]
    public class BecauseYouWantToReduceCoupling
    {       
        [Test]
        public void And_Var_Is_ISP_For_Free()
        {
            List<int> integers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };            
            
            //Before using List<T> as a property or return type, ask yourself: 
            //Can I guarantee that:
            //1. My method orders the returnvalue consistently.
            //2. It's safe for the caller to modify the collection in any way.
            //3. The return type will never need to change.
            //4. The clients of my method NEED these guarantees.
            
            Func<List<int>> intListFetcher = () => integers;
            Func<IList<int>> intIListFetcher = () => integers;
            Func<IEnumerable<int>> intEnumerableFetcher = () => integers;
            Func<IEnumerable<object>> objectEnumerableFetcher = () => integers.Cast<object>();                                  
            
            //Note the type
            Func<Iterator> iteratorFetcher = () => new Iterator(integers);

            var printObjectFetcher =
                intListFetcher; 
            //intIListFetcher;          //breaks myIntList
            //intEnumerableFetcher;     //breaks myIntList and myIntIList
            //objectEnumerableFetcher;  //breaks myIntList, myIntIList and myIntEnumerable
            //iteratorFetcher;          //myIntList, myIntListInterface, myIntIList and myObjectEnumerable


            List<int> myIntList = printObjectFetcher();//broken 4 times            
            IList<int> myIntIList = printObjectFetcher();//broken 3 times            
            IEnumerable<int> myIntEnumerable = printObjectFetcher();//broken 2 times            
            IEnumerable<object> myObjectEnumerable = printObjectFetcher().Cast<object>();//broken 1 time

            var myObjects = printObjectFetcher();//Never broken!
            
            //The number of breakages are directly relative to the "broadness" of the declared type            

            //Var achieves ISP without changing the code you depend on

            myIntList.ForEach(Console.WriteLine);
            myIntIList.ForEach(Console.WriteLine);
            myIntEnumerable.ForEach(Console.WriteLine);
            myObjectEnumerable.ForEach(Console.WriteLine);
            myObjects.ForEach(Console.WriteLine);          
        }


        private class Iterator
        {
            private readonly IEnumerable data;

            public Iterator(IEnumerable data)
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