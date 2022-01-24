using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Cadastros {
    public class Departamento {

        public int? DepartamentoId { get; set; } // Chave Primaria
        public string Nome { get; set; }

        // Associação à outro objeto
        public int? InstituicaoId { get; set; } // Chave estrangeira
        public Instituicao Instituicao { get; set; } // Referencia de Instituição,
        // mostrando que o Objeto Departamento faz parte/pertence do/ao objeto Instituição

        // Varios cursos pertencem a um Departamento
        public virtual ICollection<Curso> Cursos { get; set; }

        // Instituição tem Departamentos / - / Um ou vários Departamento(s) pertence(m) à uma Instituição 
    }
}
