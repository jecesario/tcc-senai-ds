using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return RedirectToAction("Entrar");
        }

        public ActionResult Entrar() {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult EntrarAction() {
            var usuario = new Usuario();
            usuario.Email = Request.Form["email"];
            usuario.Senha = Request.Form["senha"];
            var usuarioLogado = usuario.entrar();
            if (usuarioLogado != null) {
                Session["usuario"] = usuarioLogado;

                return RedirectToAction("MeuCurriculo", "Curriculo");
            }
            TempData["alertErro"] = "Usuário e/ou Senha inváidos";
            return RedirectToAction("Entrar");
        }

        public ActionResult Cadastrar() {
            return View(Curso.listar());
        }

        [HttpPost]
        public ActionResult CadastrarAction() {
            var usuario = new Usuario();
            usuario.Nome = Request.Form["nome"];
            usuario.Email = Request.Form["email"];
            usuario.Senha = Request.Form["senha"];
            usuario.Turma = Request.Form["turma"];
            usuario.Ano = Request.Form["ano"];
            usuario.Tipo = 0;
            usuario.CursoId = Request.Form["curso"];
            if (usuario.cadastrar()) {
                TempData["alertSucesso"] = "Usuário cadastrado. Efetue login abaixo";
                return RedirectToAction("Entrar");
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar usuário!";
            }
            return RedirectToAction("Cadastrar");
        }

        public ActionResult Sair() {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}