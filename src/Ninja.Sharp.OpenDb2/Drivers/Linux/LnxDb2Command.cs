// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using IBM.Data.Db2;
using OpenDb2.Interfaces.Linux;
using OpenDb2.Enums;
using System.Data;
using System.Data.Common;

namespace OpenDb2.Drivers.Linux
{
    /// <summary>
    /// Linux and macOS DB2 command implementation backed by <see cref="DB2Command"/> (IBM.Data.Db2).
    /// </summary>
    /// <param name="command">The underlying <see cref="DB2Command"/> to execute.</param>
    public class LnxDb2Command(DB2Command command) : ILnxDb2Command
    {
        private readonly DB2Command _command = command;
        private bool _disposed;

        /// <inheritdoc />
        public void AddParam(string parameterName, object value)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, value);
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, LnxDb2Type type, object value)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, (DB2Type)type).Value = value;
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, LnxDb2Type type, int size, object value)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, (DB2Type)type, size).Value = value;
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, LnxDb2Type type, int size, ParameterDirection direction)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, (DB2Type)type, size).Direction = direction;
        }

        /// <inheritdoc />
        public object ReadParam(string parameterName)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _command.Parameters[parameterName].Value;
        }

        /// <inheritdoc />
        public Task<int> ExecuteNonQuery()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _command.ExecuteNonQueryAsync();
        }

        /// <inheritdoc />
        public Task<DbDataReader> ExecuteReader()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _command.ExecuteReaderAsync();
        }

        /// <inheritdoc />
        public DbDataAdapter CreateDataAdapter()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return new DB2DataAdapter(_command);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _command.Dispose();
        }
    }
}
