using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models {
    public class Habilidade {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Nome { get; set; }

        public static List<Habilidade> listar() {
            var con = new MySqlConnection(dbConfig);
            var habilidades = new List<Habilidade>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM habilidades";
                var dados = query.ExecuteReader();

                if(dados.HasRows)
                {
                    while (dados.Read())
                    {
                        var habilidade = new Habilidade();
                        habilidade.Id = int.Parse(dados["id"].ToString());
                        habilidade.Nome = dados["nome"].ToString();
                        habilidades.Add(habilidade);
                    }
                }
                
            } catch (Exception e) {
                habilidades = null;
            } finally {
                con.Close();
            }

            return habilidades;
        }

        public bool cadastrar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO habilidades (nome) VALUES (@nome)";
                query.Parameters.AddWithValue("@nome", Nome);
                if(query.ExecuteNonQuery() > 0)
                {
                    resp = true;
                }
            } catch (Exception e) {
                resp = false;
            } finally {
                con.Close();
            }

            return resp;
        }

        public bool apagar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "DELETE FROM habilidades WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                if(query.ExecuteNonQuery() > 0)
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

        public bool editar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE habilidades SET nome = @nome WHERE id = @id";
                query.Parameters.AddWithValue("@nome", Nome);
                query.Parameters.AddWithValue("@id", Id);
                if(query.ExecuteNonQuery() > 0)
                {
                    resp = true;
                }
            } catch (Exception e) {
                resp = false;
            } finally {
                con.Close();
            }

            return resp;
        }

        public Habilidade buscarPorId() {
            var con = new MySqlConnection(dbConfig);
            var habilidade = new Habilidade();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM habilidades WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();
                
                if(dados.HasRows)
                {
                    while (dados.Read())
                    {
                        habilidade.Id = int.Parse(dados["id"].ToString());
                        habilidade.Nome = dados["nome"].ToString();
                    }
                } 
            } catch (Exception e) {
                habilidade = null;
            } finally {
                con.Close();
            }

            return habilidade;
        }
    }
}