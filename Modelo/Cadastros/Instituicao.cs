using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Cadastros {
    public class Instituicao {

        public int? InstituicaoId { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        // Faz referencia à Departamento, mostrando que Insituicao tem um
        // ou vários Departamentos, e que Departamento faz parte desse objeto
        public virtual ICollection<Departamento> Departamentos { get; set; }

    }
}
