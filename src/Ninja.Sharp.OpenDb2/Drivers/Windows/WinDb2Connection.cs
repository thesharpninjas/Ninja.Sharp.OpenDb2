// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Interfaces;
using OpenDb2.Interfaces.Windows;
using System.Data;
using System.Data.OleDb;

namespace OpenDb2.Drivers.Windows
{
    /// <summary>
    /// Windows-specific DB2 connection implementation backed by <see cref="OleDbConnection"/>.
    /// Registered as a scoped service via <c>AddWinDb2Services</c> or <c>AddDb2Services</c> on Windows.
    /// </summary>
    /// <param name="connectionString">The OleDb connection string used to connect to the DB2 instance.</param>
    public class WinDb2Connection(string connectionString) : IWinDb2Connection
    {
        private readonly OleDbConnection _connection = new(connectionString);
        private bool _disposed;

        /// <inheritdoc />
        public async Task Open(CancellationToken cancellationToken = default)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            await _connection.OpenAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task Close()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            await _connection.CloseAsync();
        }

        /// <inheritdoc />
        public IWinDb2Transaction BeginTransaction()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return new WinDb2Transaction(_connection.BeginTransaction());
        }

        /// <inheritdoc />
        public IWinDb2Command CreateCommand(string commandText, CommandType commandType)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            var command = _connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;

            return new WinDb2Command(command);
        }

        /// <inheritdoc />
        public IWinDb2Command CreateCommand(string commandText, CommandType commandType, IDb2Transaction transaction)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            var command = _connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = transaction.Transaction as OleDbTransaction
                ?? throw new InvalidOperationException($"Expected an OleDbTransaction but got {transaction.Transaction.GetType().Name}.");

            return new WinDb2Command(command);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
