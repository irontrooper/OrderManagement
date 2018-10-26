using OrderManagement.Core;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data
{
    /// <summary>
    /// Represents an abstract class which contains db repository methods
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class OrderManagementRepository<TEntity> : IOrderManagementRepository<TEntity> where TEntity : EntityBase
    {
        #region Members

        protected OrderManagementDbContext _DbContext;
        protected readonly IDbSet<TEntity> _DbSet;

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize an instance of OrderManagementRepository with DbContext
        /// </summary>
        /// <param name="context"></param>
        public OrderManagementRepository(OrderManagementDbContext context)
        {
            _DbContext = context;
            _DbSet = context.Set<TEntity>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the given entity to dbset
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual OperationResult<TEntity> Add(TEntity entity)
        {
            var result = new OperationResult<TEntity>();

            try
            {
                var item = _DbSet.Add(entity);
                SaveChanges();
                result.Item = item;
            }
            catch (Exception ex)
            {
                result.AddError(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// Deletes the given entity from db set
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual OperationResult Delete(TEntity entity)
        {
            var result = new OperationResult();

            try
            {
                _DbSet.Remove(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                result.AddError(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// Updates the given entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual OperationResult Edit(TEntity entity)
        {
            var result = new OperationResult();

            try
            {
                _DbContext.Entry(entity).State = EntityState.Modified;
                SaveChanges();
            }
            catch (Exception ex)
            {
                result.AddError(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetEntityById(int id)
        {
            return _DbContext.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Saves the changes in db set
        /// </summary>
        public void SaveChanges()
        {
            _DbContext.SaveChanges();
        }

        /// <summary>
        /// Gets Iquarable item list query
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> QuerableSearch()
        {
            IQueryable<TEntity> query = _DbContext.Set<TEntity>();
            return query;
        }

        #endregion
    }
}
