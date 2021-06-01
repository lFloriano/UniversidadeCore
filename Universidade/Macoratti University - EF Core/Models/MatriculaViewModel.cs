using System.ComponentModel.DataAnnotations;
using Universidade.Models.Enum;

namespace Universidade.Models
{
    public class MatriculaViewModel
    {       
        public int MatriculaID { get; set; }
        public int CursoID { get; set; }
        public int EstudanteID { get; set; }
        public Nota? Nota { get; set; }
        public CursoViewModel Curso { get; set; }
        public EstudanteViewModel Estudante { get; set; }
    }
}
