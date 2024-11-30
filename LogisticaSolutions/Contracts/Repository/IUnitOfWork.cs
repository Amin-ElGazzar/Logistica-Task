namespace LogisticaSolutions.Contracts.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save();

    }
}
