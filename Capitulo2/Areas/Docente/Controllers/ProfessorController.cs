using Capitulo2.Areas.Docente.Models;
using Capitulo2.Data;
using Capitulo2.Data.DAL.Cadastros;
using Capitulo2.Data.DAL.Docente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelo.Cadastros;
using Modelo.Docente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2.Areas.Docente.Controllers {
    [Area("Docente")]
    [Authorize]
    public class ProfessorController : Controller {

        private readonly IESContext _context;
        private readonly InstituicaoDAL instituicaoDAL;
        private readonly DepartamentoDAL departamentoDAL;
        private readonly CursoDAL cursoDAL;
        private readonly ProfessorDAL professorDAL;

        public ProfessorController(IESContext context) {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
            departamentoDAL = new DepartamentoDAL(context);
            cursoDAL = new CursoDAL(context);
            professorDAL = new ProfessorDAL(context);
        }

        public void PrepararViewBags(List<Instituicao> instituicoes, List<Departamento> departamentos, List<Curso> cursos, List<Professor> professores) {
            instituicoes.Insert(0, new Instituicao() { InstituicaoId = 0, Nome = "Selecione a Insituição" });
            ViewBag.Instituicoes = instituicoes;

            departamentos.Insert(0, new Departamento() { DepartamentoId = 0, Nome = "Selecione o Departamento" });
            ViewBag.Departamentos = departamentos;

            cursos.Insert(0, new Curso() { CursoId = 0, Nome = "Selecione o Curso" });
            ViewBag.Cursos = cursos;

            professores.Insert(0, new Professor() { ProfessorId = 0, Nome = "Selecione o Professor" });
            ViewBag.Professores = professores;
        }


        // Cadastro do Professor no Curso
        [HttpGet]
        public IActionResult AdicionarProfessor() {
            PrepararViewBags(instituicaoDAL.ObterInstituicoesClassificadasId().ToList(), new List<Departamento>().ToList(), new List<Curso>().ToList(), new List<Professor>().ToList());
            return View();
        }
        [HttpPost]
        public IActionResult AdicionarProfessor([Bind("InsituicaoId, DepartamentoId, CursoId, ProfessorId")] AdicionarProfessorViewModel model) {
            if (model.InstituicaoId == 0 || model.DepartamentoId == 0 || model.CursoId == 0 || model.ProfessorId == 0) {
                ModelState.AddModelError("", "É prececiso selecionar todos os dados");
            } else {
                cursoDAL.RegistrarProfessor((int)model.CursoId, (int)model.ProfessorId);
                PrepararViewBags(instituicaoDAL.ObterInstituicoesClassificadasId().ToList(),
                departamentoDAL.ObterDepartamentosPorInstituicao((int)model.InstituicaoId).ToList(),
                cursoDAL.ObterCursosPorDepartamento((int)model.DepartamentoId).ToList(),
                cursoDAL.ObterProfessoresForaDoCurso((int)model.CursoId).ToList());
            }
            return View(model);
        }

        public JsonResult ObterDepartamentosPorInstituicao(int actionID) {
            var departamentos = departamentoDAL.ObterDepartamentosPorInstituicao(actionID).ToList();
            return Json(new SelectList(departamentos, "DepartamentoId", "Nome"));}
        public JsonResult ObterCursosPorDepartamento(int actionID) {
            var cursos = cursoDAL.ObterCursosPorDepartamento(actionID).ToList();
            return Json(new SelectList(cursos, "CursoId", "Nome"));
        }
        public JsonResult ObterProfessoresForaDoCurso(int actionID) {
            var professores = cursoDAL.ObterProfessoresForaDoCurso(actionID).ToList();
            return Json(new SelectList(professores, "ProfessorId", "Nome"));
        }


    }
}
