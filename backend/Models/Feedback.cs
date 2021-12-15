using backend.Models.Dtos;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models {
    public class Feedback {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public int usuarioId { get; set; }

        public bool cadastrar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO feedbacks (mensagem, usuario_id) VALUES (@mensagem, @usuarioId)";
                query.Parameters.AddWithValue("@mensagem", Mensagem);
                query.Parameters.AddWithValue("@usuarioId", usuarioId);

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

        public static List<ListarFeedbackResponse> listar() {
            var con = new MySqlConnection(dbConfig);
            var feedbacks = new List<ListarFeedbackResponse>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM feedbacks ORDER BY id DESC";
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var feedback = new ListarFeedbackResponse();
                        feedback.Id = dados.GetInt32("id");
                        feedback.Mensagem = dados.GetString("mensagem");
                        var usuario = new Usuario();
                        usuario.Id = dados.GetInt32("usuario_id");
                        feedback.Usuario = usuario.buscarPorId();
                        feedbacks.Add(feedback);
                    }
                }

            } catch (Exception e) {
                feedbacks = null;
            } finally {
                con.Close();
            }

            return feedbacks;
        }
    }
}