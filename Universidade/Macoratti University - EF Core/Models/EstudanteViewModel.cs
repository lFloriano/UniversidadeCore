using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Universidade.Models
{
    public class EstudanteViewModel
    {
        public int EstudanteID { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "O nome não pode ser maior que 50 caracteres.")]
        [RegularExpression(@"^(?:[a-zA-Z]+\s?)+[a-zA-Z]+$", ErrorMessage = "O nome deve conter apenas letras")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Campo obrigatório")]
        [RegularExpression(@"^(?:[a-zA-Z]+\s?)+[a-zA-Z]+$", ErrorMessage="O Sobrenome deve conter apenas letras")]
        [StringLength(50, ErrorMessage = "O sobrenome não pode ser maior que 50 caracteres.")]
        public string SobreNome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Data de Início")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataCriacao { get; set; }
        public ICollection<MatriculaViewModel> Matriculas { get; set; }
        public string NomeCompleto { get { return $"{Nome} {SobreNome}"; } }
    }
}
