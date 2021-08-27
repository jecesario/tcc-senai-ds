using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models {
    public class Usuario {
        // private const string dbConfig = "Server=esn509vmysql;Port=3306;Database=senai_curriculos;Uid=aluno;Pwd=Senai1234";
        private const string dbConfig = "Server=soloid.com.br;Port=3306;Database=soloid_learn;Uid=soloid_learn;Pwd=MacacoNaoMataMacaco";
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public int Tipo { get; set; }

        public Usuario entrar() {
            var con = new MySqlConnection(dbConfig);
            var usuario = new Usuario();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM usuarios WHERE email = @email AND senha = @senha";
                query.Parameters.AddWithValue("@email", Email);
                query.Parameters.AddWithValue("@senha", Senha);
                var dados = query.ExecuteReader();
                if (dados.Read()) {
                    usuario.Id = int.Parse(dados["id"].ToString());
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.Senha = dados["senha"].ToString();
                    usuario.Tipo = int.Parse(dados["tipo"].ToString());
                } else {
                    usuario = null;
                }
                Console.WriteLine("Usuário logado com sucesso!");
            } catch (Exception e) {
                Console.WriteLine("Ocorreu um erro ao efeturar login" + e.Message);

            } finally {
                con.Close();
            }

            return usuario;
        }

        public bool cadastrar() {
            bool resp = false;
            var con = new MySqlConnection(dbConfig);

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "INSERT INTO usuarios (nome, email, senha, tipo) VALUES (@nome, @email, @senha, @tipo)";
                query.Parameters.AddWithValue("@nome", Nome);
                query.Parameters.AddWithValue("@email", Email);
                query.Parameters.AddWithValue("@senha", Senha);
                query.Parameters.AddWithValue("@tipo", Tipo);
                query.ExecuteNonQuery();
                Console.WriteLine("Usuário adicionado com sucesso!");
                resp = true;
            } catch (Exception e) {
                Console.WriteLine("Erro ao tentar adicionar usuário!" + e.Message);
                resp = false;
            } finally {
                con.Close();
            }

            return resp;
        }

        public static List<Usuario> listar() {
            var con = new MySqlConnection(dbConfig);
            var usuarios = new List<Usuario>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM usuarios";
                var dados = query.ExecuteReader();

                while (dados.Read()) {
                    var usuario = new Usuario();
                    usuario.Id = int.Parse(dados["id"].ToString());
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.Senha = dados["senha"].ToString();
                    usuario.Tipo = int.Parse(dados["tipo"].ToString());
                    usuarios.Add(usuario);
                    Console.WriteLine("Usuários listados com sucesso!");
                }
            } catch (Exception e) {
                Console.WriteLine("Erro ao tentar listar usuários!" + e.Message);
                usuarios = null;
            } finally {
                con.Close();
            }

            return usuarios;
        }
    }
}