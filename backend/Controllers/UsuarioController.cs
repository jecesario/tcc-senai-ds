using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
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
            var usuarios = Usuario.listar();
            if (usuarios == null || usuarios.Count == 0)
            {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum curso cadastrado!";
            }
            return View(usuarios);
        }

        public ActionResult Editar(int id)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Id == id || usuario.Tipo == 1)
            {
                var oUsuario = new Usuario();
                oUsuario.Id = id;
                List<SelectListItem> tipos = new List<SelectListItem>();
                tipos.Add(new SelectListItem { Text = "Usuário", Value = "0" });
                tipos.Add(new SelectListItem { Text = "Admin", Value = "1" });
                ViewBag.Tipos = tipos;
                return View(oUsuario.buscarPorId()); 
                
            }
            
            return RedirectToAction("Entrar", "Home");
        }
        [HttpPost]
        public ActionResult Editar()
        {
            
            var id = int.Parse(Request.Form["id"]);
            var nome = Request.Form["nome"];
            var email = Request.Form["email"];
            var senha = Request.Form["senha"];
            var tipo = int.Parse(Request.Form["tipo"]);
            var usuario = new Usuario();
            usuario.Id = id;
            usuario.Nome = nome;
            usuario.Email = email;
            usuario.Senha = senha;
            usuario.Tipo = tipo;
            if (usuario.editar())
            {
                var logado = (Usuario)Session["usuario"];
                if(logado.Tipo == 1)
                {
                    TempData["alertSucesso"] = "Usuário editado com sucesso!";
                    if (logado.Id == usuario.Id)
                    {
                        return RedirectToAction("Sair", "Home");
                    }
                }
                else 
                {
                    return RedirectToAction("Sair", "Home");
                }
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao editar Usuário!";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Apagar(int id)
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
            var oUsuario = new Usuario();
            oUsuario.Id = id;
            if (oUsuario.apagar())
            {
                TempData["alertSucesso"] = "Usuário apagado com sucesso!";
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao deletar Usuário!";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Buscar(string nome)
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
            var oUsuario = new Usuario();
            return View(oUsuario.buscarPorNome(nome));
        }
    }
}