using System;
using Application.Entidades.Base;
using Application.Interfaces.Repository.Base;
using Infra.Data.DBContext;

namespace Infra.Data.Repositories.Base
{
    public class RepositoryBase<T> : IBaseRepository<T> where T : EntityBase
    {
        private readonly ConexaContext _conexaContext;
        public RepositoryBase(ConexaContext conexadb)
        {
            _conexaContext = conexadb;
        }
        public T GetById(Guid id)
        {
            return _conexaContext.Set<T>().Find(id);
        }
    }
}