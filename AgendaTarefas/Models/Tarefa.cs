using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaTarefas.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; }

        [Required(ErrorMessage = "{0} Campo Obrigatorio!")]
        [StringLength(50, ErrorMessage = "Limite de Caracteres")]
        public string  Nome { get; set; }
        public string Data { get; set; }
        [Required(ErrorMessage = "{0} Campo Obrigatorio!")]
        [DataType(DataType.Time)]
        public string Horario { get; set; }
    }
}
