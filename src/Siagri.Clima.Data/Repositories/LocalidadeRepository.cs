using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Siagri.Clima.Business.Entities;
using Siagri.Clima.Business.Interfaces.Repositories;
using Siagri.Clima.Business.VOs;
using Siagri.Clima.Data.Context;

namespace Siagri.Clima.Data.Repositories
{
    public class LocalidadeRepository : ILocalidadeRepository
    {
        private readonly SiagriContext _context;

        public LocalidadeRepository(SiagriContext context)
        {
            _context = context;
        }

        public async Task<Localidade> Adicionar(Localidade localidade)
        {
            var result = await _context.AddAsync(localidade);
            
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<IEnumerable<Localidade>> ObterHistorico(string city, DateTime dataFiltro)
        {
            var connection = _context.Database.GetDbConnection();

            const string sql = @"
                SELECT 
                    loc.LocalidadeId,
                    loc.TemperaturaAtual,
                    loc.DataConsulta,
                    loc.Nome as Nome,
                    loc.Longitude as Longitude,
                    loc.Latitude as Latitude
                FROM Localidades loc
                WHERE loc.Nome = @city AND loc.DataConsulta > @data;
            ";

            var result = await connection.QueryAsync<Localidade, CidadeVO, CoordenadaVO, Localidade>(sql,
                (localidade, cidade, coordenada) => {

                        localidade.Cidade = cidade;
                        localidade.Coordenada = coordenada;

                        return localidade;
                    }, new {city = city, data = dataFiltro}, splitOn: "Nome, Longitude");

            return result;
        }
    }
}
