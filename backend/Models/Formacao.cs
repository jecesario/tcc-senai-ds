using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Formacao
    {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Instituicao { get; set; }
        public string Inicio { get; set; }
        public string Conclusao { get; set; }
        public string Resumo { get; set; }
        public int CurriculoId { get; set; }
        public int TipoFormacaoId { get; set; }

        public bool cadastrar()
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO formacao (nome, instituicao, inicio, conclusao, resumo, curriculo_id, tipo_formacao_id) VALUES (@nome, @instituicao, @inicio, @conclusao, @resumo, @curriculoId, @tipoFormacaoId)";
                query.Parameters.AddWithValue("@nome", Nome);
                query.Parameters.AddWithValue("@instituicao", Instituicao);
                query.Parameters.AddWithValue("@inicio", Inicio);
                query.Parameters.AddWithValue("@conclusao", Conclusao);
                query.Parameters.AddWithValue("@resumo", Resumo);
                query.Parameters.AddWithValue("@curriculoId", CurriculoId);
                query.Parameters.AddWithValue("@tipoFormacaoId", TipoFormacaoId);
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

        public List<Formacao> buscarPorCurriculoId()
        {
            var con = new MySqlConnection(dbConfig);
            var formacoes = new List<Formacao>();

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM formacao WHERE curriculo_id = @curriculoId";
                query.Parameters.AddWithValue("@curriculoId", CurriculoId);
                var dados = query.ExecuteReader();

                if (dados.HasRows)
                {
                    while (dados.Read())
                    {
                        var experiencia = new Formacao();
                        experiencia.Id = dados.GetInt32("id");
                        experiencia.Nome = dados.GetString("nome");
                        experiencia.Instituicao = dados.GetString("instituicao");
                        experiencia.Inicio = dados.GetDateTime("inicio").ToString("dd/MM/yyyy");
                        experiencia.Conclusao = dados.GetDateTime("conclusao").ToString("dd/MM/yyyy");
                        experiencia.Resumo = dados.GetString("resumo");
                        experiencia.TipoFormacaoId = dados.GetInt32("tipo_formacao_id");
                        formacoes.Add(experiencia);
                    }
                }
                else
                {
                    formacoes = null;
                }

            }
            catch (Exception e)
            {
                formacoes = null;
            }
            finally
            {
                con.Close();
            }

            return formacoes;
        }
    }
}