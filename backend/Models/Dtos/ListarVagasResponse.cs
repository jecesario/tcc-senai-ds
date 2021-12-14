using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Dtos
{
    public class ListarVagasResponse
    {
        public int Id { get; set; }
        public string DataPostagem { get; set; }
        public string DataLimite { get; set; }
        public string Cargo { get; set; }
        public string Localidade { get; set; }
        public string Contratante { get; set; }
        public double Remuneracao { get; set; }
        public string Beneficios { get; set; }
        public string Requisitos { get; set; }
        public string Atribuicoes { get; set; }
        public string Tipo { get; set; }
        public string Jornada { get; set; }
    }
}