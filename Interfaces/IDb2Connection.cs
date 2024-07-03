// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

namespace OpenDb2.Interfaces
{
    public interface IDb2Connection : IDisposable
    {
        Task Open();
        Task Close();
    }
}
