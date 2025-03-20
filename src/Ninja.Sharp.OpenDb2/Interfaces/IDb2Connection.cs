// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

namespace OpenDb2.Interfaces
{
    /// <summary>
    /// Represents a connection to a DB2 database.
    /// </summary>
    public interface IDb2Connection : IDisposable
    {
        /// <summary>
        /// Asynchronously opens the DB2 database connection.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Open();

        /// <summary>
        /// Asynchronously closes the DB2 database connection.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Close();
    }
}