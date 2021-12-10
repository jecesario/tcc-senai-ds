﻿using backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace backend.Controllers {
    public class CurriculoController : Controller {
        // GET: Curriculo
        public ActionResult Index(int pagina = 1) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var curriculos = Curriculo.listar();
            if (curriculos == null) {
                return View();
            }

            return View(curriculos.OrderBy(p => p.Id).ToPagedList(pagina, backend.Models.Util.ITENS_POR_PAGINA));
        }

        public ActionResult MeuCurriculo() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
                return RedirectToAction("Index", "Curriculo");
            }
            // Guardando dados do curriculo do usuário logado 
            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            curriculo = curriculo.buscarPorUsuarioId();
            ViewBag.Curriculo = curriculo;


            // Se o usuário ainda não tiver cadastrado um currículo, redireciona para a tela que pede para ele cadastrar um currículo
            if (curriculo == null) {
                return View();
            }

            ViewBag.UltimaEdicao = Util.dateAgo(DateTime.Parse(curriculo.DataEdicao));

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
            if (listaHabilidades != null) {
                foreach (var i in listaHabilidades) {
                    var habilidade = new Habilidade();
                    habilidade.Id = i.HabilidadeId;
                    habilidades.Add(habilidade.buscarPorId());
                }
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

        public ActionResult Cadastrar() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
                return RedirectToAction("Index", "Curriculo");
            }
            var habilidades = Habilidade.listar();
            ViewBag.Habilidades = habilidades;
            return View();
        }
        [HttpPost]
        public ActionResult CadastrarAction() {
            // Vericação de usuário logado
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
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
            curriculo.UsuarioId = Request.Form["usuarioId"];
            if (curriculo.cadastrar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Curriculo foi cadastrado.";
            } else {
                TempData["alertErro"] = "Erro!";
                TempData["alertMensagem"] = "Ocorreu um erro ao cadastrar currículo.";
            }
            // Pegando as habilidades marcadas e cadastrando na tabela de habilidades relacionadas com o curriculo do usuário logado
            var checks = Request.Form["checks"];
            var habilidadeCurriculo = new HabilidadeCurriculo();
            habilidadeCurriculo.CurriculoId = curriculo.buscarPorUsuarioId().Id;
            habilidadeCurriculo.cadastrar(checks);

            curriculo.atualizarDataEdicao();

            return RedirectToAction("MeuCurriculo", "Curriculo");
        }

        public ActionResult Editar(int id) {
            // Vericação de usuário logado
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
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
            if (listaHabilidades != null) {
                foreach (var i in listaHabilidades) {
                    var habilidade = new Habilidade();
                    habilidade.Id = i.HabilidadeId;
                    habilidadesMarcadas.Add(habilidade.buscarPorId().Id.ToString());
                }
            }
            ViewBag.Marcadas = habilidadesMarcadas;
            return View(Habilidade.listar());
        }

        [HttpPost]
        public ActionResult EditarAction() {
            // Vericação de usuário logado
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
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


            if (curriculo.editar()) {
                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Curriculo foi editado.";
            } else {
                TempData["alertErro"] = "Erro!";
                TempData["alertMensagem"] = "Ocorreu um erro ao editar currículo.";
            }

            // Pegando as habilidades marcadas e cadastrando na tabela de habilidades relacionadas com o curriculo do usuário logado
            var checks = Request.Form["checks"];
            var habilidadeCurriculo = new HabilidadeCurriculo();
            habilidadeCurriculo.CurriculoId = curriculo.buscarPorUsuarioId().Id;
            habilidadeCurriculo.editar(checks);

            curriculo.atualizarDataEdicao();

            return RedirectToAction("MeuCurriculo", "Curriculo");
        }
        public ActionResult Buscar(string habilidades, int pagina = 1) {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo != 1) {
                return RedirectToAction("Entrar", "Home");
            }
            var curriculo = new Curriculo();
            var resp = curriculo.buscarPorHabilidades(habilidades);

            if(habilidades.Equals("")) {
                return RedirectToAction("Index", "Curriculo");
            }

            if(resp == null) {
                TempData["alertErro"] = "Erro!";
                TempData["alertMensagem"] = "Nenhuma informação foi encontrada.";
                return RedirectToAction("Index", "Curriculo");
            }
            return View(resp.OrderBy(p => p.Id).ToPagedList(pagina, backend.Models.Util.ITENS_POR_PAGINA));
        }

        public ActionResult AnexarDoc() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
                return RedirectToAction("Index", "Curriculo");
            }

            // Guardando dados do curriculo do usuário logado 
            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            curriculo = curriculo.buscarPorUsuarioId();

            HttpPostedFileBase arquivo = Request.Files[0];

            var nomeArquivo = usuario.Email + Util.criptografar(DateTime.Now.Millisecond.ToString());

            if (!Directory.Exists(Server.MapPath("~/Content/Uploads"))) {
                Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
            }

            if (arquivo.ContentLength > 0) {
                var uploadPath = Server.MapPath("~/Content/Uploads");
                string caminhoArquivo = Path.Combine(@uploadPath,
                    Path.GetFileName(nomeArquivo));
                caminhoArquivo += Path.GetExtension(arquivo.FileName);
                nomeArquivo += Path.GetExtension(arquivo.FileName);
                arquivo.SaveAs(caminhoArquivo);

                // Alterando o valor de "anexo" na tabela currículo
                curriculo.Anexo = nomeArquivo;
                curriculo.anexarDoc();

                TempData["alertSucesso"] = "Sucesso!";
                TempData["alertMensagem"] = "Anexo salvo no servidor.";
            }
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }

        public ActionResult ExcluirDoc() {
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 1) {
                return RedirectToAction("Index", "Curriculo");
            }

            // Guardando dados do curriculo do usuário logado 
            var curriculo = new Curriculo();
            curriculo.UsuarioId = usuario.Id.ToString();
            curriculo = curriculo.buscarPorUsuarioId();

            var arquivo = Server.MapPath("~/Content/Uploads/") + curriculo.Anexo;
            var existe = System.IO.File.Exists(arquivo);
            if (existe) {
                try {
                    System.IO.File.Delete(arquivo);
                    curriculo.deletarDoc();
                    TempData["alertSucesso"] = "Sucesso!";
                    TempData["alertMensagem"] = "Anexo removido do servidor.";
                } catch (System.IO.IOException e) {
                    TempData["alertErro"] = "Erro!";
                    TempData["alertMensagem"] = "Ocorreu um erro ao deletar anexo.";
                }
            }
            return RedirectToAction("MeuCurriculo", "Curriculo");
        }

        public ActionResult Detalhar(int id) {
            // Vericação de usuário logado
            if (Session["usuario"] == null) {
                return RedirectToAction("Entrar", "Home");
            }
            var usuario = Session["usuario"] as Usuario;
            if (usuario.Tipo == 0) {
                return RedirectToAction("MeuCurriculo", "Curriculo");
            }
            // Guardando dados do curriculo do usuário logado 
            var curri = new Curriculo();
            curri.Id = id;
            var curriculo = curri.buscarPorId();
            ViewBag.Curriculo = curriculo;


            // Se o usuário ainda não tiver cadastrado um currículo, redireciona para tela de listagem
            if (curriculo == null) {
                TempData["alertErro"] = "Erro!";
                TempData["alertMensagem"] = "Usuário não possui currículo cadastrado.";
                return RedirectToAction("Index", "Curriculo");
            }

            ViewBag.UltimaEdicao = Util.dateAgo(DateTime.Parse(curriculo.DataEdicao));

            // Guardando curso do usuário
            var curso = new Curso();
            curso.Id = curriculo.Usuario.Id;
            ViewBag.Curso = curso.buscarPorId();

            // Guardado habilidades do usuário logado
            var habilidadeCurriculo = new HabilidadeCurriculo();
            habilidadeCurriculo.CurriculoId = curriculo.Id;
            var listaHabilidades = habilidadeCurriculo.buscarPorCurriculoId();

            // Pegando cada ID de habilidade encontrado na tabela, buscando informação na tabela de Habilidades e guardando em uma lista para enviar para a View
            var habilidades = new List<Habilidade>();
            if (listaHabilidades != null) {
                foreach (var i in listaHabilidades) {
                    var habilidade = new Habilidade();
                    habilidade.Id = i.HabilidadeId;
                    habilidades.Add(habilidade.buscarPorId());
                }
            }

            // Guardando as experiencias do usuário baseadas pelo id do curriculo
            var experiencia = new Experiencia();
            experiencia.CurriculoId = curriculo.Id;
            ViewBag.Experiencias = experiencia.buscarPorCurriculoId();

            // Guardando as formações do usuário baseadas pelo id do curriculo
            var formacao = new Formacao();
            formacao.CurriculoId = curriculo.Id;
            ViewBag.Formacoes = formacao.buscarPorCurriculoId();


            ViewBag.Usuario = curriculo.Usuario;

            return View(habilidades);
        }
    }
}