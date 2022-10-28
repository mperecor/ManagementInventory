using ManagementInventory.Application.Contracts.Persistence;
using ManagementInventory.Domain.Common;
using ManagementInventory.Infrastructure.Persistence;
using System.Collections;

namespace ManagementInventory.Infrastructure.Repositories
{
    /// <summary>
    /// UnitOfWork class that manages the creation of repositories for database access
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Repositories
        /// </summary>
        private Hashtable _repositories;

        /// <summary>
        /// Context of database
        /// </summary>
        private readonly ManagementInventoryDbContext _context;

        public ManagementInventoryDbContext ManagementInventoryDbContext => _context;

        /// <summary>
        /// Constructor to assign a dependency injection
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ManagementInventoryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to save change in database
        /// </summary>
        /// <returns>Returns the register number that have been affected</returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Err");
            }
        }

        /// <summary>
        /// Disose a context
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Method to get a repository from a entity
        /// </summary>
        /// <typeparam name="TEntity">Entity to set a repository</typeparam>
        /// <returns>Repository to entity</returns>
        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}