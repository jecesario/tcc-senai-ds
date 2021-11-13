using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Dtos
{
    public class ListarCurriculoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cidade { get; set; }
        public string Habilidades { get; set; }
    }
}