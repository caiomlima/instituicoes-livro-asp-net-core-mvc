using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Docente
{
    public class CursoProfessor {

        public int? CursoId { get; set; }
        public Curso Curso { get; set; }
        public int? ProfessorId { get; set; }
        public Professor Professor { get; set; }

    }
}
