using Capitulo2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Cadastros {
    public class InstituicaoDAL {

        private IESContext _context;

        public InstituicaoDAL(IESContext context) {
            _context = context;
        }

        public IQueryable<Instituicao> ObterInstituicoesClassificadasId() {
            return _context.Instituicoes.OrderBy(b => b.InstituicaoId);
        }

        // GET por id
        public async Task<Instituicao> ObterInstituicaoId(int? id) {
            return await _context.Instituicoes.Include(d => d.Departamentos).SingleOrDefaultAsync(m => m.InstituicaoId == id);
        }

        // Create e Update POST
        public async Task<Instituicao> GravarInstituicao(Instituicao instituicao) {
            if(instituicao.InstituicaoId == null) {
                _context.Instituicoes.Add(instituicao);
            } else {
                _context.Update(instituicao);
            }
            await _context.SaveChangesAsync();
            return instituicao;
        }

        // POST Delete
        public async Task<Instituicao> EliminarInstituicaoId(int id) {
            Instituicao instituicao = await ObterInstituicaoId(id);
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            return instituicao;
        }
    }
}
