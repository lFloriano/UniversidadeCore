using System.Collections.Generic;

namespace Universidade.Core.Entidades
{
    public abstract class EntidadeBase
    {
        public abstract void Validar();

        public List<string> MensagensErro = new List<string>();
    }
}
