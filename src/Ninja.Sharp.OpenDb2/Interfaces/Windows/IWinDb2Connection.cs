// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Data;

namespace OpenDb2.Interfaces.Windows
{
    /// <summary>
    /// Represents a connection interface for interacting with a DB2 database on Windows systems.
    /// </summary>
    public interface IWinDb2Connection : IDb2Connection
    {
        /// <summary>
        /// Begins a new transaction on the DB2 connection.
        /// </summary>
        /// <returns>An instance of <see cref="IWinDb2Transaction"/> representing the new transaction.</returns>
        IWinDb2Transaction BeginTransaction();

        /// <summary>
        /// Creates a new command to be executed against the DB2 database.
        /// </summary>
        /// <param name="commandText">The query or stored procedure to execute.</param>
        /// <param name="commandType">The type of command (e.g., Text, StoredProcedure).</param>
        /// <returns>An instance of <see cref="IWinDb2Command"/> representing the new command.</returns>
        IWinDb2Command CreateCommand(string commandText, CommandType commandType);

        /// <summary>
        /// Creates a new command to be executed against the DB2 database within the context of a transaction.
        /// </summary>
        /// <param name="commandText">The query or stored procedure to execute.</param>
        /// <param name="commandType">The type of command (e.g., Text, StoredProcedure).</param>
        /// <param name="transaction">The transaction within which the command should execute.</param>
        /// <returns>An instance of <see cref="IWinDb2Command"/> representing the new command.</returns>
        IWinDb2Command CreateCommand(string commandText, CommandType commandType, IDb2Transaction transaction);
    }
}
