using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class ExperienciaController : Controller
    {
        // GET: Experiencia
        public ActionResult Cadastrar()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            return View();
        }

        public ActionResult CadastrarAction()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1)
            {
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
            experiencia.Demissao = Request.Form["demissao"];
            experiencia.CurriculoId = curriculo.Id;
            experiencia.cadastrar();
            TempData["alertSucesso"] = "Experiencia adicionada com sucesso!";
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }
    }
}