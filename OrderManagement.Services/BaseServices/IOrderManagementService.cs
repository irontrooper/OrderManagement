using OrderManagement.Core;
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

        TEntity GetEntityById(int id);
        OperationResult<TEntity> Insert(TEntity entity);
        OperationResult Delete(TEntity entity);
        OperationResult DeleteEntityById(int id);
        OperationResult Update(TEntity entity);
        IQueryable<TEntity> QuerableSearch();

        #endregion
    }
}
