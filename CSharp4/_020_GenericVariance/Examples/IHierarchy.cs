using System.Collections.Generic;

namespace CSharp4._020_GenericVariance.Examples
{
    public interface IHierarchy<T> where T : IHierarchy<T>
    {
        /// <summary>
        /// Returns the collection direct descendants of this node.
        /// </summary>
        IEnumerable<T> Children { get; }
    }
}