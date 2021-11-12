using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Dtos
{
    public class UsuarioLoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}