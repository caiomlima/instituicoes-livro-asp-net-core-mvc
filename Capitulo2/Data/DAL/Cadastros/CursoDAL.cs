using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using Modelo.Docente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2.Data.DAL.Cadastros {
    public class CursoDAL {

        private IESContext _context;

        public CursoDAL(IESContext context) {
            _context = context;
        }

        public void RegistrarProfessor(int cursoId, int professorId) {
            var curso = _context.Cursos.Where(c => c.CursoId == cursoId).Include(cp => cp.CursoProfessores).First();
            var professor = _context.Professores.Find(professorId);
            curso.CursoProfessores.Add(new CursoProfessor() {
                Curso = curso, Professor = professor
            });
            _context.SaveChanges();
        }

        public IQueryable<Curso> ObterCursosPorDepartamento(long departamentoId) {
            var cursos = _context.Cursos.Where(c => c.DepartamentoId == departamentoId).OrderBy(d => d.DepartamentoId);
            return cursos;
        }

        public IQueryable<Professor> ObterProfessoresForaDoCurso(int cursoId) {
            var curso = _context.Cursos.Where(c => c.CursoId == cursoId).Include(cp => cp.CursoProfessores).First();
            var professoresDoCurso = curso.CursoProfessores.Select(cp =>cp.ProfessorId).ToArray();
            var professoresForaDoCurso = _context.Professores.Where(p =>!professoresDoCurso.Contains(p.ProfessorId));
            return professoresForaDoCurso;
        }



    }
}
