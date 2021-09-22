using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class HabilidadeCurriculo
    {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;

        public int Id { get; set; }
        public int HabilidadeId { get; set; }
        public int CurriculoId { get; set; }

        public bool cadastrar(string habilidades)
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try
            {
                con.Open();
                string[] lista = habilidades.Split(',');
                foreach (var habilidade in lista)
                {
                    var query = con.CreateCommand();
                    query.CommandText = "INSERT INTO habilidades_curriculos (curriculo_id, habilidade_id) VALUES (@curriculoId, @habilidadeId)";
                    query.Parameters.AddWithValue("@curriculoId", CurriculoId);
                    query.Parameters.AddWithValue("@habilidadeId", int.Parse(habilidade));
                    if (query.ExecuteNonQuery() > 0)
                    {
                        resp = true;
                    }
                }
            }
            catch (Exception e)
            {
                resp = false;
            }
            finally
            {
                con.Close();
            }

            return resp;
        }

        public bool editar(string habilidades)
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            apagarPorCurriculoId();

            try
            {
                con.Open();
                string[] lista = habilidades.Split(',');
                foreach (var habilidade in lista)
                {
                    var query = con.CreateCommand();
                    query.CommandText = "INSERT INTO habilidades_curriculos (curriculo_id, habilidade_id) VALUES (@curriculoId, @habilidadeId)";
                    query.Parameters.AddWithValue("@curriculoId", CurriculoId);
                    query.Parameters.AddWithValue("@habilidadeId", int.Parse(habilidade));
                    if (query.ExecuteNonQuery() > 0)
                    {
                        resp = true;
                    }
                }
            }
            catch (Exception e)
            {
                resp = false;
            }
            finally
            {
                con.Close();
            }

            return resp;
        }

        private bool apagarPorCurriculoId()
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "DELETE FROM habilidades_curriculos WHERE curriculo_id = @curriculoId";
                query.Parameters.AddWithValue("@curriculoId", CurriculoId);
                if (query.ExecuteNonQuery() > 0)
                {
                    resp = true;
                }
            }
            catch (Exception e)
            {
                resp = false;
            }
            finally
            {
                con.Close();
            }

            return resp;
        }

        public List<HabilidadeCurriculo> buscarPorCurriculoId()
        {
            var con = new MySqlConnection(dbConfig);
            var habilidades = new List<HabilidadeCurriculo>();

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM habilidades_curriculos WHERE curriculo_id = @curriculoId";
                query.Parameters.AddWithValue("@curriculoId", CurriculoId);
                var dados = query.ExecuteReader();

                if (dados.HasRows)
                {
                    while (dados.Read())
                    {
                        var habilidade = new HabilidadeCurriculo();
                        habilidade.Id = int.Parse(dados["id"].ToString());
                        habilidade.HabilidadeId = int.Parse(dados["habilidade_id"].ToString());
                        habilidades.Add(habilidade);
                    }
                }
                else
                {
                    habilidades = null;
                }

            }
            catch (Exception e)
            {
                habilidades = null;
            }
            finally
            {
                con.Close();
            }

            return habilidades;
        }
    }
}