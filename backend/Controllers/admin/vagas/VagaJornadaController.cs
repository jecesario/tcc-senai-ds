﻿using backend.Models;
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
        public ActionResult Index() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var vagaJornadas = VagaJornada.listar();
            if (vagaJornadas == null || vagaJornadas.Count == 0) {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum tipo de vaga cadastrado!";
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
                TempData["alertSucesso"] = "Tipo de Vaga cadastrada com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar Tipo de Vaga!";
            }

            return RedirectToAction("Index");
        }
    }
}