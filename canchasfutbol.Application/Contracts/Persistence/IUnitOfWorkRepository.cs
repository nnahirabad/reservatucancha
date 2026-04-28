



namespace canchasfutbol.Application.Contracts.Persistence
{
    public interface IUnitOfWorkRepository : IDisposable
    {
       IUserRepository UserRepository { get; }
        IReservaRepository ReservaRepository { get; }

        ICanchasRepository CanchasRepository { get; }

        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> Complete();
    }
}
