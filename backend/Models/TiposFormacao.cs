using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class TiposFormacao
    {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Nome { get; set; }

        public static List<TiposFormacao> listar()
        {
            var con = new MySqlConnection(dbConfig);
            var tiposFormacaoList = new List<TiposFormacao>();

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM tipos_formacao";
                var dados = query.ExecuteReader();

                if (dados.HasRows)
                {
                    while (dados.Read())
                    {
                        var tiposFormacao = new TiposFormacao();
                        tiposFormacao.Id = int.Parse(dados["id"].ToString());
                        tiposFormacao.Nome = dados["nome"].ToString();
                        tiposFormacaoList.Add(tiposFormacao);
                    }
                }

            }
            catch (Exception e)
            {
                tiposFormacaoList = null;
            }
            finally
            {
                con.Close();
            }

            return tiposFormacaoList;
        }
    }
}