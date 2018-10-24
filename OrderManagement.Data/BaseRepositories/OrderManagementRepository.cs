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
        public virtual TEntity Add(TEntity entity)
        {
            return _DbSet.Add(entity);
        }

        /// <summary>
        /// Deletes the given entity from db set
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Delete(TEntity entity)
        {
            return _DbSet.Remove(entity);
        }

        /// <summary>
        /// Updates the given entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Edit(TEntity entity)
        {
            _DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Find the entity by the given expression 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _DbSet.Where(predicate).AsEnumerable();
        }

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _DbSet.AsEnumerable();
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
        public virtual void Save()
        {
            _DbContext.SaveChanges();
        }

        #endregion
    }
}
