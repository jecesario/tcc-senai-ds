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
        public HttpResponseMessage Delete([FromUri] int id)
        {
            var curso = new Curso();
            curso.Id = id;
            if (curso.apagar())
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Curso " + id + " não encontrado");
        }

        public HttpResponseMessage Put([FromBody] Curso curso)
        {
            var newCurso = curso.buscarPorId();

            if (newCurso != null)
            {
                if (curso.editar())
                {
                    newCurso = curso.buscarPorId();
                    return Request.CreateResponse(HttpStatusCode.OK, newCurso);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "msg " + newCurso);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Curso " + curso.Id + " não encontrado");

        }

    }
}