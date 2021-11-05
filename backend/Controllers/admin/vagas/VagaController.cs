using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backend.Controllers.admin.vagas
{
    public class VagaController : Controller
    {
        // GET: Vaga
        public ActionResult Index()
        {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }

            var vagas = Vaga.listar();

            return View(vagas);
        }

        public ActionResult Cadastrar() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }

            // Listando jornadas de trabalho
            var jornadas = VagaJornada.listar();
            ViewBag.Jornadas = jornadas;
            // Listando tipos de contratação
            var tiposContratacao = VagaTipo.listar();
            ViewBag.TiposContratacao = tiposContratacao;

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarAction() {
            var usuario = Session["usuario"] as Usuario;
            var vaga = new Vaga();
            vaga.Cargo = Request.Form.Get("cargo");
            vaga.Quantidade = int.Parse(Request.Form.Get("quantidade"));
            vaga.Cidade = Request.Form.Get("cidade");
            vaga.Estado = Request.Form.Get("estado");
            vaga.Empresa = Request.Form.Get("empresa");
            vaga.Requisitos = Request.Form.Get("requisitos");
            vaga.Atribuicoes = Request.Form.Get("atribuicoes");
            vaga.Remuneracao = double.Parse(Request.Form.Get("remuneracao"));
            vaga.Beneficios = Request.Form.Get("beneficios");
            vaga.DataPostagem = DateTime.Now.Date.ToString();
            vaga.DataLimite = Request.Form.Get("dataLimite");
            vaga.Observacoes = Request.Form.Get("observacoes");
            vaga.VagaJornadaId = int.Parse(Request.Form.Get("jornada"));
            vaga.VagaTipoId = int.Parse(Request.Form.Get("tipo"));
            vaga.UsuarioId = usuario.Id;

            if (vaga.cadastrar()) {
                TempData["alertSucesso"] = "Vaga cadastrada com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar vaga!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id) {
            // Vericação de usuário logado
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }

            // Listando jornadas de trabalho
            var jornadas = VagaJornada.listar();
            ViewBag.Jornadas = jornadas;
            // Listando tipos de contratação
            var tiposContratacao = VagaTipo.listar();
            ViewBag.TiposContratacao = tiposContratacao;

            var vaga = new Vaga();
            vaga.Id = id;

            return View(vaga.buscarPorId());
        }

        [HttpPost]
        public ActionResult EditarAction() {
            // Vericação de usuário logado
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            // Listando jornadas de trabalho
            var jornadas = VagaJornada.listar();
            ViewBag.Jornadas = jornadas;
            // Listando tipos de contratação
            var tiposContratacao = VagaTipo.listar();
            ViewBag.TiposContratacao = tiposContratacao;

            var vaga = new Vaga();
            vaga.Id = int.Parse(Request.Form.Get("id"));
            vaga.Cargo = Request.Form.Get("cargo");
            vaga.Quantidade = int.Parse(Request.Form.Get("quantidade"));
            vaga.Cidade = Request.Form.Get("cidade");
            vaga.Estado = Request.Form.Get("estado");
            vaga.Empresa = Request.Form.Get("empresa");
            vaga.Requisitos = Request.Form.Get("requisitos");
            vaga.Atribuicoes = Request.Form.Get("atribuicoes");
            vaga.Remuneracao = double.Parse(Request.Form.Get("remuneracao"));
            vaga.Beneficios = Request.Form.Get("beneficios");
            vaga.DataPostagem = DateTime.Now.Date.ToString();
            vaga.DataLimite = Request.Form.Get("dataLimite");
            vaga.Observacoes = Request.Form.Get("observacoes");
            vaga.VagaJornadaId = int.Parse(Request.Form.Get("jornada"));
            vaga.VagaTipoId = int.Parse(Request.Form.Get("tipo"));
            vaga.UsuarioId = usuario.Id;

            if (vaga.editar()) {
                TempData["alertSucesso"] = "Vaga editada com sucesso!";
            } else {
                TempData["alertErro"] = "Ocorreu um erro ao editar vaga!";
            }

            return RedirectToAction("Index");
        }
    }
}