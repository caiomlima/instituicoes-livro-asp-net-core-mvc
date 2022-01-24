using Modelo.Discente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2.Data.DAL.Discente {
    public class AcademicoDAL {

        private IESContext _context;

        public AcademicoDAL(IESContext context) {
            _context = context;
        }

        public IQueryable<Academico> ObterAcademicosClassId() {
            return _context.Academicos.OrderBy(b => b.AcademicoId);
        }

        public async Task<Academico> ObterAcademicoId(int id) {
            return await _context.Academicos.FindAsync(id);
        }

        public async Task<Academico> GravarAcademico(Academico academico) {
            if(academico.AcademicoId == null) {
                _context.Academicos.Add(academico);
            } else {
                _context.Update(academico);
            }
            await _context.SaveChangesAsync();
            return academico;
        }

        public async Task<Academico> EliminarAcademicoId(int id) {
            Academico academico = await ObterAcademicoId(id);
            _context.Academicos.Remove(academico);
            await _context.SaveChangesAsync();
            return academico;
        }

    }
}
