using backend.Models.Dtos;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Vaga
    {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Cargo { get; set; }
        public int Quantidade { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Empresa { get; set; }
        public string Requisitos { get; set; }
        public string Atribuicoes { get; set; }
        public double Remuneracao { get; set; }
        public string Beneficios { get; set; }
        public string DataPostagem { get; set; }
        public string DataLimite { get; set; }
        public string Observacoes { get; set; }
        public int VagaJornadaId { get; set; }
        public int VagaTipoId { get; set; }
        public int UsuarioId { get; set; }

        public static List<ListarVagasResponse> listar() {
            var con = new MySqlConnection(dbConfig);
            var vagas = new List<ListarVagasResponse>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT id, cargo, cidade, estado, requisitos, atribuicoes, remuneracao, beneficios, data_postagem FROM vagas";
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var vaga = new ListarVagasResponse();
                        vaga.Id = dados.GetInt32("id");
                        vaga.DataPostagem = dados.GetDateTime("data_postagem").ToString("dd/MM/yyyy");
                        vaga.Cargo = dados.GetString("cargo");
                        vaga.Localidade = dados.GetString("cidade") + "-" + dados.GetString("estado");
                        vaga.Remuneracao = dados.GetDouble("remuneracao");
                        vaga.Beneficios = dados.GetString("beneficios");
                        vaga.Requisitos = dados.GetString("requisitos");
                        vaga.Atribuicoes = dados.GetString("atribuicoes");
                        vagas.Add(vaga);
                    }
                }

            } catch (Exception e) {
                vagas = null;
            } finally {
                con.Close();
            }

            return vagas;
        }

        public bool cadastrar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            DataPostagem = DateTime.Parse(DataPostagem).ToString("yyyy-MM-dd");
            DataLimite = DateTime.Parse(DataLimite).ToString("yyyy-MM-dd");

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO vagas (cargo, quantidade, cidade, estado, empresa, requisitos, atribuicoes, remuneracao, beneficios, data_postagem, data_limite, observacoes, id_vagas_modalidades, id_vagas_tipos, id_usuario)VALUES (@cargo, @quantidade, @cidade, @estado, @empresa, @requisitos, @atribuicoes, @remuneracao, @beneficios, @data_postagem, @data_limite, @observacoes, @id_vagas_modalidades, @id_vagas_tipos, @id_usuario)";
                query.Parameters.AddWithValue("@cargo", Cargo);
                query.Parameters.AddWithValue("@quantidade", Quantidade);
                query.Parameters.AddWithValue("@cidade", Cidade);
                query.Parameters.AddWithValue("@estado", Estado);
                query.Parameters.AddWithValue("@empresa", Empresa);
                query.Parameters.AddWithValue("@requisitos", Requisitos);
                query.Parameters.AddWithValue("@atribuicoes", Atribuicoes);
                query.Parameters.AddWithValue("@remuneracao", Remuneracao);
                query.Parameters.AddWithValue("@beneficios", Beneficios);
                query.Parameters.AddWithValue("@data_postagem", DataPostagem);
                query.Parameters.AddWithValue("@data_limite", DataLimite);
                query.Parameters.AddWithValue("@observacoes", Observacoes);
                query.Parameters.AddWithValue("@id_vagas_modalidades", VagaJornadaId);
                query.Parameters.AddWithValue("@id_vagas_tipos", VagaTipoId);
                query.Parameters.AddWithValue("@id_usuario", UsuarioId);
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

        public Vaga buscarPorId() {
            var con = new MySqlConnection(dbConfig);
            var vaga = new Vaga();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM vagas WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        vaga.Id = dados.GetInt32("id");
                        vaga.Cargo = dados.GetString("cargo");
                        vaga.Quantidade = dados.GetInt32("quantidade");
                        vaga.Cidade = dados.GetString("cidade");
                        vaga.Estado = dados.GetString("estado");
                        vaga.Empresa = dados.GetString("empresa");
                        vaga.Requisitos = dados.GetString("requisitos");
                        vaga.Atribuicoes = dados.GetString("atribuicoes");
                        vaga.Beneficios = dados.GetString("beneficios");
                        vaga.DataPostagem = dados.GetDateTime("data_postagem").ToString("dd/MM/yyyy");
                        vaga.DataLimite = dados.GetDateTime("data_limite").ToString("yyyy-MM-dd");
                        vaga.Observacoes = dados.GetString("observacoes");
                        vaga.VagaJornadaId = dados.GetInt32("id_vagas_modalidades");
                        vaga.VagaTipoId = dados.GetInt32("id_vagas_tipos");
                        vaga.UsuarioId = dados.GetInt32("id_usuario");
                    }
                } else {
                    vaga = null;
                }

            } catch (Exception e) {
                vaga = null;
            } finally {
                con.Close();
            }

            return vaga;
        }

        public bool apagar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "DELETE FROM vagas WHERE id = @id";
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

            DataPostagem = DateTime.Parse(DataPostagem).ToString("yyyy-MM-dd");
            DataLimite = DateTime.Parse(DataLimite).ToString("yyyy-MM-dd");

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE vagas SET cargo = @cargo, quantidade = @quantidade, cidade = @cidade, estado = @estado, empresa = @empresa, requisitos = @requisitos, atribuicoes = @atribuicoes, remuneracao = @remuneracao, beneficios = @beneficios, data_postagem = @data_postagem, data_limite = @data_limite, observacoes = @observacoes, id_vagas_modalidades = @id_vagas_modalidades, id_vagas_tipos = @id_vagas_tipos, id_usuario = @id_usuario WHERE id = @id";
                query.Parameters.AddWithValue("@cargo", Cargo);
                query.Parameters.AddWithValue("@quantidade", Quantidade);
                query.Parameters.AddWithValue("@cidade", Cidade);
                query.Parameters.AddWithValue("@estado", Estado);
                query.Parameters.AddWithValue("@empresa", Empresa);
                query.Parameters.AddWithValue("@requisitos", Requisitos);
                query.Parameters.AddWithValue("@atribuicoes", Atribuicoes);
                query.Parameters.AddWithValue("@remuneracao", Remuneracao);
                query.Parameters.AddWithValue("@beneficios", Beneficios);
                query.Parameters.AddWithValue("@data_postagem", DataPostagem);
                query.Parameters.AddWithValue("@data_limite", DataLimite);
                query.Parameters.AddWithValue("@observacoes", Observacoes);
                query.Parameters.AddWithValue("@id_vagas_modalidades", VagaJornadaId);
                query.Parameters.AddWithValue("@id_vagas_tipos", VagaTipoId);
                query.Parameters.AddWithValue("@id_usuario", UsuarioId);
                query.Parameters.AddWithValue("@id", Id);
                if (query.ExecuteNonQuery() > 0) {
                    resp = true;
                }
            } catch (Exception e) {
                string erro = e.Message;
                resp = false;
            } finally {
                con.Close();
            }

            return resp;
        }

    }
}