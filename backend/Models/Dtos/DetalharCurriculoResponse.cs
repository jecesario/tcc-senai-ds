using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Dtos
{
    public class DetalharCurriculoResponse
    {
        public int Id { get; set; }
        public string Github { get; set; }
        public string Linkedin { get; set; }
        public string Telefone { get; set; }
        public string Resumo { get; set; }
        public string Endereco { get; set; }
        public string Localidade { get; set; }
        public string DataEdicao { get; set; }
        public string Curso { get; set; }
        public string Anexo { get; set; }
        public Usuario Usuario { get; set; }
        public List<Experiencia> Experiencias { get; set; }
        public List<Formacao> Formacoes { get; set; }
        public List<Habilidade> Habilidades { get; set; }
    }
}