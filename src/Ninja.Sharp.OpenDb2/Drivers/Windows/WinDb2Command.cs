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
            _command.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, WinDb2Type type, object value)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, (OleDbType)type).Value = value ?? DBNull.Value;
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, WinDb2Type type, int size, object value)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, (OleDbType)type, size).Value = value ?? DBNull.Value;
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, WinDb2Type type, int size, ParameterDirection direction)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _command.Parameters.Add(parameterName, (OleDbType)type, size).Direction = direction;
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, Db2Type type, object value)
        {
            AddParam(parameterName, Db2TypeMapper.ToWindows(type), value);
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, Db2Type type, int size, object value)
        {
            AddParam(parameterName, Db2TypeMapper.ToWindows(type), size, value);
        }

        /// <inheritdoc />
        public void AddParam(string parameterName, Db2Type type, int size, ParameterDirection direction)
        {
            AddParam(parameterName, Db2TypeMapper.ToWindows(type), size, direction);
        }

        /// <inheritdoc />
        public object ReadParam(string parameterName)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            ArgumentException.ThrowIfNullOrWhiteSpace(parameterName);

            if (!_command.Parameters.Contains(parameterName))
                throw new ArgumentException($"Parameter '{parameterName}' not found.", nameof(parameterName));

            return _command.Parameters[parameterName].Value;
        }

        /// <inheritdoc />
        public Task<int> ExecuteNonQuery(CancellationToken cancellationToken = default)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _command.ExecuteNonQueryAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task<DbDataReader> ExecuteReader(CancellationToken cancellationToken = default)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _command.ExecuteReaderAsync(cancellationToken);
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
            GC.SuppressFinalize(this);
        }
    }
}
