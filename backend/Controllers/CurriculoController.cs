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
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1)
            {
                return RedirectToAction("Entrar", "Home");
            }
            //var curriculos = Curriculo.listar();
            //if (curriculos == null || curriculos.Count == 0)
            //{
            //    TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum curso cadastrado!";
            //}
            return View();
        }

        public ActionResult MeuCurriculo() {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1)
            {
                return RedirectToAction("Index", "Curriculo");
            }
            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            return View(curriculo.buscarPorUsuarioId());
        }

        public ActionResult Cadastrar()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1)
            {
                return RedirectToAction("Index", "Curriculo");
            }
            var habilidades = Habilidade.listar();
            ViewBag.Habilidades = habilidades;
            //var habilidadeCurriculo = new HabilidadeCurriculo();
            //habilidadeCurriculo.CurriculoId = 3;
            //var resp = habilidadeCurriculo.cadastrar("1,2,3");
            //if(resp)
            //{
            //    TempData["alertInfo"] = "É pra dar certo!";
            //} else
            //{
            //    TempData["alertInfo"] = "Deu errado!";
            //}
            return View();
        }
        [HttpPost]
        public ActionResult CadastrarAction()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1)
            {
                return RedirectToAction("Index", "Curriculo");
            }
            var curriculo = new Curriculo();
            curriculo.Github = Request.Form["github"];
            curriculo.Linkedin = Request.Form["linkedin"];
            curriculo.Telefone = Request.Form["telefone"];
            curriculo.Resumo = Request.Form["resumo"];
            curriculo.Endereco = Request.Form["endereco"];
            curriculo.Cidade = Request.Form["cidade"];
            curriculo.Estado = Request.Form["estado"];
            curriculo.UsuarioId = Request.Form["id"];
            curriculo.cadastrar();
            var curriculo2 = new Curriculo();
            curriculo2.UsuarioId = usuario.Id+"";
            curriculo2.buscarPorUsuarioId();
            var checks = Request.Form["checks"];
            var habilidadeCurriculo = new HabilidadeCurriculo();
            habilidadeCurriculo.CurriculoId = curriculo2.Id;
            habilidadeCurriculo.cadastrar(checks);
            
            TempData["alertInfo"] = "Tá chegando..." + Request.Form;
            return RedirectToAction("Cadastrar", "Curriculo");
        }
    }
}