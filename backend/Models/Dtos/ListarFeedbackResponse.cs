using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Dtos {
    public class ListarFeedbackResponse {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public Usuario Usuario { get; set; }
    }
}