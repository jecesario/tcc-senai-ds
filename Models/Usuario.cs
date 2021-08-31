﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models {
    public class Usuario
    {
        // private const string dbConfig = "Server=esn509vmysql;Port=3306;Database=senai_curriculos;Uid=aluno;Pwd=Senai1234";
        //private const string dbConfig = "Server=soloid.com.br;Port=3306;Database=soloid_learn;Uid=soloid_learn;Pwd=MacacoNaoMataMacaco";
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public int Tipo { get; set; }

        public Curso Curso { get; set; }

        public Usuario entrar()
        {
            var con = new MySqlConnection(dbConfig);
            var usuario = new Usuario();

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM usuarios WHERE email = @email AND senha = @senha";
                query.Parameters.AddWithValue("@email", Email);
                query.Parameters.AddWithValue("@senha", Senha);
                var dados = query.ExecuteReader();
                if (dados.Read())
                {
                    usuario.Id = int.Parse(dados["id"].ToString());
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.Senha = dados["senha"].ToString();
                    usuario.Tipo = int.Parse(dados["tipo"].ToString());
                }
                else
                {
                    usuario = null;
                }
                Console.WriteLine("Usuário logado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro ao efeturar login" + e.Message);

            }
            finally
            {
                con.Close();
            }

            return usuario;
        }

        public bool cadastrar()
        {
            bool resp = false;
            var con = new MySqlConnection(dbConfig);

            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao tentar adicionar usuário!" + e.Message);
                resp = false;
            }
            finally
            {
                con.Close();
            }

            return resp;
        }

        public static List<Usuario> listar()
        {
            var con = new MySqlConnection(dbConfig);
            var usuarios = new List<Usuario>();

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM usuarios";
                var dados = query.ExecuteReader();

                while (dados.Read())
                {
                    var usuario = new Usuario();
                    usuario.Id = int.Parse(dados["id"].ToString());
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.Senha = dados["senha"].ToString();
                    usuario.Tipo = int.Parse(dados["tipo"].ToString());
                    usuarios.Add(usuario);
                    Console.WriteLine("Usuários listados com sucesso!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao tentar listar usuários!" + e.Message);
                usuarios = null;
            }
            finally
            {
                con.Close();
            }

            return usuarios;
        }

        public bool apagar()
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "DELETE FROM usuarios WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
                resp = true;
                Console.WriteLine("Usuario deletado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao deletar Usuario " + e.Message);
                resp = false;
            }
            finally
            {
                con.Close();
            }

            return resp;
        }

        public bool editar()
        {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE usuarios SET nome = @nome, email = @email, senha = @senha, tipo = @tipo WHERE id = @id";
                query.Parameters.AddWithValue("@nome", Nome);
                query.Parameters.AddWithValue("@email", Email);
                query.Parameters.AddWithValue("@senha", Senha);
                query.Parameters.AddWithValue("@tipo", Tipo);
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
                Console.WriteLine("Usuario editado com sucesso!");
                resp = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao editar Usuario! " + e.Message);
                resp = false;
            }
            finally
            {
                con.Close();
            }

            return resp;
        }

        public Usuario buscarPorId()
        {
            var con = new MySqlConnection(dbConfig);
            var usuario = new Usuario();

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM usuarios WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();

                while (dados.Read())
                {
                    usuario.Id = int.Parse(dados["id"].ToString());
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.Senha = dados["senha"].ToString();
                    usuario.Tipo = int.Parse(dados["tipo"].ToString());
                }
                Console.WriteLine("Sucesso ao buscar Usuario por ID");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao buscar Usuario por Id" + e.Message);
                usuario = null;
            }
            finally
            {
                con.Close();
            }

            return usuario;
        }
    }
}