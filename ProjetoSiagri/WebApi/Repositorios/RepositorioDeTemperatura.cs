using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebApi.Repositorios
{
    public class RepositorioDeTemperatura
    {        
        private readonly string connectionString = @"Data Source="+ Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Dados\BDWebApi.s3db");       

        public void Salvar(OpenWeatherResultado resultado)
        {
            SQLitePCL.Batteries_V2.Init();

            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                 
                var altera = "UPDATE TemperaturasPorCidade SET TemperaturaAtual = @temp, Latitude = @lat, Longitude = @lon, Dataconsulta = @data "
                    + "WHERE CodigoCidade = @codigo And DataConsulta = @data";

                using (SqliteCommand cmd = new SqliteCommand(altera, conn))
                {
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@codigo", resultado.id);
                    cmd.Parameters.AddWithValue("@temp", resultado.main.temp);
                    cmd.Parameters.AddWithValue("@lat", resultado.coord.lat);
                    cmd.Parameters.AddWithValue("@lon", resultado.coord.lon);
                    cmd.Parameters.AddWithValue("@data", DateTime.UtcNow.Date);

                    var result = cmd.ExecuteNonQuery();

                    if (result.Equals(0))
                    {
                        var insere = "INSERT INTO TemperaturasPorCidade(CodigoCidade,NomeCidade, TemperaturaAtual,Latitude,Longitude,DataConsulta) Values (@codigo,@nome,@temp,@lat,@lon,@data)";

                        using (SqliteCommand cmdInsert = new SqliteCommand(insere, conn))
                        {
                            cmdInsert.Prepare();
                            cmdInsert.Parameters.AddWithValue("@codigo", resultado.id);
                            cmdInsert.Parameters.AddWithValue("@nome", resultado.name);
                            cmdInsert.Parameters.AddWithValue("@temp", resultado.main.temp);
                            cmdInsert.Parameters.AddWithValue("@lat", resultado.coord.lat);
                            cmdInsert.Parameters.AddWithValue("@lon", resultado.coord.lon);
                            cmdInsert.Parameters.AddWithValue("@data", DateTime.UtcNow.Date);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }

                conn.Close();
            }

        }

        public List<OpenWeatherResultado> Consultar(int codigoCidade, DateTime dtInicial,DateTime dtFinal)
        {
            var historico = new List<OpenWeatherResultado>();
            SQLitePCL.Batteries_V2.Init();

            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                var altera = "SELECT * FROM TemperaturasPorCidade WHERE CodigoCidade = @codigo AND DataConsulta > @dataInicial AND DataConsulta < @dataFinal ";

                using (SqliteCommand cmd = new SqliteCommand(altera, conn))
                {
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@codigo", codigoCidade);                    
                    cmd.Parameters.AddWithValue("@dataInicial", dtInicial);
                    cmd.Parameters.AddWithValue("@dataFinal", dtFinal);

                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OpenWeatherResultado temp = new OpenWeatherResultado();
                            temp.main = new OpenWeatherMainResultado();
                            temp.coord = new OpenWeatherCoordResultado();

                            temp.id = Int32.Parse(reader["CodigoCidade"].ToString());
                            temp.name = reader["NomeCidade"].ToString();
                            temp.main.temp = Double.Parse(reader["TemperaturaAtual"].ToString());
                            temp.coord.lat = Double.Parse(reader["Latitude"].ToString());
                            temp.coord.lon = Double.Parse(reader["Longitude"].ToString());

                            historico.Add(temp);
                        }
                    }
                }

                conn.Close();
            }
            return historico;

        }

    }
}
