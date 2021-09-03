using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace backend.Controllers.api
{
    public class UsuarioController : ApiController
    {
        public List<Usuario> Get()
        {
            return Usuario.listar();
        }

        public HttpResponseMessage Post([FromBody] Usuario usuario)
        {
            if (usuario.buscarPorEmail() == null)
            {
                if (usuario.cadastrar())
                {
                    return Request.CreateResponse(HttpStatusCode.Created);
                } else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "E-mail " + usuario.Email + " já cadastrado!");
        }

        public HttpResponseMessage Delete([FromUri] int id)
        {
            var usuario = new Usuario();
            usuario.Id = id;
            if (usuario.apagar())
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Usuário " + id + " não encontrado");
        }

        public HttpResponseMessage Put([FromBody] Usuario usuario)
        {
            var newUsuario = usuario.buscarPorId();

            if (newUsuario == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Usuário " + usuario.Id + " não encontrado");
            }

            if (newUsuario.buscarPorEmail().Email == usuario.Email || usuario.buscarPorEmail() == null)
            {
                if (usuario.editar())
                {
                    newUsuario = usuario.buscarPorId();
                    return Request.CreateResponse(HttpStatusCode.OK, newUsuario);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Não editou");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Usuário com E-mail: " + usuario.Email + " já cadastrado");
        }

        public HttpResponseMessage GetUsuario([FromUri] int id)
        {
            var usuario = new Usuario();
            usuario.Id = id;
            if (usuario.buscarPorId() != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, usuario.buscarPorId());

            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Curso " + usuario.Id + " não encontrado");
        }
    }
    
}