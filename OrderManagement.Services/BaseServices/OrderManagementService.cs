using OrderManagement.Core;
using OrderManagement.Data;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public abstract class OrderManagementService<TEntity> : IOrderManagementService<TEntity> where TEntity : EntityBase
    {
        #region Members

        private IOrderManagementRepository<TEntity> _Repository;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of OrderManagementService
        /// </summary>
        /// <param name="repository"></param>
        public OrderManagementService(IOrderManagementRepository<TEntity> repository)
        {
            _Repository = repository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the given entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _Repository.Add(entity);
            _Repository.Save();
             
        }

        /// <summary>
        /// Deletes the given entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _Repository.Delete(entity);
            _Repository.Save();
        }

        /// <summary>
        /// Finds the items by given expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _Repository.Find(predicate);
        }

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _Repository.GetAll();
        }

        /// <summary>
        /// Gets entity by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetEntityById(int id)
        {
            return _Repository.GetEntityById(id);
        }

        /// <summary>
        /// Updates the given entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _Repository.Edit(entity);
            _Repository.Save();
        }

        #endregion
    }
}
