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
    public class CursoController : ApiController
    {
        // GET: Curso
        public List<Curso> Get()
        {
            return Curso.listar();
        }

        public HttpResponseMessage Post([FromBody] Curso curso)
        {
            if (curso.cadastrar())
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}