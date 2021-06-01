using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universidade.Core.Entidades
{
    public class Curso : EntidadeBase
    {
        public Curso()
        {
            Matriculas = new List<Matricula>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CursoID { get; protected set; }
        public string Titulo { get; protected set; }
        public int Creditos { get; protected set; }
        public ICollection<Matricula> Matriculas { get; protected set; }
        public int DepartamentoID { get; protected set; }
        public Departamento Departamento { get; protected set; }
        public int LotacaoAlunos { get; protected set; }

        public override void Validar()
        {
            ValidarTitulo();
            ValidarCreditos();
            ValidarLotacao();
        }

        public bool ValidarTitulo()
        {
            if (String.IsNullOrEmpty(Titulo))
            {
                MensagensErro.Add("Título do curso é obrigatório");
                return false;
            }

            if (Titulo.Length > 100)
            {
                MensagensErro.Add("Título do curso não pode ultrapassar 100 caracteres");
                return false;
            }

            return true;
        }

        public bool ValidarCreditos()
        {
            if(Creditos < 1)
            {
                MensagensErro.Add("Créditos não podem ser inferiores a 1");
                return false;
            }

            return true;
        }

        public bool ValidarLotacao()
        {
            if(LotacaoAlunos < 0)
            {
                MensagensErro.Add("Lotação do curso não pode ser inferior a zero");
                return false;
            }

            return true;
        }
    }
}
