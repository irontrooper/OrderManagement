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
    /// Represents a base interface which containes crud methods for business services
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IOrderManagementService<TEntity> : IOrderManagmentServiceBase where TEntity : EntityBase
    {
        #region Methods 

        IEnumerable<TEntity> GetAll();
        TEntity GetEntityById(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);

        #endregion
    }
}
