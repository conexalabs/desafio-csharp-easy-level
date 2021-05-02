using System;
using System.Linq;
using Application.Entidades.Base;
using Application.Interfaces.Repository.Base;
using Infra.Data.DBContext;

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
    }
}