using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Cadastros {
    public class CursoDisciplina {

        public int? CursoId { get; set; }
        public Curso Curso { get; set; }

        public int? DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }

    }
}
