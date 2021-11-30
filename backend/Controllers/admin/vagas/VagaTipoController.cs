using backend.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers.admin.vagas
{
    public class VagaTipoController : Controller
    {
        // GET: VagaTipo
        public ActionResult Index(int pagina = 1)
        {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var tipoVagas = VagaTipo.listar().OrderBy(p => p.Id).ToPagedList(pagina, 2);
            if (tipoVagas == null || tipoVagas.Count == 0) {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum tipo de vaga cadastrado!";
            }
            return View(tipoVagas);
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
            var vagaTipo = new VagaTipo();
            vagaTipo.Descricao = Request.Form["descricao"];
            if (vagaTipo.cadastrar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Tipo de Jornada " + vagaTipo.Descricao + " foi cadastrada.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar tipo de jornada!";
                TempData["alertMensagem"] = "Verifique os dados e tente novamente.";
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
            var vagaTipo = new VagaTipo();
            vagaTipo.Id = id;

            return View(vagaTipo.buscarPorId());
        }

        [HttpPost]
        public ActionResult EditarAction() {
            var descricao = Request.Form["descricao"];
            var id = int.Parse(Request.Form["id"]);

            var vagaTipo = new VagaTipo();
            vagaTipo.Id = id;
            vagaTipo.Descricao = descricao;
            if (vagaTipo.editar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Tipo de Jornada " + vagaTipo.Descricao + " foi editada.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao editar tipo de jornada!";
                TempData["alertMensagem"] = "Verifique os dados e tente novamente.";
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
            var vagaTipo = new VagaTipo();
            vagaTipo.Id = id;
            if (vagaTipo.apagar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Tipo de Jornada foi apagada.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao apagar tipo de jornada!";
                TempData["alertMensagem"] = "Verifique os dados e tente novamente.";
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
            var vagaTipo = new VagaTipo();
            return View(vagaTipo.buscarPorDescricao(descricao).OrderBy(p => p.Id).ToPagedList(pagina, 2));
        }
    }
}