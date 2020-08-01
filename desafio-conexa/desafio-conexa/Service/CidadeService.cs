using desafio_conexa.DbContexts;
using desafio_conexa.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_conexa.Validation;
using System.Security.Cryptography.X509Certificates;

namespace desafio_conexa.Service
{
    public class CidadeService
    {
        private readonly CidadesDbContext _contexto;
        private readonly TemperaturasDbContext _contextoTemp;
        public CidadeService(CidadesDbContext context, TemperaturasDbContext temperaturasDb)
        {
            _contexto = context;
            _contextoTemp = temperaturasDb;
        }

        public Retorno RetornaTemperaturaPorLocalizacao(string lat, string lon)
        {
            var retorno = new Retorno();
            if (FormatValidation.ValidarParametroVazio(lat,lon))
            {
                retorno.Mensagem = "Informe a latitude e longitude";
                return retorno;
            }

            if (!FormatValidation.ValidarFormatoInput<decimal>(lat,lon))
            {
                retorno.Mensagem = "Formato inválido";
                return retorno;
            }
            var api = new WeatherMap();
            var weather = api.getApiLatLong(lat,lon);
            if (weather != null && !string.IsNullOrEmpty(weather.Name))
            {
                retorno.Mensagem = GravarDados(weather.Name, weather.main.Temp);
                retorno.temperatura = weather.main.Temp;
                retorno.Sucesso = true;
                return retorno;
            }
            else
            {
                retorno.Mensagem = "Não foi possível retornar dados da api de temperaturas.";
                return retorno;
            }

        }
        public Retorno RetornaTemperaturaPorNome(string nomeCidade)
        {
            var retorno = new Retorno();

            if (FormatValidation.ValidarParametroVazio(nomeCidade))
            {
                retorno.Mensagem = "Informe o nome da cidade";
                return retorno;
            }
            if (!FormatValidation.ValidarFormatoInput<string>(nomeCidade))
            {
                retorno.Mensagem = "Formato inválido";
                return retorno;
            }
            
            var api = new WeatherMap();
            var weather = api.getObjetoApiCidade(nomeCidade);
            if (weather != null)
            {
                retorno.Mensagem = GravarDados(nomeCidade, weather.main.Temp);
                retorno.temperatura = weather.main.Temp;
                retorno.Sucesso = true;
                return retorno;
            }
            else
            {
                retorno.Mensagem = "Não foi possível retornar dados da api de temperaturas.";
                return retorno;
            }
                
        }

        public Retorno RetornarHistorico(string nomeCidade, string lat, string lon)
        {
            var retorno = new Retorno();
            if (!FormatValidation.ValidarParametroVazio(nomeCidade))
            {
               
                retorno.Historico = MontaListaHistorico(nomeCidade);
                retorno.Mensagem = retorno.Historico?.Count > 0 ? "Sucesso" : "Não há histórico para esta cidade";
                retorno.Sucesso = true;
                return retorno;
            }
            else if (!FormatValidation.ValidarParametroVazio(lat, lon))
            {
                if (!FormatValidation.ValidarFormatoInput<decimal>(lat, lon))
                {
                    retorno.Mensagem = "Formato inválido";
                    return retorno;
                }
                var api = new WeatherMap();
                var weather = api.getApiLatLong(lat, lon);
                if (weather != null && !string.IsNullOrEmpty(weather.Name))
                {
                    retorno.Historico = MontaListaHistorico(weather.Name);
                    retorno.Mensagem = retorno.Historico?.Count > 0 ? "Sucesso" : "Não há histórico para esta cidade";
                    retorno.Sucesso = true;
                    return retorno;
                }
                else
                {
                    retorno.Mensagem = "Não foi possível retornar dados da api de temperaturas.";
                    return retorno;
                }
            }
            else
            {
                retorno.Mensagem = "Informe o nome da cidade ou a localização";
                return retorno;
            }

            return retorno;
                

        }
        
        private List<HistoricoRetorno> MontaListaHistorico(string nomeCidade)
        {
            var registro = _contexto.Cidades.FirstOrDefault(x => x.Nome.ToLower() == nomeCidade.ToLower());
            if (registro != null)
            {
                var temperaturas = _contextoTemp.Temperaturas.Where(x => x.DataCaptura > DateTime.Now.AddDays(-30).Date && x.IdCidade == registro.Id).ToList();
                if(temperaturas.Count > 0)
                    return temperaturas.Select(x => new HistoricoRetorno() { Data = x.DataCaptura, Temperatura = x.TemperaturaAt }).ToList();
            }

            return null;
        }
        public string GravarDados(string nomeCidade, decimal temperatura)
        {
            try
            {
                var cidade = _contexto.Cidades.FirstOrDefault(x => x.Nome.ToLower() == nomeCidade.ToLower());
                if (cidade == null)
                {
                    GravarDadosCidade(nomeCidade);
                    cidade = _contexto.Cidades.FirstOrDefault(x => x.Nome.ToLower() == nomeCidade.ToLower());
                    if(cidade != null)
                        GravarDadosTemperatura(temperatura, cidade.Id);
                }
                else
                {
                    GravarDadosTemperatura(temperatura, cidade.Id);
                }

                return "Sucesso";

            }catch(Exception e)
            {
                return e.Message;
            }
            
            
        }
        public void GravarDadosTemperatura(decimal temperatura, int idCidade)
        {
            var tempService = new TemperaturaService(_contextoTemp);
            tempService.GravarDados(temperatura, idCidade);
          
        }

        public void GravarDadosCidade(string nomeCidade)
        {
            _contexto.Add(new Cidade() { Nome = nomeCidade });
            _contexto.SaveChanges();
        }
    }
}
