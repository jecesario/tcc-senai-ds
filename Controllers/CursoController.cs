using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class CursoController : Controller
    {
        public ActionResult Index() {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var cursos = Curso.listar();
            if (cursos == null || cursos.Count == 0) {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum curso cadastrado!";
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
        public ActionResult Cadastrar(string nome) {
            var curso = new Curso();
            curso.Nome = nome;
            if (curso.cadastrar()) {
                TempData["alertSucesso"] = "Curso cadastrado com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar curso!";
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
        public ActionResult Editar() {
            var nome = Request.Form["nome"];
            var id = int.Parse(Request.Form["id"]);

            var curso = new Curso();
            curso.Id = id;
            curso.Nome = nome;
            if (curso.editar()) {
                TempData["alertSucesso"] = "Curso editado com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao editar curso!";
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
                TempData["alertSucesso"] = "Curso apagado com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao deletar curso!";
            }
            return RedirectToAction("Index");
        }
    }
}