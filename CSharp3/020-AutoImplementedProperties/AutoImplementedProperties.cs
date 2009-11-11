using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CSharp3._030_AutoProperties
{
    [TestFixture]
    public class AutoImplementedProperties
    {
        string Private { get; set; }
        protected virtual string Protected { get; set; }
        protected virtual string MixedModifiers { get; private set; }


    }
}
