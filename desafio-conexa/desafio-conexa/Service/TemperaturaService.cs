using desafio_conexa.DbContexts;
using desafio_conexa.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_conexa.Service
{
    public class TemperaturaService
    {
        private TemperaturasDbContext _contexto;
        public TemperaturaService(TemperaturasDbContext context)
        {
            _contexto = context;
        }
        public void GravarDados(decimal temperatura, int idCidade)
        {
            var registro = _contexto.Temperaturas.FirstOrDefault(x => x.IdCidade == idCidade && x.DataCaptura == DateTime.Now.Date);
            if(registro!= null)
            {
                registro.TemperaturaAt = temperatura;
                _contexto.Update(registro);
            }
            else
            {
                _contexto.Add(new Temperatura() { IdCidade = idCidade, TemperaturaAt = temperatura, DataCaptura = DateTime.Now.Date });
            }
                
            

            _contexto.SaveChanges();
        }

    }
}
