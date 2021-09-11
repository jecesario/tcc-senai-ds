using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class HabilidadeController : Controller
    {
        public ActionResult Index() {
            if(Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if(usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var habilidades = Habilidade.listar();
            if (habilidades == null || habilidades.Count == 0) {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhuma habilidade cadastrada!";
            }
            return View(habilidades);
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
            var habilidade = new Habilidade();
            habilidade.Nome = Request.Form["nome"];
            if (habilidade.cadastrar()) {
                TempData["alertSucesso"] = "Habilidade cadastrada com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar habilidade!";
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
            var habilidade = new Habilidade();
            habilidade.Id = id;
            return View(habilidade.buscarPorId());
        }

        [HttpPost]
        public ActionResult EditarAction() {
            var nome = Request.Form["nome"];
            var id = int.Parse(Request.Form["id"]);

            var habilidade = new Habilidade();
            habilidade.Id = id;
            habilidade.Nome = nome;
            if (habilidade.editar()) {
                TempData["alertSucesso"] = "Habilidade editada com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao editar habilidade!";
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
            var habilidade = new Habilidade();
            habilidade.Id = id;
            if (habilidade.apagar()) {
                TempData["alertSucesso"] = "Habilidade apagada com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao deletar habilidade!";
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
            var habilidade = new Habilidade();
            if (habilidade.buscarPorNome(nome) == null)
            {
                TempData["alertErro"] = "Tá vindo vaizo " + nome;
            }
            return View(habilidade.buscarPorNome(nome));
        }
    }
}