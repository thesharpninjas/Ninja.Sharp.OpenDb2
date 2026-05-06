// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Data;

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
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Open(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously closes the DB2 database connection.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Close();

        /// <summary>
        /// Begins a new transaction on the DB2 connection.
        /// </summary>
        /// <returns>An instance of <see cref="IDb2Transaction"/> representing the new transaction.</returns>
        IDb2Transaction BeginTransaction();

        /// <summary>
        /// Creates a new command to be executed against the DB2 database.
        /// </summary>
        /// <param name="commandText">The query or stored procedure to execute.</param>
        /// <param name="commandType">The type of command (e.g., Text, StoredProcedure).</param>
        /// <returns>An instance of <see cref="IDb2Command"/> representing the new command.</returns>
        IDb2Command CreateCommand(string commandText, CommandType commandType);

        /// <summary>
        /// Creates a new command to be executed against the DB2 database within the context of a transaction.
        /// </summary>
        /// <param name="commandText">The query or stored procedure to execute.</param>
        /// <param name="commandType">The type of command (e.g., Text, StoredProcedure).</param>
        /// <param name="transaction">The transaction within which the command should execute.</param>
        /// <returns>An instance of <see cref="IDb2Command"/> representing the new command.</returns>
        IDb2Command CreateCommand(string commandText, CommandType commandType, IDb2Transaction transaction);
    }
}