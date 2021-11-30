using backend.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class TipoFormacaoController : Controller
    {
        // GET: TipoFormacao
        public ActionResult Index(int pagina = 1)
        {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var tipoFormacao = TipoFormacao.listar().OrderBy(p => p.Id).ToPagedList(pagina, 2);
            if (tipoFormacao == null || tipoFormacao.Count == 0) {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum tipo de formação cadastrado!";
            }
            return View(tipoFormacao);
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
            var tipoFormacao = new TipoFormacao();
            tipoFormacao.Nome = Request.Form["nome"];
            if (tipoFormacao.cadastrar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Tipo de Formação " + tipoFormacao.Nome + " foi cadastrada.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar tipo de formação!";
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
            var tipoFormacao = new TipoFormacao();
            tipoFormacao.Id = id;

            return View(tipoFormacao.buscarPorId());
        }

        public ActionResult EditarAction() {
            var nome = Request.Form["nome"];
            var id = int.Parse(Request.Form["id"]);

            var tipoFormacao = new TipoFormacao();
            tipoFormacao.Id = id;
            tipoFormacao.Nome = nome;
            if (tipoFormacao.editar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Tipo de Formação " + tipoFormacao.Nome + " foi editada.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao editar tipo de formação!";
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
            var tipoFormacao = new TipoFormacao();
            tipoFormacao.Id = id;
            if (tipoFormacao.apagar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Tipo de Formação foi apagada.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao apagar tipo de formação!";
                TempData["alertMensagem"] = "Verifique os dados e tente novamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Buscar(string nome, int pagina = 1) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var tipoFormacao = new TipoFormacao();
            return View(tipoFormacao.buscarPorNome(nome).OrderBy(p => p.Id).ToPagedList(pagina, 2));
        }
    }
}