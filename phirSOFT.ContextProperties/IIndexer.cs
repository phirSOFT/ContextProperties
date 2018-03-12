using System;
using System.Collections.Generic;
using System.Text;

namespace phirSOFT.ContextProperties
{
    /// <summary>
    /// Redirects an index operation
    /// </summary>
    /// <typeparam name="TKey">The key of the index</typeparam>
    /// <typeparam name="TValue">The value of the index</typeparam>
    public interface IIndexer<in TKey, out TValue>
    {
        /// <summary>
        /// Gets the value associated with a given key.
        /// </summary>
        /// <param name="key">The key of the value to retrive.</param>
        /// <returns>The value associated with the key.</returns>
        TValue this[TKey key] { get; }
    }
}
