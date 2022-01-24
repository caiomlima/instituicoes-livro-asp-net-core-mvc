using Modelo.Docente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Cadastros {
    public class Curso {

        public int? CursoId { get; set; }
        public string Nome { get; set; }

        public int? DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        public virtual ICollection<CursoDisciplina> CursoDisciplinas { get; set; }
        public virtual ICollection<CursoProfessor> CursoProfessores { get; set; }

    }
}
