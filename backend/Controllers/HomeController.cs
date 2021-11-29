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
            TempData["alertErro"] = "Ocorreu um erro ao efetuar login!";
            TempData["alertMensagem"] = "Usuário e/ou Senha inválidos.";
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

            if(usuario.buscarPorEmail() != null) {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar usuário!";
                TempData["alertMensagem"] = "O e-mail informado já pertece a outro usuário.";
                return RedirectToAction("Cadastrar");
            }

            if (usuario.cadastrar()) {
                TempData["alertSucesso"] = "Usuário cadastrado com sucesso!";
                TempData["alertMensagem"] = "Preencha os dados de login para acessar o sistema.";
                return RedirectToAction("Entrar");
            }
            TempData["alertErro"] = "Ocorreu um erro ao cadastrar usuário!";
            TempData["alertMensagem"] = "Verifique seus dados e preencha novamente.";
            return RedirectToAction("Cadastrar");
        }

        public ActionResult Sair() {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}