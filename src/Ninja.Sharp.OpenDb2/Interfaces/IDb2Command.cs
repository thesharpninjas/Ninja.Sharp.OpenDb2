// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Data.Common;

namespace OpenDb2.Interfaces
{
    public interface IDb2Command : IDisposable
    {
        void AddParam(string parameterName, object value);
        object ReadParam(string parameterName);
        Task<int> ExecuteNonQuery();
        Task<DbDataReader> ExecuteReader();
        DbDataAdapter CreateDataAdapter();
    }
}
