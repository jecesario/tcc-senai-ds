using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Curriculo
    {
        public int Id { get; set; }
        public string Github { get; set; }
        public string Linkedin { get; set; }
        public string Telefone { get; set; }
        public string Resumo { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Usuario Usuario { get; set; }
        public Curso Curso { get; set; }
    }
}