using System.Data;

namespace OpenDb2.Interfaces
{
    public interface IDb2Transaction : IDisposable
    {
        void Commit();
        void Rollback();
        IDbTransaction Transaction { get; }
    }
}
