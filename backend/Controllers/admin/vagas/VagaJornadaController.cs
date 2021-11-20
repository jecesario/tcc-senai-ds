using backend.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers.admin.vagas
{
    public class VagaJornadaController : Controller
    {
        // GET: VagaJornada
        public ActionResult Index(int pagina = 1) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var vagaJornadas = VagaJornada.listar().OrderBy(p => p.Id).ToPagedList(pagina, 2);
            if (vagaJornadas == null || vagaJornadas.Count == 0) {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum tipo de Jornada cadastrado!";
            }
            return View(vagaJornadas);
        }

        public ActionResult Cadastrar() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarAction() {
            var vagaJornada = new VagaJornada();
            vagaJornada.Descricao = Request.Form["descricao"];
            if (vagaJornada.cadastrar()) {
                TempData["alertSucesso"] = "Tipo de Jornada cadastrada com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar Tipo de Jornada!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var vagaJornada = new VagaJornada();
            vagaJornada.Id = id;

            return View(vagaJornada.buscarPorId());
        }

        [HttpPost]
        public ActionResult EditarAction() {
            var descricao = Request.Form["descricao"];
            var id = int.Parse(Request.Form["id"]);

            var vagaJornada = new VagaJornada();
            vagaJornada.Id = id;
            vagaJornada.Descricao = descricao;
            if (vagaJornada.editar()) {
                TempData["alertSucesso"] = "Tipo de Jornada editada com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao editar Tipo de Jornada!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var vagaJornada = new VagaJornada();
            vagaJornada.Id = id;
            if (vagaJornada.apagar()) {
                TempData["alertSucesso"] = "Tipo de Jornada apagado com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao deletar Tipo de Jornada!";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Buscar(string descricao, int pagina = 1) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var vagaJornada = new VagaJornada();
            return View(vagaJornada.buscarPorDescricao(descricao).OrderBy(p => p.Id).ToPagedList(pagina, 2));
        }
    }
}