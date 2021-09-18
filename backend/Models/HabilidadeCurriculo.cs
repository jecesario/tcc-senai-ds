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
                Console.WriteLine("Deu ruimmmmmmmmmm " + e.Message);
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