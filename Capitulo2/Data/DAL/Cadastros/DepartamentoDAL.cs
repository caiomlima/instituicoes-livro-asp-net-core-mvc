using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2.Data {
    public class DepartamentoDAL {

		private IESContext _context;

		public DepartamentoDAL(IESContext context) {
			_context = context;
		}

		public IQueryable<Departamento> ObterDepartamentosClassificadosId() {
			return _context.Departamentos.Include(i => i.Instituicao).OrderBy(b => b.DepartamentoId);
		}

		// GET por id
		public async Task<Departamento> ObterDepartamentoId(int  id) {
			var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoId == id);
			_context.Instituicoes.Where(i => departamento.InstituicaoId == i.InstituicaoId).Load(); ;
			return departamento;
		}

		// Create e Update POST
		public async Task<Departamento> GravarDepartamento(Departamento  departamento) {
			if (departamento.DepartamentoId == null) {
				_context.Departamentos.Add(departamento);
												} else {
				_context.Update(departamento);
			}
			await _context.SaveChangesAsync();
			return departamento;
		}

		// POST Delete
		public async Task<Departamento> EliminarDepartamentoId(int id) {
			Departamento departamento = await ObterDepartamentoId(id);
			_context.Departamentos.Remove(departamento);
			await _context.SaveChangesAsync();
			return departamento;
		}

		public IQueryable<Departamento> ObterDepartamentosPorInstituicao(int instituicaoId) {
			var departamentos = _context.Departamentos.Where(d => d.InstituicaoId == instituicaoId).OrderBy(d => d.InstituicaoId);
			return departamentos;
		}


	}
}
