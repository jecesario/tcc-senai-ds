using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class VagaTipo
    {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Descricao { get; set; }

        public bool cadastrar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO vagas_tipos (descricao) VALUES (@descricao)";
                query.Parameters.AddWithValue("@descricao", Descricao);

                if (query.ExecuteNonQuery() > 0) {
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

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "DELETE FROM vagas_tipos WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                if (query.ExecuteNonQuery() > 0) {
                    resp = true;
                }
            } catch (Exception e) {
                resp = false;
            } finally {
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
                query.CommandText = "UPDATE vagas_tipos SET descricao = @descricao WHERE id = @id";
                query.Parameters.AddWithValue("@descricao", Descricao);
                query.Parameters.AddWithValue("@id", Id);
                if (query.ExecuteNonQuery() > 0) {
                    resp = true;
                }
            } catch (Exception e) {
                resp = false;
            } finally {
                con.Close();
            }

            return resp;
        }

        public VagaTipo buscarPorId() {
            var con = new MySqlConnection(dbConfig);
            var vagaTipo = new VagaTipo();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM vagas_tipos WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        vagaTipo.Id = int.Parse(dados["id"].ToString());
                        vagaTipo.Descricao = dados["descricao"].ToString();
                    }
                } else {
                    vagaTipo = null;
                }

            } catch (Exception e) {
                vagaTipo = null;
            } finally {
                con.Close();
            }

            return vagaTipo;
        }

        public static List<VagaTipo> listar() {
            var con = new MySqlConnection(dbConfig);
            var vagaTipos = new List<VagaTipo>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM vagas_tipos";
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var vagaTipo = new VagaTipo();
                        vagaTipo.Id = int.Parse(dados["id"].ToString());
                        vagaTipo.Descricao = dados["descricao"].ToString();
                        vagaTipos.Add(vagaTipo);
                    }
                }

            } catch (Exception e) {
                vagaTipos = null;
            } finally {
                con.Close();
            }

            return vagaTipos;
        }

        public List<VagaTipo> buscarPorDescricao(string descricao) {
            var con = new MySqlConnection(dbConfig);
            var vagaTipos = new List<VagaTipo>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM vagas_tipos WHERE descricao LIKE @descricao";
                query.Parameters.AddWithValue("@descricao", "%" + descricao + "%");
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var vagaTipo = new VagaTipo();
                        vagaTipo.Id = int.Parse(dados["id"].ToString());
                        vagaTipo.Descricao = dados["descricao"].ToString();
                        vagaTipos.Add(vagaTipo);
                    }
                } else {
                    vagaTipos = null;
                }

            } catch (Exception e) {
                vagaTipos = null;
            } finally {
                con.Close();
            }

            return vagaTipos;
        }
    }
}