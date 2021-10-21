using backend.Models;
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
        public ActionResult Index()
        {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var tipoVagas = VagaTipo.listar();
            if (tipoVagas == null || tipoVagas.Count == 0) {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum tipo de vaga cadastrado!";
            }
            return View(tipoVagas);
        }
    }
}