using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data
{
    /// <summary>
    /// Represents an interface which contains entity methods
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IOrderManagementRepository<TEntity> where TEntity : EntityBase
    {
        #region Methods

        IEnumerable<TEntity> GetAll();
        TEntity GetEntityById(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        TEntity Delete(TEntity entity);
        void Edit(TEntity entity);
        void Save();

        #endregion
    }
}
