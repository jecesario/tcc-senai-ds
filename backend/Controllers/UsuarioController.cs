using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuarios = Usuario.listar();
            if (usuarios == null || usuarios.Count == 0)
            {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum curso cadastrado!";
            }
            return View(usuarios);
        }

        public ActionResult Editar(int id)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Id == id || usuario.Tipo == 1)
            {
                var oUsuario = new Usuario();
                oUsuario.Id = id;
                ViewBag.Cursos = Curso.listar();
                return View(oUsuario.buscarPorId()); 
                
            }
            
            return RedirectToAction("Entrar", "Home");
        }
        [HttpPost]
        public ActionResult Editar()
        {
            // Pegando informações do form
            var usuario = new Usuario();
            usuario.Id = int.Parse(Request.Form["id"]);
            usuario.Nome = Request.Form["nome"];
            usuario.Email = Request.Form["email"];
            usuario.Senha = Request.Form["senha"];
            usuario.Turma = Request.Form["turma"];
            usuario.Ano = Request.Form["ano"];
            usuario.Tipo = (Request.Form["tipo"] == null) ? 0 : int.Parse(Request.Form["tipo"]);
            usuario.CursoId = Request.Form["curso"];
            var logado = (Usuario)Session["usuario"];
            var emailAntigo = usuario.buscarPorId().Email;

            // Verificando se é o próprio usuário se editando ou se é um admin
            //if(usuario.Id == logado.Id)
            //{
                if (usuario.buscarPorEmail() == null || usuario.buscarPorEmail().Email == emailAntigo)
                {
                    if (usuario.editar())
                    {

                        if (logado.Tipo == 1)
                        {
                            TempData["alertSucesso"] = "Usuário editado com sucesso!";
                            if (logado.Id == usuario.Id)
                            {
                                return RedirectToAction("Sair", "Home");
                            }
                            return RedirectToAction("Index", "Usuario");
                        }
                        else
                        {
                            TempData["alertSucesso"] = "Usuário editado com sucesso! Efetue login abaixo.";
                            return RedirectToAction("Sair", "Home");
                        }

                    }
                    else
                    {
                        TempData["alertErro"] = "Ocorreu um erro ao editar Usuário!";
                    }
                }
                else
                {
                    TempData["alertErro"] = "E-mail já cadastrado por outro usuário!";
                }
            //} else
            //{
            //    if (usuario.buscarPorEmail() == null || usuario.buscarPorEmail().Email == emailAntigo)
            //    {
            //        if (usuario.editar())
            //        {
            //            TempData["alertSucesso"] = "Usuário editado com sucesso!";
            //            return RedirectToAction("Index", "Usuario");

            //        }
            //        else
            //        {
            //            TempData["alertErro"] = "Ocorreu um erro ao editar Usuário!";
            //        }
            //    }
            //    else
            //    {
            //        TempData["alertErro"] = "E-mail já cadastrado por outro usuário!";
            //    }
            //}

            return RedirectToAction("Editar/"+usuario.Id, "Usuario");

        }

        public ActionResult Apagar(int id)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var oUsuario = new Usuario();
            oUsuario.Id = id;
            if (oUsuario.apagar())
            {
                TempData["alertSucesso"] = "Usuário apagado com sucesso!";
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao deletar Usuário!";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Buscar(string nome)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var oUsuario = new Usuario();
            return View(oUsuario.buscarPorNome(nome));
        }
    }
}