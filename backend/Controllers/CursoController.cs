using backend.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    
    public class CursoController : Controller
    {
        public ActionResult Index(int pagina = 1) {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var cursos = Curso.listar().OrderBy(p => p.Id).ToPagedList(pagina, 10);
            if (cursos == null) {
                return View();
            }
            return View(cursos);
        }

        public ActionResult Cadastrar() {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarAction() {
            var curso = new Curso();
            curso.Nome = Request.Form["nome"];
            if (curso.cadastrar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Curso " + curso.Nome + " foi cadastrado.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar curso!";
                TempData["alertMensagem"] = "Verifique os dados e tente novamente.";
            }

            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id) {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var curso = new Curso();
            curso.Id = id;

            return View(curso.buscarPorId());
        }

        [HttpPost]
        public ActionResult EditarAction() {
            var nome = Request.Form["nome"];
            var id = int.Parse(Request.Form["id"]);

            var curso = new Curso();
            curso.Id = id;
            curso.Nome = nome;
            if (curso.editar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Curso " + curso.Nome + " foi editado.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao editar curso!";
                TempData["alertMensagem"] = "Verifique os dados e tente novamente.";
            }

            return RedirectToAction("Index");
        }
        public ActionResult Apagar(int id) {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var curso = new Curso();
            curso.Id = id;
            if (curso.apagar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Curso foi apagado.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao apagar curso!";
                TempData["alertMensagem"] = "Verifique os dados e tente novamente.";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Buscar(string nome, int pagina = 1)
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
            var curso = new Curso();
            var resp = curso.buscarPorNome(nome);
            if (resp == null) {
                TempData["alertErro"] = "Erro!";
                TempData["alertMensagem"] = "Nenhuma informação foi encontrada.";
                return RedirectToAction("Index");
            }
            return View(resp.OrderBy(p => p.Id).ToPagedList(pagina, 10));
        }
    }
}