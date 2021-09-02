using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}