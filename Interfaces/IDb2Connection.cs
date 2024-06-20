namespace OpenDb2.Interfaces
{
    public interface IDb2Connection : IDisposable
    {
        Task Open();
        Task Close();
    }
}
