// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Interfaces;
using OpenDb2.Interfaces.Windows;
using System.Data;
using System.Data.OleDb;

namespace OpenDb2.Drivers.Windows
{
    public class WinDb2Connection(string connectionString) : IWinDb2Connection
    {
        private readonly OleDbConnection _connection = new(connectionString);

        /// <inheritdoc />
        public async Task Open() => await _connection.OpenAsync();

        /// <inheritdoc />
        public async Task Close() => await _connection.CloseAsync();

        /// <inheritdoc />
        public IWinDb2Transaction BeginTransaction() => new WinDb2Transaction(_connection.BeginTransaction());

        /// <inheritdoc />
        public IWinDb2Command CreateCommand(string commandText, CommandType commandType)
        {
            var command = _connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;

            return new WinDb2Command(command);
        }

        /// <inheritdoc />
        public IWinDb2Command CreateCommand(string commandText, CommandType commandType, IDb2Transaction transaction)
        {
            var command = _connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = (OleDbTransaction)transaction.Transaction;

            return new WinDb2Command(command);
        }

        public void Dispose() => _connection.Dispose();
    }
}
