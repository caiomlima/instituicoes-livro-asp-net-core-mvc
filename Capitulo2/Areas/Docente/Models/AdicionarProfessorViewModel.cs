using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2.Areas.Docente.Models {
    public class AdicionarProfessorViewModel {

        public int? InstituicaoId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? CursoId { get; set; }
        public int? ProfessorId { get; set; }

    }
}
