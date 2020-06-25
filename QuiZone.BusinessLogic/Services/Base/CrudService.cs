using AutoMapper;
using QuiZone.Common.LoggerService;
using QuiZone.DataAccess.Models.Abstractions;
using QuiZone.DataAccess.Repository;
using QuiZone.DataAccess.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuiZone.BusinessLogic.Services.Base
{
    /// <summary>
    /// Entity CRUD service
    /// </summary>
    /// <see cref="ICrudService{T}"/>
    public abstract class CrudService<TEntityDTO, TEntity> : ICrudService<TEntityDTO, TEntity>
        where TEntityDTO : class
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Saves changes
        /// </summary>
        protected readonly IUnitOfWork database;

        /// <summary>
        /// Logs on error
        /// </summary>
        protected readonly ILoggerManager logger;

        /// <summary>
        /// CRUD operations on entity
        /// </summary>
        protected readonly IBaseRepository<TEntity> repository;

        /// <summary>
        /// AutpMapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        /// <param name="logger">Log on error</param>
        protected CrudService(IUnitOfWork database,
            ILoggerManager logger,
            IBaseRepository<TEntity> repository,
            IMapper mapper)
        {
            this.database = database;
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all model's article
        /// </summary>
        /// <returns>IEnumerable: all data</returns>
        public virtual async Task<IEnumerable<TEntityDTO>> GetAllAsync()
        {
            var tableObject = await repository.GetAllAsync();

            return tableObject == null
                ? null
                : mapper.Map<IEnumerable<TEntityDTO>>(tableObject);
        }

        /// <summary>
        /// Get model by id
        /// </summary>
        /// <param name="id">Id of the model to take</param>
        /// <returns>Founded model or null on failure</returns>
        public virtual async Task<TEntityDTO> GetAsync(int id)
        {
            var tableObject = await repository.GetByIdAsync(id);

            return tableObject == null
                ? null
                : mapper.Map<TEntityDTO>(tableObject);

        }


        public virtual async Task<TEntity> InsertAsync(TEntityDTO value)
        {
            var tableObject = mapper.Map<TEntityDTO, TEntity>(value);
            var insertedTableObject = await repository.InsertAsync(tableObject);

            bool isSaved = await database.SaveAsync();

            return isSaved
                ? insertedTableObject
                : null;

        }
        public virtual async Task<TEntityDTO> UpdateAsync(int id, TEntityDTO value)
        {
            var existedTableObject = await repository.GetByIdAsync(id);
            if (existedTableObject == null)
            {
                return null;
            }

            var tableObject = mapper.Map<TEntityDTO,TEntity>(value);

            tableObject.Id = id;

            var updatedTableObject = repository.Update(tableObject);
            bool isSaved = await database.SaveAsync();

            return isSaved
                ? mapper.Map<TEntity, TEntityDTO>(updatedTableObject)
                : null;
        }

        public virtual async Task<TEntityDTO> RemoveAsync(int id)
        {
            var deletedTableObject = await repository.RemoveByIdAsync(id);
            bool isSaved = await database.SaveAsync();

            return isSaved
                ? mapper.Map<TEntity, TEntityDTO>(deletedTableObject)
                : null;
        }
    }
}
