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
    public class CurriculoController : ApiController
    {
        // GET: Curriculo
        public HttpResponseMessage Get()
        {
            var headers = Request.Headers;
            if(headers.Contains("Authorization"))
            {
                var token = headers.GetValues("Authorization").First();
                if(Usuario.validarToken(token))
                {
                    var curriculos = Curriculo.listar();
                    return Request.CreateResponse(HttpStatusCode.OK, curriculos);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Parece que você não tem acesso!");
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Curriculo/{id}")]
        public HttpResponseMessage GetById([FromUri] int id)
        {
            var headers = Request.Headers;
            if (headers.Contains("Authorization"))
            {
                var token = headers.GetValues("Authorization").First();
                if (Usuario.validarToken(token))
                {
                    var curriculo = new Curriculo();
                    curriculo.Id = id;
                    var curriculos = curriculo.buscarPorId();
                    return Request.CreateResponse(HttpStatusCode.OK, curriculos);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Parece que você não tem acesso!" + id);
        }
    }
}