using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace backend.Controllers.api
{
    public class HabilidadeController : ApiController
    {
        // GET: HabilidadeApi
        public List<Habilidade> Get()
        {
            var habilidades = Habilidade.listar();
            return habilidades;
        }
    }
}