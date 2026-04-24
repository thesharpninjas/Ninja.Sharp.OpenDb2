// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Enums;
using OpenDb2.Interfaces.Windows;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace OpenDb2.Drivers.Windows
{
    /// <summary>
    /// Windows-specific DB2 command implementation backed by <see cref="OleDbCommand"/>.
    /// </summary>
    /// <param name="command">The underlying <see cref="OleDbCommand"/> to execute.</param>
    public class WinDb2Command(OleDbCommand command) : IWinDb2Command
    {
        private readonly OleDbCommand _command = command;
        private bool _disposed;

        /// <inheritdoc />
        public void AddParam(string parameterName, object value)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.AddWithValue(parameterName, value);
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, WinDb2Type type, object value)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, (OleDbType)type).Value = value;
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, WinDb2Type type, int size, object value)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, (OleDbType)type, size).Value = value;
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, WinDb2Type type, int size, ParameterDirection direction)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, (OleDbType)type, size).Direction = direction;
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
            return new OleDbDataAdapter(_command);
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
