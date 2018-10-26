using OrderManagement.Core;
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

        TEntity GetEntityById(int id);
        OperationResult<TEntity> Add(TEntity entity);
        OperationResult Delete(TEntity entity);
        OperationResult Edit(TEntity entity);
        IQueryable<TEntity> QuerableSearch();
        void SaveChanges();

        #endregion
    }
}
