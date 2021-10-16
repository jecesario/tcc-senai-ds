using backend.Models;
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
        public ActionResult Index()
        {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var tipoFormacao = TipoFormacao.listar();
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
                TempData["alertSucesso"] = "Tipo de formação cadastrado com sucesso!";
            }
            else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar tipo de formação!";
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
                TempData["alertSucesso"] = "Tipo de formação editado com sucesso!";
            }
            else {
                TempData["alertErro"] = "Ocorreu um erro ao editar tipo de formação!";
            }

            return RedirectToAction("Index");
        }
    }
}