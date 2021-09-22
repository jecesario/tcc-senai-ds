using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Experiencia
    {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Cargo { get; set; }
        public string Empregador { get; set; }
        public string Resumo { get; set; }
        public string Admissao { get; set; }
        public string Demissao { get; set; }
        public int CurriculoId { get; set; }

        public bool cadastrar()
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO experiencias (cargo, empregador, resumo, admissao, demissao, curriculo_id) VALUES (@cargo, @empregador, @resumo, @admissao, @demissao, @curriculoId)";
                query.Parameters.AddWithValue("@cargo", Cargo);
                query.Parameters.AddWithValue("@empregador", Empregador);
                query.Parameters.AddWithValue("@resumo", Resumo);
                query.Parameters.AddWithValue("@admissao", Admissao);
                query.Parameters.AddWithValue("@demissao", Demissao);
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
    }
}