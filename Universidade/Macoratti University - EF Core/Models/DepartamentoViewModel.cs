using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Universidade.Models
{
    public class DepartamentoViewModel
    {
        public int DepartamentoID { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode ser maior que 100 caracteres.")]
        [RegularExpression(@"^(?:[a-zA-Z]+\s?)+[a-zA-Z]+$", ErrorMessage = "O campo deve conter apenas letras")]
        public string Nome { get; set; }
        
        [Required]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Supervisor { get; set; }
        public ICollection<CursoViewModel> Cursos { get; set; }
    }
}
