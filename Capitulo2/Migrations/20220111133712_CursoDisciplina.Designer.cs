﻿// <auto-generated />
using System;
using Capitulo2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Capitulo2.Migrations
{
    [DbContext(typeof(IESContext))]
    [Migration("20220111133712_CursoDisciplina")]
    partial class CursoDisciplina
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Modelo.Cadastros.Curso", b =>
                {
                    b.Property<int?>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CursoId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Modelo.Cadastros.CursoDisciplina", b =>
                {
                    b.Property<int?>("CursoId")
                        .HasColumnType("int");

                    b.Property<int?>("DisciplinaId")
                        .HasColumnType("int");

                    b.HasKey("CursoId", "DisciplinaId");

                    b.HasIndex("DisciplinaId");

                    b.ToTable("CursoDisciplina");
                });

            modelBuilder.Entity("Modelo.Cadastros.Departamento", b =>
                {
                    b.Property<int?>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("InstituicaoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartamentoId");

                    b.HasIndex("InstituicaoId");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("Modelo.Cadastros.Disciplina", b =>
                {
                    b.Property<int?>("DisciplinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DisciplinaId");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("Modelo.Cadastros.Instituicao", b =>
                {
                    b.Property<int?>("InstituicaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstituicaoId");

                    b.ToTable("Instituicoes");
                });

            modelBuilder.Entity("Modelo.Cadastros.Curso", b =>
                {
                    b.HasOne("Modelo.Cadastros.Departamento", "Departamento")
                        .WithMany("Cursos")
                        .HasForeignKey("DepartamentoId");

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("Modelo.Cadastros.CursoDisciplina", b =>
                {
                    b.HasOne("Modelo.Cadastros.Curso", "Curso")
                        .WithMany("CursoDisciplinas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modelo.Cadastros.Disciplina", "Disciplina")
                        .WithMany("CursoDisciplinas")
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Disciplina");
                });

            modelBuilder.Entity("Modelo.Cadastros.Departamento", b =>
                {
                    b.HasOne("Modelo.Cadastros.Instituicao", "Instituicao")
                        .WithMany("Departamentos")
                        .HasForeignKey("InstituicaoId");

                    b.Navigation("Instituicao");
                });

            modelBuilder.Entity("Modelo.Cadastros.Curso", b =>
                {
                    b.Navigation("CursoDisciplinas");
                });

            modelBuilder.Entity("Modelo.Cadastros.Departamento", b =>
                {
                    b.Navigation("Cursos");
                });

            modelBuilder.Entity("Modelo.Cadastros.Disciplina", b =>
                {
                    b.Navigation("CursoDisciplinas");
                });

            modelBuilder.Entity("Modelo.Cadastros.Instituicao", b =>
                {
                    b.Navigation("Departamentos");
                });
#pragma warning restore 612, 618
        }
    }
}