using ConexaDesafio.Negocio.Interfaces;
using ConexaDesafio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConexaDesafio.Negocio.Servicos
{
    public class TemperaturaServico 
    {
        private readonly ITemperaturaRepositorio _temperaturaRepositorio;

        public TemperaturaServico(ITemperaturaRepositorio cidadeTemperaturaRepositorio)
        {
            _temperaturaRepositorio = cidadeTemperaturaRepositorio;
        }
        public async Task Adicione(Temperatura cidadeT)
        {
            await _temperaturaRepositorio.Adicionar(cidadeT);
        }

        public void Dispose()
        {
            _temperaturaRepositorio?.Dispose();
        }
    }
}
