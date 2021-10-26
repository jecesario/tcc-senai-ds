using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class VagaJornada
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
                query.CommandText = "INSERT INTO vagas_jornadas (descricao) VALUES (@descricao)";
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
                query.CommandText = "DELETE FROM vagas_jornadas WHERE id = @id";
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
                query.CommandText = "UPDATE vagas_jornadas SET descricao = @descricao WHERE id = @id";
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

        public VagaJornada buscarPorId() {
            var con = new MySqlConnection(dbConfig);
            var vagaJornada = new VagaJornada();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM vagas_jornadas WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        vagaJornada.Id = int.Parse(dados["id"].ToString());
                        vagaJornada.Descricao = dados["descricao"].ToString();
                    }
                } else {
                    vagaJornada = null;
                }

            } catch (Exception e) {
                vagaJornada = null;
            } finally {
                con.Close();
            }

            return vagaJornada;
        }

        public static List<VagaJornada> listar() {
            var con = new MySqlConnection(dbConfig);
            var vagaJornadas = new List<VagaJornada>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM vagas_jornadas";
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var vagaJornada = new VagaJornada();
                        vagaJornada.Id = int.Parse(dados["id"].ToString());
                        vagaJornada.Descricao = dados["descricao"].ToString();
                        vagaJornadas.Add(vagaJornada);
                    }
                }

            } catch (Exception e) {
                vagaJornadas = null;
            } finally {
                con.Close();
            }

            return vagaJornadas;
        }

        public List<VagaJornada> buscarPorDescricao(string descricao) {
            var con = new MySqlConnection(dbConfig);
            var vagaJornadas = new List<VagaJornada>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM vagas_jornadas WHERE descricao LIKE @descricao";
                query.Parameters.AddWithValue("@descricao", "%" + descricao + "%");
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var vagaJornada = new VagaJornada();
                        vagaJornada.Id = int.Parse(dados["id"].ToString());
                        vagaJornada.Descricao = dados["descricao"].ToString();
                        vagaJornadas.Add(vagaJornada);
                    }
                } else {
                    vagaJornadas = null;
                }

            } catch (Exception e) {
                vagaJornadas = null;
            } finally {
                con.Close();
            }

            return vagaJornadas;
        }
    }
}