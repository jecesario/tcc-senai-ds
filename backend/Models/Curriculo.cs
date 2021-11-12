using backend.Models.Dtos;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Curriculo
    {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Github { get; set; }
        public string Linkedin { get; set; }
        public string Telefone { get; set; }
        public string Resumo { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string DataEdicao { get; set; }
        public string UsuarioId { get; set; }

        public static List<CurriculoResponse> listar()
        {
            var con = new MySqlConnection(dbConfig);
            var curriculos = new List<CurriculoResponse>();

            con.Open();
            var query = con.CreateCommand();
            query.CommandText = "SELECT curriculos.id, telefone, cidade, usuarios.nome, GROUP_CONCAT(habilidades.nome SEPARATOR ', ') AS habilidades FROM curriculos INNER JOIN usuarios ON usuario_id = usuarios.id INNER JOIN habilidades_curriculos ON curriculos.id = habilidades_curriculos.curriculo_id INNER JOIN habilidades ON habilidades.id = habilidades_curriculos.habilidade_id GROUP BY usuario_id;";
            var dados = query.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var curriculo = new CurriculoResponse();
                    curriculo.Nome = dados["nome"].ToString();
                    curriculo.Telefone = dados["telefone"].ToString();
                    curriculo.Cidade = dados["cidade"].ToString();
                    curriculo.Habilidades = dados["habilidades"].ToString();
                    curriculos.Add(curriculo);
                }
            }
            else
            {
                curriculos = null;
            }
            con.Close();
        return curriculos;
        }

        public Curriculo buscarPorUsuarioId()
        {
            var con = new MySqlConnection(dbConfig);
            var curriculo = new Curriculo();

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM curriculos WHERE usuario_id = @usuarioId";
                query.Parameters.AddWithValue("@usuarioId", UsuarioId);
                var dados = query.ExecuteReader();

                if (dados.HasRows)
                {
                    while (dados.Read())
                    {
                        curriculo.Id = int.Parse(dados["id"].ToString());
                        curriculo.Github = dados["github"].ToString();
                        curriculo.Linkedin = dados["linkedin"].ToString();
                        curriculo.Telefone = dados["telefone"].ToString();
                        curriculo.Resumo = dados["resumo"].ToString();
                        curriculo.Endereco = dados["endereco"].ToString();
                        curriculo.Cidade = dados["cidade"].ToString();
                        curriculo.Estado = dados["estado"].ToString();
                        curriculo.DataEdicao = dados.GetDateTime("data_edicao").ToString();
                    }
                }
                else
                {
                    curriculo = null;
                }

            }
            catch (Exception e)
            {
                curriculo = null;
            }
            finally
            {
                con.Close();
            }

            return curriculo;
        }

        public bool cadastrar()
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO curriculos (github, linkedin, telefone, resumo, endereco, cidade, estado, usuario_id) VALUES (@github, @linkedin, @telefone, @resumo, @endereco, @cidade, @estado, @usuarioId)";
                query.Parameters.AddWithValue("@github", Github);
                query.Parameters.AddWithValue("@linkedin", Linkedin);
                query.Parameters.AddWithValue("@telefone", Telefone);
                query.Parameters.AddWithValue("@resumo", Resumo);
                query.Parameters.AddWithValue("@endereco", Endereco);
                query.Parameters.AddWithValue("@cidade", Cidade);
                query.Parameters.AddWithValue("@estado", Estado);
                query.Parameters.AddWithValue("@usuarioId", UsuarioId);

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

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE curriculos SET github = @github, linkedin = @linkedin, telefone = @telefone, resumo = @resumo, endereco = @endereco, cidade = @cidade, estado = @estado, usuario_id = @usuarioId WHERE id = @id";
                query.Parameters.AddWithValue("@github", Github);
                query.Parameters.AddWithValue("@linkedin", Linkedin);
                query.Parameters.AddWithValue("@telefone", Telefone);
                query.Parameters.AddWithValue("@resumo", Resumo);
                query.Parameters.AddWithValue("@endereco", Endereco);
                query.Parameters.AddWithValue("@cidade", Cidade);
                query.Parameters.AddWithValue("@estado", Estado);
                query.Parameters.AddWithValue("@usuarioId", UsuarioId);
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

        public void atualizarDataEdicao()
        {
            var con = new MySqlConnection(dbConfig);

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE curriculos SET data_edicao = @dataEdicao WHERE id = @id";
                query.Parameters.AddWithValue("@dataEdicao", DateTime.Now);
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
}