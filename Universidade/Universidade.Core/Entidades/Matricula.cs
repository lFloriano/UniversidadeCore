using Universidade.Core.Enum;

namespace Universidade.Core.Entidades
{
    public class Matricula : EntidadeBase
    {
        public int MatriculaID { get; protected set; }
        public int CursoID { get; protected set; }
        public int EstudanteID { get; protected set; }
        public Nota? Nota { get; protected set; }
        public Curso Curso { get; protected set; }
        public Estudante Estudante { get; protected set; }

        public override void Validar()
        {
            ValidarCurso();
            ValidarEstudante();
        }

        public bool ValidarCurso()
        {
            if(CursoID <= 0)
            {
                MensagensErro.Add("Id do curso não é valido");

                return false;
            }

            return true;
        }

        public bool ValidarEstudante()
        {
            if (EstudanteID <= 0)
            {
                MensagensErro.Add("Id do estudante não é valido");

                return false;
            }

            return true;
        }
    }
}
