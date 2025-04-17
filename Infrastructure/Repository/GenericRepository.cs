using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region properties
        private readonly soporteContext _context;
        #endregion

        #region constructor
        public GenericRepository(soporteContext contexto)
        {
            _context = contexto;
        }
        #endregion

        #region methods
        public async Task<ResponseModel> Check(Expression<Func<TEntity, bool>> filter)
        {
            ResponseModel rm = new ResponseModel();

            try
            {
                TEntity entity = _context.
                    Set<TEntity>().
                    Where(filter).
                    FirstOrDefault();

                if (entity != null)
                {
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Obtener", entity);
                }
                else
                {
                    rm.SetResponse(false, "No existen los datos consultados!.", "Obtener");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}");
            }

            return rm;
        }

        public async Task<ResponseModel> Insert(TEntity entity)
        {
            ResponseModel rm = new ResponseModel();

            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();

                rm.SetResponse(true, "Registro guardado exitosamente!.", "Guardar", entity);
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}");
            }

            return rm;
        }

        public async Task<ResponseModel> Update(TEntity entity)
        {
            ResponseModel rm = new ResponseModel();

            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
                rm.SetResponse(true, "Registro actualizado exitosamente!.", "Actualizar", entity);
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}");
            }

            return rm;
        }

        public async Task<ResponseModel> Delete(TEntity entity)
        {
            ResponseModel rm = new ResponseModel();

            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                rm.SetResponse(true, "Registro eliminado exitosamente!.", "Eliminar", entity);
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}");
            }

            return rm;
        }

        public async Task<ResponseModel> Get(Expression<Func<TEntity, bool>> filter)
        {
            ResponseModel rm = new ResponseModel();

            try
            {
                IQueryable<TEntity> entity = _context.
                    Set<TEntity>().
                    Where(filter);

                if (entity.ToList().Count > 0)
                {
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Obtener", entity);
                }
                else
                {
                    rm.SetResponse(false, "No existen los datos consultados!.", "Obtener");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}");
            }

            return rm;
        }

        public async Task<ResponseModel> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            ResponseModel rm = new ResponseModel();

            try
            {
                IQueryable<TEntity> queryEntity = filter == null ? _context.Set<TEntity>() :
                    _context.Set<TEntity>().Where(filter);

                if (queryEntity != null)
                {
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Obtener", queryEntity);
                }
                else
                {
                    rm.SetResponse(false, "No existen los datos consultados!.", "Obtener");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}");
            }

            return rm;
        }

        public async Task<ResponseModel> GetLast(Expression<Func<TEntity, object>> orderBy)
        {
            ResponseModel rm = new ResponseModel();

            try
            {
                TEntity lastItem = await _context.
                    Set<TEntity>().
                    OrderByDescending(orderBy).
                    FirstOrDefaultAsync();

                if (lastItem != null)
                {
                    rm.SetResponse(true, "Consulta realizada exitosamente!", "Último Registro", lastItem);
                }
                else
                {
                    rm.SetResponse(false, "No existen los datos consultados!", "Último Registro");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}");
            }

            return rm;
        }

        #endregion
    }
}
