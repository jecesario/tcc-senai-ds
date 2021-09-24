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

            Inicio = DateTime.Parse(Inicio).ToString("yyyy-MM-dd");
            Conclusao = DateTime.Parse(Conclusao).ToString("yyyy-MM-dd");

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

        public bool apagar()
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "DELETE FROM formacao WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
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

        public bool editar()
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;
            Inicio = DateTime.Parse(Inicio).ToString("yyyy-MM-dd");
            Conclusao = DateTime.Parse(Conclusao).ToString("yyyy-MM-dd");
            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE formacao SET nome = @nome, instituicao = @instituicao, inicio = @inicio, conclusao = @conclusao, resumo = @resumo, curriculo_id = @curriculoId, tipo_formacao_id = @tipoFormacaoId WHERE id = @id";
                query.Parameters.AddWithValue("@nome", Nome);
                query.Parameters.AddWithValue("@instituicao", Instituicao);
                query.Parameters.AddWithValue("@inicio", Inicio);
                query.Parameters.AddWithValue("@conclusao", Conclusao);
                query.Parameters.AddWithValue("@resumo", Resumo);
                query.Parameters.AddWithValue("@curriculoId", CurriculoId);
                query.Parameters.AddWithValue("@tipoFormacaoId", TipoFormacaoId);
                query.Parameters.AddWithValue("@id", Id);
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

        public Formacao buscarPorId()
        {
            var con = new MySqlConnection(dbConfig);
            var formacao = new Formacao();

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM formacao WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();

                if (dados.HasRows)
                {
                    while (dados.Read())
                    {
                        formacao.Id = dados.GetInt32("id");
                        formacao.Nome = dados.GetString("nome");
                        formacao.Instituicao = dados.GetString("instituicao");
                        formacao.Inicio = dados.GetDateTime("inicio").ToString("dd/MM/yyyy");
                        formacao.Conclusao = dados.GetDateTime("conclusao").ToString("dd/MM/yyyy");
                        formacao.Resumo = dados.GetString("resumo");
                        formacao.TipoFormacaoId = dados.GetInt32("tipo_formacao_id");
                    }
                }
                else
                {
                    formacao = null;
                }

            }
            catch (Exception e)
            {
                formacao = null;
            }
            finally
            {
                con.Close();
            }

            return formacao;
        }
    }
}