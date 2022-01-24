using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Cadastros {
    public class Disciplina {

        public int? DisciplinaId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<CursoDisciplina> CursoDisciplinas { get; set; }

    }
}
