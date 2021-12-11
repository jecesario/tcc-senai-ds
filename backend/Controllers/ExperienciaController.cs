using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers {
    public class ExperienciaController : Controller {
        // GET: Experiencia
        public ActionResult Cadastrar() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
                return RedirectToAction("Entrar", "Home");
            }

            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            var experiencia = new Experiencia();
            experiencia.CurriculoId = curriculo.buscarPorUsuarioId().Id;

            if (experiencia.buscarPorCurriculoId() != null) {
                if (experiencia.buscarPorCurriculoId().Count >= 3) {
                    TempData["alertErro"] = "Ocorreu um erro!";
                    TempData["alertMensagem"] = "Você só pode cadastrar 3 experiências.";
                    return RedirectToAction("MeuCurriculo", "Curriculo");
                }
            } 
            return View();
        }

        public ActionResult CadastrarAction() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
                return RedirectToAction("Entrar", "Home");
            }

            // Guardando dados do curriculo do usuário logado 
            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            curriculo = curriculo.buscarPorUsuarioId();

            var experiencia = new Experiencia();
            experiencia.Cargo = Request.Form["cargo"];
            experiencia.Empregador = Request.Form["empregador"];
            experiencia.Resumo = Request.Form["resumo"];
            experiencia.Admissao = Request.Form["admissao"];
            experiencia.Demissao = Request.Form["demissao"] == null ? experiencia.Admissao : Request.Form["demissao"];
            experiencia.CurriculoId = curriculo.Id;
            if (experiencia.cadastrar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Experiencia foi cadastrada.";
                curriculo.atualizarDataEdicao();
            } else {
                TempData["alertErro"] = "Erro!";
                TempData["alertMensagem"] = "Ocorreu um erro ao cadastrar experiência.";
            }
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }

        public ActionResult Editar(int id) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var experiencia = new Experiencia();
            experiencia.Id = id;
            return View(experiencia.buscarPorId());
        }

        public ActionResult EditarAction() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
                return RedirectToAction("Entrar", "Home");
            }

            // Guardando dados do curriculo do usuário logado 
            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            curriculo = curriculo.buscarPorUsuarioId();

            var experiencia = new Experiencia();
            experiencia.Cargo = Request.Form["cargo"];
            experiencia.Empregador = Request.Form["empregador"];
            experiencia.Resumo = Request.Form["resumo"];
            experiencia.Admissao = Request.Form["admissao"];
            experiencia.Demissao = Request.Form["demissao"] == null ? experiencia.Admissao : Request.Form["demissao"];
            experiencia.CurriculoId = curriculo.Id;
            experiencia.Id = int.Parse(Request.Form["id"]);
            if (experiencia.editar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Experiencia foi editada.";
                curriculo.atualizarDataEdicao();
            } else {
                TempData["alertErro"] = "Erro!";
                TempData["alertMensagem"] = "Ocorreu um erro ao editar experiência.";
            }
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }

        public ActionResult Apagar(int id) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
                return RedirectToAction("Entrar", "Home");
            }

            // Guardando dados do curriculo do usuário logado 
            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            curriculo = curriculo.buscarPorUsuarioId();

            var experiencia = new Experiencia();
            experiencia.Id = id;
            if (experiencia.apagar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Experiencia foi apagada.";
                curriculo.atualizarDataEdicao();
            } else {
                TempData["alertErro"] = "Erro!";
                TempData["alertMensagem"] = "Ocorreu um erro ao apagar experiência.";
            }
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }
    }
}