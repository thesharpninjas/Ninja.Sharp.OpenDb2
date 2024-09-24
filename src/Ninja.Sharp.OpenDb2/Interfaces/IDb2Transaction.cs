// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

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
