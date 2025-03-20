// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using IBM.Data.Db2;
using OpenDb2.Interfaces.Linux;
using OpenDb2.Enums;
using System.Data;
using System.Data.Common;

namespace OpenDb2.Drivers.Linux
{
    public class LnxDb2Command(DB2Command command) : ILnxDb2Command
    {
        private readonly DB2Command _command = command;

        /// <inheritdoc />
        public void AddParam(string parameterName, object value) =>
            _command.Parameters.Add(parameterName, value);

        /// <inheritdoc />
        public void AddParam(string parameterName, LnxDb2Type type, object value) =>
            _command.Parameters.Add(parameterName, (DB2Type)type).Value = value;

        /// <inheritdoc />
        public void AddParam(string parameterName, LnxDb2Type type, int size, object value) =>
            _command.Parameters.Add(parameterName, (DB2Type)type, size).Value = value;

        /// <inheritdoc />
        public void AddParam(string parameterName, LnxDb2Type type, int size, ParameterDirection direction) =>
            _command.Parameters.Add(parameterName, (DB2Type)type, size).Direction = direction;

        /// <inheritdoc />
        public object ReadParam(string parameterName) =>
            _command.Parameters[parameterName].Value;

        /// <inheritdoc />
        public Task<int> ExecuteNonQuery() => _command.ExecuteNonQueryAsync();

        /// <inheritdoc />
        public Task<DbDataReader> ExecuteReader() => _command.ExecuteReaderAsync();

        /// <inheritdoc />
        public DbDataAdapter CreateDataAdapter() => new DB2DataAdapter(_command);

        /// <inheritdoc />
        public void Dispose() => _command.Dispose();
    }
}
