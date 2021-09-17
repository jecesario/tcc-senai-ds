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

            var usuario = new Usuario();
            usuario.Id = int.Parse(Request.Form["id"]);
            usuario.Nome = Request.Form["nome"];
            usuario.Email = Request.Form["email"];
            usuario.Senha = Request.Form["senha"];
            usuario.Turma = Request.Form["turma"];
            usuario.Ano = Request.Form["ano"];
            usuario.Tipo = int.Parse(Request.Form["tipo"]);
            usuario.CursoId = Request.Form["curso"];
            
            if(usuario.editar())
            {
                TempData["alertErro"] = Request.Form;
            } else
            {
                TempData["alertErro"] = "Tá foda" + usuario.Turma + " - " + usuario.Ano;
            }
            
            //if (usuario.editar())
            //{
            //    var logado = (Usuario)Session["usuario"];
            //    if (logado.Tipo == 1)
            //    {
            //        TempData["alertSucesso"] = "Usuário editado com sucesso!";
            //        if (logado.Id == usuario.Id)
            //        {
            //            return RedirectToAction("Sair", "Home");
            //        }
            //    }
            //    else
            //    {
            //        return RedirectToAction("Sair", "Home");
            //    }

            //    TempData["alertErro"] = Request.Form;
            //}
            //else
            //{
            //    TempData["alertErro"] = "Ocorreu um erro ao editar Usuário!";
            //}
            return RedirectToAction("Editar", "Usuario");

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