using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models {
    public class Habilidade {
        private const string dbConfig = "Server=soloid.com.br;Port=3306;Database=soloid_learn;Uid=soloid_learn;Pwd=MacacoNaoMataMacaco";
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

                while (dados.Read()) {
                    var habilidade = new Habilidade();
                    habilidade.Id = int.Parse(dados["id"].ToString());
                    habilidade.Nome = dados["nome"].ToString();
                    habilidades.Add(habilidade);
                }
                Console.WriteLine("Sucesso ao listar todas as habilidades!");
            } catch (Exception e) {
                Console.WriteLine("Erro ao listar as habilidades!" + e.Message);
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
                query.ExecuteNonQuery();
                resp = true;
                Console.WriteLine("Habilidade cadastrada com sucesso!");
            } catch (Exception e) {
                Console.WriteLine("Erro ao cadastrar habilidade " + e.Message);
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
                query.CommandText = "DELETE FROM habilidades WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
                resp = true;
                Console.WriteLine("Habilidade deletada com sucesso!");
            } catch (Exception e) {
                Console.WriteLine("Erro ao deletar Habilidades " + e.Message);
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
                query.CommandText = "UPDATE habilidades SET nome = @nome WHERE id = @id";
                query.Parameters.AddWithValue("@nome", Nome);
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
                Console.WriteLine("Habilidade editada com sucesso!");
                resp = true;
            } catch (Exception e) {
                Console.WriteLine("Erro ao editar Habilidade! " + e.Message);
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

                while (dados.Read()) {
                    habilidade.Id = int.Parse(dados["id"].ToString());
                    habilidade.Nome = dados["nome"].ToString();
                }
                Console.WriteLine("Sucesso ao buscar habilidade por ID");
            } catch (Exception e) {
                Console.WriteLine("Erro ao buscar habilidade por Id" + e.Message);
                habilidade = null;
            } finally {
                con.Close();
            }

            return habilidade;
        }
    }
}