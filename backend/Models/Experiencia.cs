using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models {
    public class Experiencia {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Cargo { get; set; }
        public string Empregador { get; set; }
        public string Resumo { get; set; }
        public string Admissao { get; set; }
        public string Demissao { get; set; }
        public int CurriculoId { get; set; }

        public bool cadastrar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            Admissao = DateTime.Parse(Admissao).ToString("yyyy-MM-dd");
            Demissao = DateTime.Parse(Demissao).ToString("yyyy-MM-dd");

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO experiencias (cargo, empregador, resumo, admissao, demissao, curriculo_id) VALUES (@cargo, @empregador, @resumo, @admissao, @demissao, @curriculoId)";
                query.Parameters.AddWithValue("@cargo", Cargo);
                query.Parameters.AddWithValue("@empregador", Empregador);
                query.Parameters.AddWithValue("@resumo", Resumo);
                query.Parameters.AddWithValue("@admissao", Admissao);
                query.Parameters.AddWithValue("@demissao", Demissao);
                query.Parameters.AddWithValue("@curriculoId", CurriculoId);
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

        public List<Experiencia> buscarPorCurriculoId() {
            var con = new MySqlConnection(dbConfig);
            var experiencias = new List<Experiencia>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM experiencias WHERE curriculo_id = @curriculoId ORDER BY admissao DESC";
                query.Parameters.AddWithValue("@curriculoId", CurriculoId);
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var experiencia = new Experiencia();
                        experiencia.Id = dados.GetInt32("id");
                        experiencia.Cargo = dados.GetString("cargo");
                        experiencia.Empregador = dados.GetString("empregador");
                        experiencia.Resumo = dados.GetString("resumo");
                        experiencia.Admissao = dados.GetDateTime("admissao").ToString("dd/MM/yyyy");
                        experiencia.Demissao = dados.GetDateTime("demissao").ToString("dd/MM/yyyy");
                        experiencias.Add(experiencia);
                    }
                } else {
                    experiencias = null;
                }

            } catch (Exception e) {
                experiencias = null;
            } finally {
                con.Close();
            }

            return experiencias;
        }

        public bool apagar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "DELETE FROM experiencias WHERE id = @id";
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

            Admissao = DateTime.Parse(Admissao).ToString("yyyy-MM-dd");
            Demissao = DateTime.Parse(Demissao).ToString("yyyy-MM-dd");
            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE experiencias SET cargo = @cargo, empregador = @empregador, resumo = @resumo, admissao = @admissao, demissao = @demissao, curriculo_id = @curriculoId WHERE id = @id";
                query.Parameters.AddWithValue("@cargo", Cargo);
                query.Parameters.AddWithValue("@empregador", Empregador);
                query.Parameters.AddWithValue("@resumo", Resumo);
                query.Parameters.AddWithValue("@admissao", Admissao);
                query.Parameters.AddWithValue("@demissao", Demissao);
                query.Parameters.AddWithValue("@curriculoId", CurriculoId);
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

        public Experiencia buscarPorId() {
            var con = new MySqlConnection(dbConfig);
            var experiencia = new Experiencia();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM experiencias WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        experiencia.Id = dados.GetInt32("id");
                        experiencia.Cargo = dados.GetString("cargo");
                        experiencia.Empregador = dados.GetString("empregador");
                        experiencia.Resumo = dados.GetString("resumo");
                        experiencia.Admissao = dados.GetDateTime("admissao").ToString("yyyy-MM-dd");
                        experiencia.Demissao = dados.GetDateTime("demissao").ToString("yyyy-MM-dd");
                    }
                } else {
                    experiencia = null;
                }

            } catch (Exception e) {
                experiencia = null;
            } finally {
                con.Close();
            }

            return experiencia;
        }

    }
}