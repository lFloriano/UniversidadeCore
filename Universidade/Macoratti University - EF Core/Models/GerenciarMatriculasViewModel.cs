using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidade.Models
{
    public class GerenciarMatriculasViewModel
    {
        public CursoViewModel Curso { get; set; }
        public List<MatriculaViewModel> Matriculas { get; set; }
    }
}
