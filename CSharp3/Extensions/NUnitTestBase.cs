using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CSharp3.Extensions
{
    public class NUnitTestBase : AssertionHelper
    {
        protected static ConstraintExpression Is { get{return new ConstraintExpression(); }}
        protected static HasConstraints Has{get{ return new HasConstraints();}}
    }

    public class HasConstraints
    {
        public ConstraintExpression All { get { return Has.All; } }
        public ResolvableConstraintExpression Attribute(Type attributeType) { return Has.Attribute(attributeType); }
        public ResolvableConstraintExpression Count { get { return Has.Count; } }
        public ResolvableConstraintExpression InnerException { get { return Has.InnerException; } }
        public ResolvableConstraintExpression Length { get { return Has.Length; } }
        public CollectionContainsConstraint Member(object expected) { return Has.Member(expected); }
        public ResolvableConstraintExpression Message { get { return Has.Message; } }
        public ConstraintExpression No { get { return Has.No; } }
        public ResolvableConstraintExpression Property(string name) { return Has.Property(name); }
        public ConstraintExpression Some { get { return Has.Some; } }
    }
}