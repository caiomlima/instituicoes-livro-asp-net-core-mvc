using Modelo.Cadastros;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo.Discente;
using Capitulo2.Models.Infra;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Modelo.Docente;

namespace Capitulo2.Data {
    public class IESContext : IdentityDbContext<UsuarioDoApp> {

        public IESContext(DbContextOptions<IESContext> options) : base(options) {
        }
        
        // BdSet faz com que uma Entidade (Arquivo.cs) seja criada como tabela
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Academico> Academicos { get; set; }
        public DbSet<Professor> Professores { get; set; }

        // Metodos para modificações das tabelas
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoDisciplina>()
                .HasKey(cd => new { cd.CursoId, cd.DisciplinaId });

            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(c => c.Curso)
                .WithMany(cd => cd.CursoDisciplinas)
                .HasForeignKey(c => c.CursoId);

            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(d => d.Disciplina)
                .WithMany(cd => cd.CursoDisciplinas)
                .HasForeignKey(d => d.DisciplinaId);

            modelBuilder.Entity<CursoProfessor>()
                .HasKey(cd => new { cd.CursoId, cd.ProfessorId });

            modelBuilder.Entity<CursoProfessor>()
                .HasOne(c => c.Curso)
                .WithMany(cd => cd.CursoProfessores)
                .HasForeignKey(cd => cd.CursoId);

            modelBuilder.Entity<CursoProfessor>()
                .HasOne(d => d.Professor)
                .WithMany(cd => cd.CursoProfessores)
                .HasForeignKey(d => d.ProfessorId);
        }

    }
}
