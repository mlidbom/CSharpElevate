using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace CSharp3._070_LambdaExpressions
{
    [TestFixture]
    public class Intro
    {
        private readonly Stack<object> log = new Stack<object>();

        private void Log(object message)
        {
            log.Push(message);
        }

        [Test]
        public void OneReasonWhyYouWantLambdas()
        {
            //Yuck! Five lines of code including the Named method declaration just to call one method.
            WaitCallback printMessageFromNamedMethod = new WaitCallback(Log);
            ThreadPool.QueueUserWorkItem(printMessageFromNamedMethod, "Hi there from typed named delegate");

            //Painful, but at least I don't have to define a named method.
            WaitCallback printMessage = new WaitCallback(delegate(object message) { log.Push(message); });
            ThreadPool.QueueUserWorkItem(printMessage, "Hi there from typed delegate");

            //Still more noise than signal
            ThreadPool.QueueUserWorkItem(delegate(object message) { log.Push(message); }, "Hi there from anonymous delegate");

            //Not bad. Lambdas are nice.
            ThreadPool.QueueUserWorkItem(message => log.Push(message), "Hi there from lambda");

            //Now we're talking! Use method groups whenever possible to make your code more readable.
            ThreadPool.QueueUserWorkItem(log.Push, "Hi there from method group");

            Thread.Sleep(100);

            foreach (var message in log)
            {
                Console.WriteLine(message);
            }
        }
    }
}