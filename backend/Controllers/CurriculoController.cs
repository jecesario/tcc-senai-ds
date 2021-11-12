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
            var curriculos = Curriculo.listar();
            if (curriculos == null || curriculos.Count == 0)
            {
                TempData["alertInfo"] = "Epa, perai! Parece que não tem nenhum curso cadastrado!";
            }
            return View(curriculos);
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
            // Guardando dados do curriculo do usuário logado 
            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            curriculo = curriculo.buscarPorUsuarioId();
            ViewBag.Curriculo = curriculo;

            ViewBag.UltimaEdicao = Util.dateAgo(DateTime.Parse(curriculo.DataEdicao));

            // Se o usuário ainda não tiver cadastrado um currículo, redireciona para a tela que pede para ele cadastrar um currículo
            if (curriculo == null)
            {
                return View();
            }

            // Guardando curso do usuário
            var curso = new Curso();
            curso.Id = int.Parse(usuario.CursoId);
            ViewBag.Curso = curso.buscarPorId();

            // Guardado habilidades do usuário logado
            var habilidadeCurriculo = new HabilidadeCurriculo();
            habilidadeCurriculo.CurriculoId = curriculo.Id;
            var listaHabilidades = habilidadeCurriculo.buscarPorCurriculoId();

            // Pegando cada ID de habilidade encontrado na tabela, buscando informação na tabela de Habilidades e guardando em uma lista para enviar para a View
            var habilidades = new List<Habilidade>();
            foreach(var i in listaHabilidades)
            {
                var habilidade = new Habilidade();
                habilidade.Id = i.HabilidadeId;
                habilidades.Add(habilidade.buscarPorId());
            }

            // Guardando as experiencias do usuário baseadas pelo id do curriculo
            var experiencia = new Experiencia();
            experiencia.CurriculoId = curriculo.Id;
            ViewBag.Experiencias = experiencia.buscarPorCurriculoId();

            // Guardando as formações do usuário baseadas pelo id do curriculo
            var formacao = new Formacao();
            formacao.CurriculoId = curriculo.Id;
            ViewBag.Formacoes = formacao.buscarPorCurriculoId();


            return View(habilidades);
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
            // Vericação de usuário logado
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1)
            {
                return RedirectToAction("Index", "Curriculo");
            }

            // Preenchendo os dados e cadastrando curriculo
            var curriculo = new Curriculo();
            curriculo.Github = Request.Form["github"];
            curriculo.Linkedin = Request.Form["linkedin"];
            curriculo.Telefone = Request.Form["telefone"];
            curriculo.Resumo = Request.Form["resumo"];
            curriculo.Endereco = Request.Form["endereco"];
            curriculo.Cidade = Request.Form["cidade"];
            curriculo.Estado = Request.Form["estado"];
            curriculo.UsuarioId = Request.Form["id"];
            if (curriculo.cadastrar())
            {
                TempData["alertSucesso"] = "Curriculo cadastrado com sucesso!";
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao cadastrar Curriculo!";
            }
            // Pegando as habilidades marcadas e cadastrando na tabela de habilidades relacionadas com o curriculo do usuário logado
            var checks = Request.Form["checks"];
            var habilidadeCurriculo = new HabilidadeCurriculo();
            habilidadeCurriculo.CurriculoId = curriculo.buscarPorUsuarioId().Id;
            habilidadeCurriculo.cadastrar(checks);

            curriculo.atualizarDataEdicao();
            
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }

        public ActionResult Editar(int id)
        {
            // Vericação de usuário logado
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1)
            {
                return RedirectToAction("Index", "Curriculo");
            }

            // Guardando dados do curriculo do usuário logado 
            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            curriculo = curriculo.buscarPorUsuarioId();
            ViewBag.Curriculo = curriculo;

            // Guardando curso do usuário
            var curso = new Curso();
            curso.Id = int.Parse(usuario.CursoId);
            ViewBag.Curso = curso.buscarPorId();

            // Guardado habilidades do usuário logado
            var habilidadeCurriculo = new HabilidadeCurriculo();
            habilidadeCurriculo.CurriculoId = curriculo.Id;
            var listaHabilidades = habilidadeCurriculo.buscarPorCurriculoId();

            // Pegando cada ID de habilidade encontrado na tabela, buscando informação na tabela de Habilidades e guardando em uma lista para enviar para a View
            var habilidadesMarcadas = new List<string>();
            foreach (var i in listaHabilidades)
            {
                var habilidade = new Habilidade();
                habilidade.Id = i.HabilidadeId;
                habilidadesMarcadas.Add(habilidade.buscarPorId().Id.ToString());
            }
            ViewBag.Marcadas = habilidadesMarcadas;
            return View(Habilidade.listar());
        }

        [HttpPost]
        public ActionResult EditarAction()
        {
            // Vericação de usuário logado
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1)
            {
                return RedirectToAction("Index", "Curriculo");
            }

            // Preenchendo os dados e cadastrando curriculo
            var curriculo = new Curriculo();
            curriculo.Id = int.Parse(Request.Form["id"]);
            curriculo.Github = Request.Form["github"];
            curriculo.Linkedin = Request.Form["linkedin"];
            curriculo.Telefone = Request.Form["telefone"];
            curriculo.Resumo = Request.Form["resumo"];
            curriculo.Endereco = Request.Form["endereco"];
            curriculo.Cidade = Request.Form["cidade"];
            curriculo.Estado = Request.Form["estado"];
            curriculo.UsuarioId = Request.Form["usuarioId"];
            

            if (curriculo.editar())
            {
                TempData["alertSucesso"] = "Curriculo editado com sucesso!";
            }
            else
            {
                TempData["alertErro"] = "Ocorreu um erro ao editar Curriculo!";
            }

            // Pegando as habilidades marcadas e cadastrando na tabela de habilidades relacionadas com o curriculo do usuário logado
            var checks = Request.Form["checks"];
            var habilidadeCurriculo = new HabilidadeCurriculo();
            habilidadeCurriculo.CurriculoId = curriculo.buscarPorUsuarioId().Id;
            habilidadeCurriculo.editar(checks);

            curriculo.atualizarDataEdicao();

            return RedirectToAction("MeuCurriculo", "Curriculo");
        }
    }
}