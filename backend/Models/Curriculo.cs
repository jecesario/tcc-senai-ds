using MySqlConnector;
using System;
using System.Collections.Generic;
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
        public string UsuarioId { get; set; }

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
                        curriculo.Telefone = dados["linkedin"].ToString();
                        curriculo.Resumo = dados["resumo"].ToString();
                        curriculo.Endereco = dados["endereco"].ToString();
                        curriculo.Cidade = dados["cidade"].ToString();
                        curriculo.Estado = dados["estado"].ToString();
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
    }
}