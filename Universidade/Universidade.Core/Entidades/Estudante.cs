using System;
using System.Collections.Generic;

namespace Universidade.Core.Entidades
{
    public class Estudante : EntidadeBase
    {
        public int EstudanteID { get; protected set; }
        public string SobreNome { get; protected set; }
        public string Nome { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public ICollection<Matricula> Matriculas { get; protected set; }

        public override void Validar()
        {
            ValidarNome();
            ValidarSobrenome();
            ValidarDataCricao();
        }

        public bool ValidarNome()
        {
            if (String.IsNullOrEmpty(Nome))
            {
                MensagensErro.Add("Nome do estudante é obrigatório");
                return false;
            }

            if (Nome.Length > 100)
            {
                MensagensErro.Add("Nome do estudante não pode ultrapassar 100 caracteres");
                return false;
            }

            return true;
        }

        public bool ValidarSobrenome()
        {
            if (String.IsNullOrEmpty(Nome))
            {
                MensagensErro.Add("Sobrenome do estudante é obrigatório");
                return false;
            }

            if (Nome.Length > 100)
            {
                MensagensErro.Add("Sobrenome do estudante não pode ultrapassar 100 caracteres");
                return false;
            }

            return true;
        }

        public bool ValidarDataCricao()
        {
            if(DataCriacao <= new DateTime(1950, 1, 1))
            {
                MensagensErro.Add("A data dee ser superior a 01/01/1950");
                return false;
            }

            return true;
        }
    }
}
