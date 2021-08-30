using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Entrar");
        }

        public ActionResult Entrar()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Entrar(string email, string senha)
        {
            var usuario = new Usuario();
            usuario.Email = email;
            usuario.Senha = senha;
            var usuarioLogado = usuario.entrar();
            if(usuarioLogado != null)
            {
                Session["usuario"] = usuarioLogado;
                
                return RedirectToAction("Index", "Curriculo");
            }
            return RedirectToAction("Entrar");
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(string nome, string email, string senha)
        {
            var usuario = new Usuario();
            usuario.Nome = nome;
            usuario.Email = email;
            usuario.Senha = senha;
            usuario.Tipo = 0;
            if (usuario.cadastrar())
            {
                return RedirectToAction("Entrar");
            }
            return RedirectToAction("Cadastrar");
        }

        public ActionResult Sair()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}