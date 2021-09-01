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
    public class HabilidadeController : ApiController
    {
        // ok Jean
        public List<Habilidade> Get()
        {
            return Habilidade.listar();
        }
        // ok Jean
        public HttpResponseMessage Post([FromBody] Habilidade habilidade)
        {
            if (habilidade.cadastrar())
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // ok Jean
        public HttpResponseMessage Delete([FromUri] int id)
        {
            var habilidade = new Habilidade();
            habilidade.Id = id;
            if (habilidade.apagar())
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Habilidade " + id + " não encontrada");
        }

        // ok Jean
        public HttpResponseMessage Put([FromBody] Habilidade habilidade)
        {
            var newHabilidade = habilidade.buscarPorId();

            if (newHabilidade != null)
            {
                if (habilidade.editar())
                {
                    newHabilidade = habilidade.buscarPorId();
                    return Request.CreateResponse(HttpStatusCode.OK, newHabilidade);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Habilidade " + habilidade.Id + " não encontrada");

        }



        // ok Jean
        public HttpResponseMessage GetHabilidade([FromUri] int id)
        {
            var habilidade = new Habilidade();
            habilidade.Id = id;
            if (habilidade.buscarPorId() != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, habilidade.buscarPorId());

            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Habilidade " + habilidade.Id + " não encontrada");
        }
    }
}
