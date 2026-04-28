

using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Domain.Models;
using canchasfutbol.Infrastructuree.Services;
using System.Collections;


namespace canchasfutbol.Infrastructuree.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private Hashtable _repositories;

        private readonly AppDbContext _context;

        private IReservaRepository _reservaRepository;

        private ICanchasRepository _canchasRepository;

        private IUserRepository _userRepository;



        public IReservaRepository ReservaRepository => _reservaRepository ??= new ReservaRepository(_context);

        

        public UnitOfWorkRepository(AppDbContext context)
        {
            _context = context;
            

        } 
        public AppDbContext Context => _context;

        public ICanchasRepository CanchasRepository => _canchasRepository ??= new CanchasRepository(_context);

        public IUserRepository UserRepository => _userRepository ??=  new UserRepository(_context);

        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
          if(_repositories == null)
            {
                _repositories = new Hashtable();    
            }
          var type = typeof(TEntity).Name;

            if(! _repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
