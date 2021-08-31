using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class CurriculoController : Controller
    {
        // GET: Curriculo
        public ActionResult Index()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var curriculos = Curriculo.listar();
            if (curriculos == null || curriculos.Count == 0)
            {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum curso cadastrado!";
            }
            return View(curriculos);
        }
    }
}