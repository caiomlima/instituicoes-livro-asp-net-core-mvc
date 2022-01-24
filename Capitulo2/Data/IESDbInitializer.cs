using Modelo.Cadastros;
using Modelo.Docente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2.Data {
    public class IESDbInitializer {

        public static void Initialize(IESContext context) {
            context.Database.EnsureCreated();

			if (context.Departamentos.Any()) {
				return;
			}
			var instituicoes = new Instituicao[] {
				new Instituicao {Nome="UniPaulista", Endereco="São Paulo"},
				new Instituicao {Nome="UniCarioca", Endereco="Rio de Janeiro"}
			};
			foreach (Instituicao i in instituicoes) {
				context.Instituicoes.Add(i);
			}
			context.SaveChanges();

			var departamentos = new Departamento[] {
				new Departamento {Nome="Ciência da Computação", InstituicaoId=1},
				new Departamento {Nome="Ciência de Alimentos", InstituicaoId=2}
			};
			foreach (Departamento d in departamentos) {
				context.Departamentos.Add(d);
			}
			context.SaveChanges();


			if(context.Cursos.Any()) {
				return;
            }
			var cursos = new Curso[] {
				new Curso {Nome="Engenharia da Computação", DepartamentoId=1},
				new Curso {Nome="Engenharia de Alimentos", DepartamentoId=2}
			};
			foreach (Curso c in cursos) {
				context.Cursos.Add(c);
			}
			context.SaveChanges();


			if(context.Professores.Any()) {
				return;
            }
			var professores = new Professor[] {
				new Professor {Nome="José dos Santos"},
				new Professor {Nome="Camila Ferreira"}
			};
			foreach (Professor p in professores) {
				context.Professores.Add(p);
			}
			context.SaveChanges();

		}

	}
}
