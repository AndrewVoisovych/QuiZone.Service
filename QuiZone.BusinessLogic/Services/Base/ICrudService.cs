using System.Collections.Generic;
using System.Threading.Tasks;
using QuiZone.DataAccess.Models.Abstractions;

namespace QuiZone.BusinessLogic.Services.Base
{
    /// <summary>
    /// Set a behavior of services 
    /// </summary>
    public interface ICrudService<TEntityDTO, TEntity> 
        where TEntityDTO : class
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity</returns>
        Task<TEntityDTO> GetAsync(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Entities</returns>
        Task<IEnumerable<TEntityDTO>> GetAllAsync();


        /// Add methods to service
        /// <summary>
        /// Registers a new entity
        /// </summary>
        /// <param name="value">New entity</param>
        /// <returns>Created entity</returns>
        Task<TEntity> InsertAsync(TEntityDTO value);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="value">Entity model to update</param>
        /// <returns>Updated entity</returns>
        Task<TEntityDTO> UpdateAsync(int id, TEntityDTO value);

        /// <summary>
        /// Removes entity with this id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>void</returns>
        Task<TEntityDTO> RemoveAsync(int id);
    }
}

