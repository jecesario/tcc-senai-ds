using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class FormacaoController : Controller
    {
        // GET: Formacao
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
            return View(TipoFormacao.listar());
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

            var formacao = new Formacao();
            formacao.Nome = Request.Form["nome"];
            formacao.Instituicao = Request.Form["instituicao"];
            formacao.Inicio = Request.Form["inicio"];
            formacao.Conclusao = Request.Form["conclusao"];
            formacao.Resumo = Request.Form["resumo"];
            formacao.CurriculoId = curriculo.Id;
            formacao.TipoFormacaoId = int.Parse(Request.Form["tipoFormacao"]);
            if (formacao.cadastrar())
            {
                TempData["alertSucesso"] = "Formação cadastrada com sucesso!";
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar Formação!";
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
            var formacao = new Formacao();
            formacao.Id = id;
            ViewBag.Formacao = formacao.buscarPorId();
            return View(TipoFormacao.listar());
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

            var formacao = new Formacao();
            formacao.Nome = Request.Form["nome"];
            formacao.Instituicao = Request.Form["instituicao"];
            formacao.Inicio = Request.Form["inicio"];
            formacao.Conclusao = Request.Form["conclusao"];
            formacao.Resumo = Request.Form["resumo"];
            formacao.CurriculoId = curriculo.Id;
            formacao.TipoFormacaoId = int.Parse(Request.Form["tipoFormacao"]);
            formacao.Id = int.Parse(Request.Form["id"]);
            if (formacao.editar())
            {
                TempData["alertSucesso"] = "Formação editada com sucesso!";
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao editar Formação!";
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
            var formacao = new Formacao();
            formacao.Id = id;
            if (formacao.apagar())
            {
                TempData["alertSucesso"] = "Formação apagada com sucesso!";
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao deletar Formação!";
            }
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }
    }
}