using backend.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers {
    public class UsuarioController : Controller {
        // GET: Usuario
        public ActionResult Index(int pagina = 1) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuarios = Usuario.listar().OrderBy(p => p.Id).ToPagedList(pagina, backend.Models.Util.ITENS_POR_PAGINA);

            return View(usuarios);
        }

        public ActionResult Editar(int id) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Id == id || usuario.Tipo == 1) {
                var oUsuario = new Usuario();
                oUsuario.Id = id;
                ViewBag.Cursos = Curso.listar();
                return View(oUsuario.buscarPorId());

            }

            return RedirectToAction("Entrar", "Home");
        }
        [HttpPost]
        public ActionResult Editar() {
            // Pegando informações do form
            var usuario = new Usuario();
            usuario.Id = int.Parse(Request.Form["id"]);
            usuario.Nome = Request.Form["nome"];
            usuario.Email = Request.Form["email"];
            usuario.Turma = Request.Form["turma"];
            usuario.Ano = Request.Form["ano"];
            usuario.Tipo = (Request.Form["tipo"] == null) ? 0 : int.Parse(Request.Form["tipo"]);
            usuario.CursoId = Request.Form["curso"];
            var logado = (Usuario)Session["usuario"];
            var emailAntigo = usuario.buscarPorId().Email;

            // Verificando se é o próprio usuário se editando ou se é um admin
            if (usuario.buscarPorEmail() == null || usuario.buscarPorEmail().Email == emailAntigo) {
                if (usuario.editar()) {
                    if (!Request.Form["senha"].Equals("")) {
                        usuario.Senha = Request.Form["senha"];
                        usuario.atualizarSenha();
                    }
                    TempData["alertSucesso"] = "Sucesso!";
                    TempData["alertMensagem"] = "O usuário foi editado corretamente.";
                    if (logado.Tipo == 1) {
                        return RedirectToAction("Index", "Usuario");
                    } else {
                        Session["usuario"] = usuario;
                        return RedirectToAction("MeuCurriculo", "Curriculo");
                    }

                } else {
                    TempData["alertSucesso"] = "Erro!";
                    TempData["alertMensagem"] = "Ocorreu um erro ao ediar usuário.";
                }
            } else {
                TempData["alertSucesso"] = "Erro!";
                TempData["alertMensagem"] = "E-mail já cadastrado por outro usuário.";
            }

            return RedirectToAction("Editar/" + usuario.Id, "Usuario");

        }

        public ActionResult Apagar(int id) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var oUsuario = new Usuario();
            oUsuario.Id = id;
            if (oUsuario.apagar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Usuário foi apagado.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao apagar usuário!";
                TempData["alertMensagem"] = "Verifique os dados e tente novamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Buscar(string nome, int pagina = 1) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var oUsuario = new Usuario();
            var usuarioList = oUsuario.buscarPorNome(nome);
            if(usuarioList == null) {
                TempData["alertErro"] = "Ocorreu um erro!";
                TempData["alertMensagem"] = "Nenhum usuário foi encontrado.";
                return RedirectToAction("Index");
            }
            return View(usuarioList.OrderBy(p => p.Id).ToPagedList(pagina, backend.Models.Util.ITENS_POR_PAGINA));
        }
    }
}