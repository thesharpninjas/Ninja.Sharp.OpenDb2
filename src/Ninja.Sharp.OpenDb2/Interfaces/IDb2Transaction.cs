// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Data;

namespace OpenDb2.Interfaces
{
    /// <summary>
    /// Represents a DB2 database transaction.
    /// </summary>
    public interface IDb2Transaction : IDisposable
    {
        /// <summary>
        /// Commits the current transaction, making all changes permanent.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back the current transaction, undoing all changes.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Gets the underlying database transaction object.
        /// </summary>
        IDbTransaction Transaction { get; }
    }
}