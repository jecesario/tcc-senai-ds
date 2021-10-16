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
    }
}