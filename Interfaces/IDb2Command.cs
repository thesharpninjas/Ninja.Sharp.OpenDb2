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
