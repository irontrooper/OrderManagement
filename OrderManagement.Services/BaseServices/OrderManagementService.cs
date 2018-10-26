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
    /// Represents a abstract class which manages crud operations on given entity type
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
        public virtual OperationResult<TEntity> Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var result = new OperationResult<TEntity>();

            try
            {
                result = _Repository.Add(entity);
            }
            catch (Exception ex)
            {

                result.AddError(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// Deletes the given entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual OperationResult Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return _Repository.Delete(entity);

        }

        /// <summary>
        /// Deletes entity which has the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual OperationResult DeleteEntityById(int id)
        {
            var result = new OperationResult();
            var entity = _Repository.GetEntityById(id);
            if (entity != null)
            {
                result = Delete(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets entity by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetEntityById(int id)
        {
            return _Repository.GetEntityById(id);
        }

        /// <summary>
        /// Updates the given entity
        /// </summary>
        /// <param name="entity"></param>
        public OperationResult Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return _Repository.Edit(entity);
        }

        /// <summary>
        ///  Gets Iquarable item list query
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> QuerableSearch()
        {
            return _Repository.QuerableSearch();
        }

        #endregion
    }
}
