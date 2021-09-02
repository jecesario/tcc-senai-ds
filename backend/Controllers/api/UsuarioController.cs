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
            if(usuario.buscarPorEmail() == null)
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
    }
}