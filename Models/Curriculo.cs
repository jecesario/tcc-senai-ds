using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Curriculo
    {
        public int Id { get; set; }
        public string Aluno { get; set; }

        public static List<Curriculo> listar()
        {
            var lista = new List<Curriculo>();
            var curriculo = new Curriculo();
            curriculo.Id = 1;
            curriculo.Aluno = "Eduardo";
            lista.Add(curriculo);
            

            return lista;
        }
    }
}