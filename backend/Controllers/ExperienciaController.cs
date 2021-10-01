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
            experiencia.Demissao = Request.Form["demissao"] == null ? experiencia.Admissao : Request.Form["demissao"];
            experiencia.CurriculoId = curriculo.Id;
            if (experiencia.cadastrar())
            {
                TempData["alertSucesso"] = "Experiencia cadastrada com sucesso!";
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrada Experiencia!";
            }
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }

        public ActionResult Editar(int id)
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
            var experiencia = new Experiencia();
            experiencia.Id = id;
            return View(experiencia.buscarPorId());
        }

        public ActionResult EditarAction()
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
            experiencia.Demissao = Request.Form["demissao"] == null ? experiencia.Admissao : Request.Form["demissao"];
            experiencia.CurriculoId = curriculo.Id;
            experiencia.Id = int.Parse(Request.Form["id"]);
            if(experiencia.editar())
            {
                TempData["alertSucesso"] = "Experiencia editada com sucesso!";
            } else
            {
                TempData["alertErro"] = "Ocorreu um erro ao editar Experiencia!";
            }
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }

        public ActionResult Apagar(int id)
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
            var experiencia = new Experiencia();
            experiencia.Id = id;
            if (experiencia.apagar())
            {
                TempData["alertSucesso"] = "Experiencia apagada com sucesso!";
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao deletar Experiencia!";
            }
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }
    }
}