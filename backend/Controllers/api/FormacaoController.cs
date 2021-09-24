using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace backend.Controllers.api
{
    public class FormacaoController : ApiController
    {
        [HttpGet]
        [Route("api/Formacao/{curriculoId}")]
        public HttpResponseMessage GetFormacoes([FromUri] int curriculoId)
        {
            var formacao = new Formacao();
            formacao.CurriculoId = curriculoId;

            if(formacao.buscarPorCurriculoId() != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, formacao.buscarPorCurriculoId());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Nenhuma formação encontrada para o curriculo " + curriculoId);
        }
    }
}