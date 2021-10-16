﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models {
    public class TiposFormacao {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Nome { get; set; }

        public bool cadastrar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO tipos_formacao (nome) VALUES (@nome)";
                query.Parameters.AddWithValue("@nome", Nome);

                if (query.ExecuteNonQuery() > 0) {
                    resp = true;
                }
            }
            catch (Exception e) {
                resp = false;
            }
            finally {
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
                query.CommandText = "DELETE FROM tipos_formacao WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                if (query.ExecuteNonQuery() > 0) {
                    resp = true;
                }
            }
            catch (Exception e) {
                resp = false;
            }
            finally {
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
                query.CommandText = "UPDATE tipos_formacao SET nome = @nome WHERE id = @id";
                query.Parameters.AddWithValue("@nome", Nome);
                query.Parameters.AddWithValue("@id", Id);
                if (query.ExecuteNonQuery() > 0) {
                    resp = true;
                }
            }
            catch (Exception e) {
                resp = false;
            }
            finally {
                con.Close();
            }

            return resp;
        }

        public TiposFormacao buscarPorId() {
            var con = new MySqlConnection(dbConfig);
            var tiposFormacao = new TiposFormacao();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM tipos_formacao WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        tiposFormacao.Id = int.Parse(dados["id"].ToString());
                        tiposFormacao.Nome = dados["nome"].ToString();
                    }
                }
                else {
                    tiposFormacao = null;
                }

            }
            catch (Exception e) {
                tiposFormacao = null;
            }
            finally {
                con.Close();
            }

            return tiposFormacao;
        }

        public static List<TiposFormacao> listar() {
            var con = new MySqlConnection(dbConfig);
            var tiposFormacaoList = new List<TiposFormacao>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM tipos_formacao";
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var tiposFormacao = new TiposFormacao();
                        tiposFormacao.Id = int.Parse(dados["id"].ToString());
                        tiposFormacao.Nome = dados["nome"].ToString();
                        tiposFormacaoList.Add(tiposFormacao);
                    }
                }

            }
            catch (Exception e) {
                tiposFormacaoList = null;
            }
            finally {
                con.Close();
            }

            return tiposFormacaoList;
        }
    }
}