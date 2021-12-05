﻿using backend.Models.Dtos;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Usuario
    {
        private static string dbConfig = ConfigurationManager.ConnectionStrings["dbConfigSenai"].ConnectionString;
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Turma { get; set; }
        public string Ano { get; set; }
        public int Tipo { get; set; }
        public string CursoId { get; set; }

        public Usuario entrar() {
            var con = new MySqlConnection(dbConfig);
            var usuario = new Usuario();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM usuarios WHERE email = @email AND senha = @senha";
                query.Parameters.AddWithValue("@email", Email);
                query.Parameters.AddWithValue("@senha", Util.criptografar(Senha));
                var dados = query.ExecuteReader();
                if (dados.Read()) {
                    usuario.Id = int.Parse(dados["id"].ToString());
                    usuario.Nome = dados["nome"].ToString();
                    usuario.Email = dados["email"].ToString();
                    usuario.Senha = dados["senha"].ToString();
                    usuario.Turma = dados["turma"].ToString();
                    usuario.Ano = dados["ano"].ToString();
                    usuario.Tipo = int.Parse(dados["tipo"].ToString());
                    usuario.CursoId = dados["curso_id"].ToString();
                } else {
                    usuario = null;
                }
            } catch (Exception e) {
                usuario = null;
            } finally {
                con.Close();
            }

            return usuario;
        }

        public bool cadastrar() {
            bool resp = false;
            var con = new MySqlConnection(dbConfig);

            if (CursoId.Equals("")) {
                CursoId = null;
            }

            if (buscarPorEmail() == null) {
                try {
                    con.Open();
                    var query = con.CreateCommand();
                    query.CommandText = "INSERT INTO usuarios (nome, email, senha, turma, ano, tipo, token, curso_id) VALUES (@nome, @email, @senha, @turma, @ano, @tipo, @token, @cursoId)";
                    query.Parameters.AddWithValue("@nome", Nome);
                    query.Parameters.AddWithValue("@email", Email);
                    query.Parameters.AddWithValue("@senha", Util.criptografar(Senha));
                    query.Parameters.AddWithValue("@turma", Turma);
                    query.Parameters.AddWithValue("@ano", Ano);
                    query.Parameters.AddWithValue("@tipo", Tipo);
                    query.Parameters.AddWithValue("@token", Util.criptografar(Email+DateTime.Now.Millisecond));
                    query.Parameters.AddWithValue("@cursoId", CursoId);
                    if (query.ExecuteNonQuery() > 0) {
                        resp = true;
                    }

                } catch (Exception e) {
                    resp = false;
                } finally {
                    con.Close();
                }
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

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var usuario = new Usuario();
                        usuario.Id = int.Parse(dados["id"].ToString());
                        usuario.Nome = dados["nome"].ToString();
                        usuario.Email = dados["email"].ToString();
                        usuario.Senha = dados["senha"].ToString();
                        usuario.Turma = dados["turma"].ToString();
                        usuario.Ano = dados["ano"].ToString();
                        usuario.Tipo = int.Parse(dados["tipo"].ToString());
                        usuario.CursoId = dados["curso_id"].ToString();
                        usuarios.Add(usuario);
                    }
                } else {
                    usuarios = null;
                }
            } catch (Exception e) {
                usuarios = null;
            } finally {
                con.Close();
            }

            return usuarios;
        }

        public bool apagar() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "DELETE FROM usuarios WHERE id = @id";
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

            if (CursoId.Equals("0")) {
                CursoId = null;
            }

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE usuarios SET nome = @nome, email = @email, turma = @turma, ano = @ano, tipo = @tipo, curso_id = @cursoId WHERE id = @id";
                query.Parameters.AddWithValue("@nome", Nome);
                query.Parameters.AddWithValue("@email", Email);
                query.Parameters.AddWithValue("@turma", Turma);
                query.Parameters.AddWithValue("@ano", Ano);
                query.Parameters.AddWithValue("@tipo", Tipo);
                query.Parameters.AddWithValue("@cursoId", CursoId);
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

        public bool atualizarSenha() {
            var con = new MySqlConnection(dbConfig);
            bool resp = false;

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "UPDATE usuarios SET senha = @senha WHERE id = @id";
                query.Parameters.AddWithValue("@senha", Util.criptografar(Senha));
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

        public Usuario buscarPorId() {
            var con = new MySqlConnection(dbConfig);
            var usuario = new Usuario();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM usuarios WHERE id = @id";
                query.Parameters.AddWithValue("@id", Id);
                var dados = query.ExecuteReader();
                if (dados.HasRows) {
                    while (dados.Read()) {
                        usuario.Id = int.Parse(dados["id"].ToString());
                        usuario.Nome = dados["nome"].ToString();
                        usuario.Email = dados["email"].ToString();
                        usuario.Senha = dados["senha"].ToString();
                        usuario.Turma = dados["turma"].ToString();
                        usuario.Ano = dados["ano"].ToString();
                        usuario.Tipo = int.Parse(dados["tipo"].ToString());
                        usuario.CursoId = dados["curso_id"].ToString();
                    }
                } else {
                    usuario = null;
                }

            } catch (Exception e) {
                usuario = null;
            } finally {
                con.Close();
            }

            return usuario;
        }

        public Usuario buscarPorEmail() {
            var con = new MySqlConnection(dbConfig);
            var usuario = new Usuario();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM usuarios WHERE email = @email";
                query.Parameters.AddWithValue("@email", Email);
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        usuario.Id = int.Parse(dados["id"].ToString());
                        usuario.Nome = dados["nome"].ToString();
                        usuario.Email = dados["email"].ToString();
                        usuario.Senha = dados["senha"].ToString();
                        usuario.Turma = dados["turma"].ToString();
                        usuario.Ano = dados["ano"].ToString();
                        usuario.Tipo = int.Parse(dados["tipo"].ToString());
                        usuario.CursoId = dados["curso_id"].ToString();
                    }
                } else {
                    usuario = null;
                }
            } catch (Exception e) {
                usuario = null;
            } finally {
                con.Close();
            }

            return usuario;
        }
        public List<Usuario> buscarPorNome(string nome) {
            var con = new MySqlConnection(dbConfig);
            var usuarios = new List<Usuario>();

            try {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT * FROM usuarios WHERE nome LIKE @nome";
                query.Parameters.AddWithValue("@nome", "%" + nome + "%");
                var dados = query.ExecuteReader();

                if (dados.HasRows) {
                    while (dados.Read()) {
                        var usuario = new Usuario();
                        usuario.Id = dados.GetInt32("id");
                        usuario.Nome = dados.GetString("nome");
                        usuario.Turma = dados.GetString("turma");
                        usuario.Ano = dados.GetString("ano");
                        usuario.Email = dados.GetString("email");
                        usuarios.Add(usuario);
                    }
                } else {
                    usuarios = null;
                }

            } catch (Exception e) {
                usuarios = null;
            } finally {
                con.Close();
            }

            return usuarios;
        }

        public UsuarioLoginResponse entrarApi()
        {
            var con = new MySqlConnection(dbConfig);
            var usuario = new UsuarioLoginResponse();

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT token FROM usuarios WHERE email = @email AND senha = @senha AND tipo = @tipo";
                query.Parameters.AddWithValue("@email", Email);
                query.Parameters.AddWithValue("@senha", Util.criptografar(Senha));
                query.Parameters.AddWithValue("@tipo", 1);
                var dados = query.ExecuteReader();
                if (dados.Read())
                {
                    usuario.Token = dados.GetString("token");
                }
                else
                {
                    usuario = null;
                }
            }
            catch (Exception e)
            {
                usuario = null;
            }
            finally
            {
                con.Close();
            }

            return usuario;
        }

        public static bool validarToken(string token)
        {
            var con = new MySqlConnection(dbConfig);
            var resp = false;

            try
            {
                con.Open();
                var query = con.CreateCommand();
                query.CommandText = "SELECT token FROM usuarios WHERE token = @token AND tipo = 1";
                query.Parameters.AddWithValue("@Token", token);
                var dados = query.ExecuteReader();
                if (dados.HasRows)
                {
                    resp = true;
                }

            }
            catch (Exception e)
            {
                resp = false;
            }
            finally
            {
                con.Close();
            }

            return resp;
        }
    }
}