using ConexaDesafio.Dados.Contexto;
using ConexaDesafio.Negocio.Interfaces;
using ConexaDesafio.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConexaDesafio.Dados.Repositorio
{
    public abstract class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : Entidade
    {
        protected readonly ConexaDesafioDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public RepositorioBase(ConexaDesafioDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task Adicionar(TEntity entidade)
        {
            await DbSet.AddAsync(entidade);
            await SalvarMudancas();
        }

        public async Task<int> SalvarMudancas()
        {
            return await Db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
