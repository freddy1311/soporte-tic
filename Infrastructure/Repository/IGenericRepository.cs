using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<ResponseModel> Check(Expression<Func<TEntity, bool>> filter);
        Task<ResponseModel> Insert(TEntity entity);
        Task<ResponseModel> Update(TEntity entity);
        Task<ResponseModel> Delete(TEntity entity);
        Task<ResponseModel> Get(Expression<Func<TEntity, bool>> filter);
        Task<ResponseModel> GetAll(Expression<Func<TEntity, bool>> filter = null);
        Task<ResponseModel> GetLast(Expression<Func<TEntity, object>> orderBy);
    }
}
