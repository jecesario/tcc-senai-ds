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
            return View();
        }

        [HttpPost]
        public ActionResult Entrar(string email, string senha)
        {
            var usuario = new Usuario();
            usuario.Email = email;
            usuario.Senha = senha;
            Console.WriteLine("HomeController: " + usuario.entrar());
            if(usuario.entrar() != null)
            {
                Console.WriteLine("HomeController: Id = " + usuario.entrar().Id);
                Console.WriteLine("HomeController: Nome = " + usuario.entrar().Nome);
                Console.WriteLine("HomeController: Email = " + usuario.entrar().Email);
                Console.WriteLine("HomeController: Senha = " + usuario.entrar().Senha);
                
                return RedirectToAction("Index", "Curso");
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
    }
}