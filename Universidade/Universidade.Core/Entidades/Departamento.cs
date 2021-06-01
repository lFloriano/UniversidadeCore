using System;
using System.Collections.Generic;

namespace Universidade.Core.Entidades
{
    public class Departamento : EntidadeBase
    {
        public Departamento()
        {
            Cursos = new List<Curso>();
        }

        public int DepartamentoID { get; protected set; }
        public string Nome { get; protected set; }
        public string Supervisor { get; protected set; }
        public ICollection<Curso> Cursos { get; protected set; }

        public override void Validar()
        {
            ValidarNome();
            ValidarSupervisor();
        }

        public bool ValidarNome()
        {
            if (String.IsNullOrEmpty(Nome))
            {
                MensagensErro.Add("Nome do departamento é obrigatório");
                return false;
            }

            if (Nome.Length > 100)
            {
                MensagensErro.Add("Nome do departamento não pode ultrapassar 100 caracteres");
                return false;
            }            

            return true;
        }
        public bool ValidarSupervisor()
        {
            if (String.IsNullOrWhiteSpace(Supervisor))
            {
                MensagensErro.Add("Supervisor inválido");
                return false;
            }

            return true;
        }
    }
}
