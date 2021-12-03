using backend.Models.Dtos;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models {
    public class Curriculo {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Github { get; set; }
        public string Linkedin { get; set; }
        public string Telefone { get; set; }
        public string Resumo { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string DataEdicao { get; set; }
        public string Anexo { get; set; }
        public string UsuarioId { get; set; }

        public static List<ListarCurriculoResponse> listar() {
            var con = new MySqlConnection(dbConfig);
            var curriculos = new List<ListarCurriculoResponse>();

            con.Open();
            var query = con.CreateCommand();
            query.CommandText = "SELECT curriculos.id, telefone, cidade, usuarios.nome, GROUP_CONCAT(habilidades.nome SEPARATOR ', ') AS habilidades FROM curriculos INNER JOIN usuarios ON usuario_id = usuarios.id INNER JOIN habilidades_curriculos ON curriculos.id = habilidades_curriculos.curriculo_id INNER JOIN habilidades ON habilidades.id = habilidades_curriculos.habilidade_id GROUP BY usuario_id;";
            var dados = query.ExecuteReader();

            if (dados.HasRows) {
                while (dados.Read()) {
                    var curriculo = new ListarCurriculoResponse();
                    curriculo.Id = dados.GetInt32("id");
                    curriculo.Nome = dados["nome"].ToString();
                    curriculo.Telefone = dados["telefone"].ToString();
                    curriculo.Cidade = dados["cidade"].ToString();
                    curriculo.Habilidades = dados["habilidades"].ToString();
                    curriculos.Add(curriculo);
                }
            } else {
                curriculos = null;
            }
            con.Close();
            return curriculos;
        }

        public List<ListarCurriculoResponse> buscarPorHabilidades(string habilidades) {
            var con = new MySqlConnection(dbConfig);
            var curriculos = new List<ListarCurriculoResponse>();

            var query = con.CreateCommand();
            var items = habilidades.Split(',');

            var queryString = " HAVING ";

            for (var i = 0; i < items.Length; i++) {
                if (i == 0) {
                    queryString += "habilidades LIKE @hab" + i;
                    query.Parameters.AddWithValue("@hab" + i, '%' + items[i].Trim() + '%'); ;
                } else {
                    queryString += " AND habilidades LIKE @hab" + i;
                    query.Parameters.AddWithValue("@hab" + i, '%' + items[i].Trim() + '%');
                }
            }

            con.Open();
            query.CommandText = "SELECT curriculos.id, telefone, cidade, usuarios.nome, GROUP_CONCAT(habilidades.nome SEPARATOR ', ') AS habilidades FROM curriculos INNER JOIN usuarios ON usuario_id = usuarios.id INNER JOIN habilidades_curriculos ON curriculos.id = habilidades_curriculos.curriculo_id INNER JOIN habilidades ON habilidades.id = habilidades_curriculos.habilidade_id GROUP BY usuario_id" + queryString;
            var dados = query.ExecuteReader();

            if (dados.HasRows) {
                while (dados.Read()) {
                    var curriculo = new ListarCurriculoResponse();
                    curriculo.Id = dados.GetInt32("id");
                    curriculo.Nome = dados["nome"].ToString();
                    curriculo.Telefone = dados["telefone"].ToString();
                    curriculo.Cidade = dados["cidade"].ToString();
                    curriculo.Habilidades = dados["habilidades"].ToString();
                    curriculos.Add(curriculo);
                }
            } else {
                curriculos = null;
            }
            con.Close();
            return curriculos;
        }

        public Curriculo buscarPorUsuarioId() {
            var con = new MySqlConnection(dbConfig);
            var curriculo = new Curriculo();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM curriculos WHERE usuario_id = @usuarioId";
                query.Parameters.AddWithValue("@usuarioId", UsuarioId);
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        curriculo.Id = dados.GetInt32("id");
                        curriculo.Github = dados.GetString("github");
                        curriculo.Linkedin = dados.GetString("linkedin");
                        curriculo.Telefone = dados.GetString("telefone");
                        curriculo.Resumo = dados.GetString("resumo");
                        curriculo.Endereco = dados.GetString("endereco");
                        curriculo.Cidade = dados.GetString("cidade");
                        curriculo.Estado = dados.GetString("estado");
                        curriculo.DataEdicao = dados.GetDateTime("data_edicao").ToString();
                        curriculo.Anexo = dados["anexo"].ToString();
                    }
                } else {
                    curriculo = null;
                }

            } catch (Exception e) {
                curriculo = null;
            } finally {
                con.Close();
            }

            return curriculo;
        }

        public DetalharCurriculoResponse buscarPorId() {
            var con = new MySqlConnection(dbConfig);
            var curriculo = new DetalharCurriculoResponse();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM curriculos WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        curriculo.Id = int.Parse(dados["id"].ToString());
                        curriculo.Github = dados["github"].ToString();
                        curriculo.Linkedin = dados["linkedin"].ToString();
                        curriculo.Telefone = dados["telefone"].ToString();
                        curriculo.Resumo = dados["resumo"].ToString();
                        curriculo.Endereco = dados["endereco"].ToString();
                        curriculo.Localidade = dados["cidade"].ToString() + "-" + dados["estado"].ToString();
                        curriculo.DataEdicao = dados.GetDateTime("data_edicao").ToString();

                        // Preenchendo usuário
                        var usuario = new Usuario();
                        usuario.Id = int.Parse(dados["usuario_id"].ToString());
                        curriculo.Usuario = usuario.buscarPorId();

                        // Guardando curso do usuário
                        var curso = new Curso();
                        curso.Id = int.Parse(curriculo.Usuario.CursoId);
                        curriculo.Curso = curso.buscarPorId().Nome;

                        // Guardado habilidades do usuário logado
                        var habilidadeCurriculo = new HabilidadeCurriculo();
                        habilidadeCurriculo.CurriculoId = curriculo.Id;
                        var listaHabilidades = habilidadeCurriculo.buscarPorCurriculoId();

                        // Pegando cada ID de habilidade encontrado na tabela, buscando informação na tabela de Habilidades e guardando em uma lista para enviar para a View
                        var habilidades = new List<Habilidade>();
                        foreach (var i in listaHabilidades) {
                            var habilidade = new Habilidade();
                            habilidade.Id = i.HabilidadeId;
                            habilidades.Add(habilidade.buscarPorId());
                        }
                        curriculo.Habilidades = habilidades;

                        // Guardando as experiencias do usuário baseadas pelo id do curriculo
                        var experiencia = new Experiencia();
                        experiencia.CurriculoId = curriculo.Id;
                        curriculo.Experiencias = experiencia.buscarPorCurriculoId();

                        // Guardando as formações do usuário baseadas pelo id do curriculo
                        var formacao = new Formacao();
                        formacao.CurriculoId = curriculo.Id;
                        curriculo.Formacoes = formacao.buscarPorCurriculoId();

                    }
                } else {
                    curriculo = null;
                }

            } catch (Exception e) {
                curriculo = null;
            } finally {
                con.Close();
            }

            return curriculo;
        }

        public bool cadastrar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO curriculos (github, linkedin, telefone, resumo, endereco, cidade, estado, data_edicao, usuario_id) VALUES (@github, @linkedin, @telefone, @resumo, @endereco, @cidade, @estado, @dataEdicao, @usuarioId)";
                query.Parameters.AddWithValue("@github", Github);
                query.Parameters.AddWithValue("@linkedin", Linkedin);
                query.Parameters.AddWithValue("@telefone", Telefone);
                query.Parameters.AddWithValue("@resumo", Resumo);
                query.Parameters.AddWithValue("@endereco", Endereco);
                query.Parameters.AddWithValue("@cidade", Cidade);
                query.Parameters.AddWithValue("@estado", Estado);
                query.Parameters.AddWithValue("@dataEdicao", DateTime.Now.Date);
                query.Parameters.AddWithValue("@usuarioId", UsuarioId);

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
                query.CommandText = "UPDATE curriculos SET github = @github, linkedin = @linkedin, telefone = @telefone, resumo = @resumo, endereco = @endereco, cidade = @cidade, estado = @estado, usuario_id = @usuarioId WHERE id = @id";
                query.Parameters.AddWithValue("@github", Github);
                query.Parameters.AddWithValue("@linkedin", Linkedin);
                query.Parameters.AddWithValue("@telefone", Telefone);
                query.Parameters.AddWithValue("@resumo", Resumo);
                query.Parameters.AddWithValue("@endereco", Endereco);
                query.Parameters.AddWithValue("@cidade", Cidade);
                query.Parameters.AddWithValue("@estado", Estado);
                query.Parameters.AddWithValue("@usuarioId", UsuarioId);
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

        public void atualizarDataEdicao() {
            var con = new MySqlConnection(dbConfig);

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE curriculos SET data_edicao = @dataEdicao WHERE id = @id";
                query.Parameters.AddWithValue("@dataEdicao", DateTime.Now);
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
            } catch (Exception e) {
            } finally {
                con.Close();
            }
        }

        public bool anexarDoc() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE curriculos SET anexo = @anexo WHERE id = @id";
                query.Parameters.AddWithValue("@anexo", Anexo);
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

        public bool deletarDoc() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE curriculos SET anexo = @anexo WHERE id = @id";
                query.Parameters.AddWithValue("@anexo", null);
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
    }
}