using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Universidade.Models
{
    public class CursoViewModel
    {
        public int CursoID { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "O título não pode ter mais que 100 caracteres.")]
        [RegularExpression(@"^(?:[a-zA-Z]+\s?)+[a-zA-Z]+$", ErrorMessage = "O título deve conter apenas letras")]
        public string Titulo { get; set; }
        
        [Required]
        [Range(0, 99, ErrorMessage = "Os créditos não podem ter mais que 2 caracteres.")]
        public int Creditos { get; set; }
        public ICollection<MatriculaViewModel> Matriculas { get; set; }
        
        [Required]
        public int DepartamentoID { get; set; }

        [Display(Name ="Alocação Máxima")]
        public int LotacaoAlunos { get; set; }
        public DepartamentoViewModel Departamento { get; set; }

        [Display(Name="Nº de Alunos")]
        public int QuantidadeMatriculas { 
            get 
            {
                return Matriculas != null ? Matriculas.Count : 0; 
            } 
        }
    }
}
