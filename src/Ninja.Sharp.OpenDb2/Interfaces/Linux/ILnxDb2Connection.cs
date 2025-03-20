// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Data;

namespace OpenDb2.Interfaces.Linux
{
    /// <summary>
    /// Represents a connection interface for interacting with a DB2 database on Linux systems.
    /// </summary>
    public interface ILnxDb2Connection : IDb2Connection
    {
        /// <summary>
        /// Begins a new transaction on the DB2 connection.
        /// </summary>
        /// <returns>A new instance of <see cref="ILnxDb2Transaction"/> representing the transaction.</returns>
        ILnxDb2Transaction BeginTransaction();

        /// <summary>
        /// Creates a new command with the specified command text and type.
        /// </summary>
        /// <param name="commandText">The query or stored procedure name to execute.</param>
        /// <param name="commandType">The type of command (e.g., Text, StoredProcedure, TableDirect).</param>
        /// <returns>A new instance of <see cref="ILnxDb2Command"/> configured with the specified command text and type.</returns>
        ILnxDb2Command CreateCommand(string commandText, CommandType commandType);

        /// <summary>
        /// Creates a new command with the specified command text, type, and transaction.
        /// </summary>
        /// <param name="commandText">The query or stored procedure name to execute.</param>
        /// <param name="commandType">The type of command (e.g., Text, StoredProcedure, TableDirect).</param>
        /// <param name="transaction">The transaction within which the command executes.</param>
        /// <returns>A new instance of <see cref="ILnxDb2Command"/> configured with the specified parameters.</returns>
        ILnxDb2Command CreateCommand(string commandText, CommandType commandType, IDb2Transaction transaction);
    }
}