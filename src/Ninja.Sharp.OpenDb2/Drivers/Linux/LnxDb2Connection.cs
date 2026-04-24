// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using IBM.Data.Db2;
using OpenDb2.Interfaces;
using OpenDb2.Interfaces.Linux;
using System.Data;

namespace OpenDb2.Drivers.Linux
{
    /// <summary>
    /// Linux and macOS DB2 connection implementation backed by <see cref="DB2Connection"/> (IBM.Data.Db2).
    /// Registered as a scoped service via <c>AddLnxDb2Services</c> or <c>AddDb2Services</c> on Linux/macOS.
    /// </summary>
    /// <param name="connectionString">The IBM DB2 connection string used to connect to the DB2 instance.</param>
    public class LnxDb2Connection(string connectionString) : ILnxDb2Connection
    {
        private readonly DB2Connection _connection = new(connectionString);
        private bool _disposed;

        /// <inheritdoc />
        public async Task Open()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            await _connection.OpenAsync();
        }

        /// <inheritdoc />
        public async Task Close()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            await _connection.CloseAsync();
        }

        /// <inheritdoc />
        public ILnxDb2Transaction BeginTransaction()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return new LnxDb2Transaction(_connection.BeginTransaction());
        }

        /// <inheritdoc />
        public ILnxDb2Command CreateCommand(string commandText, CommandType commandType)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            var command = _connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;

            return new LnxDb2Command(command);
        }

        /// <inheritdoc />
        public ILnxDb2Command CreateCommand(string commandText, CommandType commandType, IDb2Transaction transaction)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            var command = _connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = transaction.Transaction as DB2Transaction
                ?? throw new InvalidOperationException($"Expected a DB2Transaction but got {transaction.Transaction.GetType().Name}.");

            return new LnxDb2Command(command);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _connection.Dispose();
        }
    }
}
