using System.Collections.Generic;

namespace CSharp3.Util.Hierarchies
{
    public interface IHierarchy<T> where T : IHierarchy<T>
    {
        /// <summary>
        /// Returns the collection direct descendants of this node.
        /// </summary>
        IEnumerable<T> Children { get; }
    }
}