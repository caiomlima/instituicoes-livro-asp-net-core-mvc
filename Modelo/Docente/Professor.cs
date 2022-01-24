using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Docente {
    public class Professor {

        public int? ProfessorId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<CursoProfessor> CursoProfessores { get; set; }

    }
}
