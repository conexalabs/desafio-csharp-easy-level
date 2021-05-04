using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Entidades.Base;
using Application.Interfaces.Repository.Base;
using Infra.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {

        private readonly ConexaContext _conexaContext;
        public BaseRepository(ConexaContext conexadb)
        {
            _conexaContext = conexadb;
        }
        public T GetById(Guid id)
        {
            return _conexaContext.Set<T>().Find(id);    
        }

        public async Task<T> Add(T t)
        { 
             await _conexaContext.Set<T>().AddAsync(t);
             await _conexaContext.SaveChangesAsync();
             return t;

        }

        public async Task<T> Update(T t)
        {
            
            var obj =  _conexaContext.Update(t).Entity;
            await _conexaContext.SaveChangesAsync();
            return obj;
        }
    }
}