using backend.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Index(int pagina = 1)
        {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var feedbacks = Feedback.listar().OrderBy(p => p.Id).ToPagedList(pagina, 12);
            if (feedbacks == null) {
                return View();
            }
            return View(feedbacks);
        }

        public ActionResult Cadastrar() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarAction() {
            var usuario = Session["usuario"] as Usuario;
            var feedback = new Feedback();
            feedback.Mensagem = Request.Form["mensagem"];
            feedback.usuarioId = usuario.Id;
            if (feedback.cadastrar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Seu feedback foi cadastrado.";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar feedback!";
                TempData["alertMensagem"] = "Verifique os dados e tente novamente.";
            }

            if(usuario.Tipo == 1) {
                return RedirectToAction("Index", "Feedback");
            }

            return RedirectToAction("MeuCurriculo", "Curriculo");
        }
    }
}