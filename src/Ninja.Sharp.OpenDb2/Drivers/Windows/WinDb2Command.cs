// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Enums;
using OpenDb2.Interfaces.Windows;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace OpenDb2.Drivers.Windows
{
    public class WinDb2Command(OleDbCommand command) : IWinDb2Command
    {
        private readonly OleDbCommand _command = command;

        /// <inheritdoc />
        public void AddParam(string parameterName, object value) =>
            _command.Parameters.AddWithValue(parameterName, value);

        /// <inheritdoc />
        public void AddParam(string parameterName, WinDb2Type type, object value) =>
            _command.Parameters.Add(parameterName, (OleDbType)type).Value = value;

        /// <inheritdoc />
        public void AddParam(string parameterName, WinDb2Type type, int size, object value) =>
            _command.Parameters.Add(parameterName, (OleDbType)type, size).Value = value;

        /// <inheritdoc />
        public void AddParam(string parameterName, WinDb2Type type, int size, ParameterDirection direction) =>
            _command.Parameters.Add(parameterName, (OleDbType)type, size).Direction = direction;

        /// <inheritdoc />
        public object ReadParam(string parameterName) =>
            _command.Parameters[parameterName].Value;

        public Task<int> ExecuteNonQuery() => _command.ExecuteNonQueryAsync();

        /// <inheritdoc />
        public Task<DbDataReader> ExecuteReader() => _command.ExecuteReaderAsync();

        /// <inheritdoc />
        public DbDataAdapter CreateDataAdapter() => new OleDbDataAdapter(_command);

        /// <inheritdoc />
        public void Dispose() => _command.Dispose();
    }
}
