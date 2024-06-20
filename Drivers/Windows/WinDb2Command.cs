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

        public void AddParam(string parameterName, object value) =>
            _command.Parameters.Add(parameterName, value);

        public void AddParam(string parameterName, WinDb2Type type, object value) =>
            _command.Parameters.Add(parameterName, (OleDbType)type).Value = value;

        public void AddParam(string parameterName, WinDb2Type type, int size, object value) =>
            _command.Parameters.Add(parameterName, (OleDbType)type, size).Value = value;

        public void AddParam(string parameterName, WinDb2Type type, int size, ParameterDirection direction) =>
            _command.Parameters.Add(parameterName, (OleDbType)type, size).Direction = direction;

        public object ReadParam(string parameterName) =>
            _command.Parameters[parameterName].Value;

        public Task<int> ExecuteNonQuery() => _command.ExecuteNonQueryAsync();

        public Task<DbDataReader> ExecuteReader() => _command.ExecuteReaderAsync();

        public DbDataAdapter CreateDataAdapter() => new OleDbDataAdapter(_command);

        public void Dispose() => _command.Dispose();
    }
}
