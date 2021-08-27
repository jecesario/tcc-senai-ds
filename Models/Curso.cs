using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models {
    public class Curso {
        //private const string dbConfig = "Server=soloid.com.br;Port=3306;Database=soloid_learn;Uid=soloid_learn;Pwd=MacacoNaoMataMacaco";
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Nome { get; set; }

        public bool cadastrar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO cursos (nome) VALUES (@nome)";
                query.Parameters.AddWithValue("@nome", Nome);
                query.ExecuteNonQuery();
                resp = true;
                Console.WriteLine("Curso cadastrado com sucesso!");
            } catch (Exception e) {
                Console.WriteLine("Erro ao cadastrar curso " + e.Message);
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
                query.CommandText = "DELETE FROM cursos WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
                resp = true;
                Console.WriteLine("Curso deletado com sucesso!");
            } catch (Exception e) {
                Console.WriteLine("Erro ao deletar Cursos " + e.Message);
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
                query.CommandText = "UPDATE cursos SET nome = @nome WHERE id = @id";
                query.Parameters.AddWithValue("@nome", Nome);
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
                Console.WriteLine("Curso editado com sucesso!");
                resp = true;
            } catch (Exception e) {
                Console.WriteLine("Erro ao editar Curso! " + e.Message);
                resp = false;
            } finally {
                con.Close();
            }

            return resp;
        }

        public Curso buscarPorId() {
            var con = new MySqlConnection(dbConfig);
            var curso = new Curso();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM cursos WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();

                while (dados.Read()) {
                    curso.Id = int.Parse(dados["id"].ToString());
                    curso.Nome = dados["nome"].ToString();
                }
                Console.WriteLine("Sucesso ao buscar curso por ID");
            } catch (Exception e) {
                Console.WriteLine("Erro ao buscar curso por Id" + e.Message);
                curso = null;
            } finally {
                con.Close();
            }

            return curso;
        }

        public static List<Curso> listar() {
            var con = new MySqlConnection(dbConfig);
            var cursos = new List<Curso>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM cursos";
                var dados = query.ExecuteReader();

                while (dados.Read()) {
                    var curso = new Curso();
                    curso.Id = int.Parse(dados["id"].ToString());
                    curso.Nome = dados["nome"].ToString();
                    cursos.Add(curso);
                }
                Console.WriteLine("Sucesso ao listar todos os cursos!");
            } catch (Exception e) {
                Console.WriteLine("Erro ao listar os cursos!" + e.Message);
                cursos = null;
            } finally {
                con.Close();
            }

            return cursos;
        }
    }
}