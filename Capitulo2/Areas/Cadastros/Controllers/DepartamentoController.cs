using Capitulo2.Data;
using Modelo.Cadastros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Capitulo2.Areas.Cadastros.Controllers {
    [Area("Cadastros")]
    [Authorize]
    public class DepartamentoController : Controller {

        private readonly IESContext _context;
        private readonly DepartamentoDAL departamentoDAL;
        private readonly InstituicaoDAL instituicaoDAL;

        public DepartamentoController(IESContext context) {
            this._context = context;
            instituicaoDAL = new InstituicaoDAL(context);
            departamentoDAL = new DepartamentoDAL(context);
        }

        private async Task<IActionResult> ObterViewDepartamentoId(int? id) {
            if (id == null) {
                return NotFound();
            }
            var departamento = await departamentoDAL.ObterDepartamentoId((int)id);
            if (departamento == null) {
                return NotFound();
            }
            return View(departamento);
        }

        public async Task<IActionResult> Index() {
            return View(await departamentoDAL.ObterDepartamentosClassificadosId().ToListAsync());
        }

        // GET e POST Create
        [HttpGet]
        public IActionResult Create() {
            // Faz um dropdown list para selecionar objeto de outra tabela, onde essa e a outra são relacionadas
            var instituicoes = instituicaoDAL.ObterInstituicoesClassificadasId().ToList();
            instituicoes.Insert(0, new Instituicao() { InstituicaoId = 0, Nome = "Selecione a instituição" });
            ViewBag.instituicoes = instituicoes;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, InstituicaoId")] Departamento departamento) {
            try {
                if(ModelState.IsValid) {
                    await departamentoDAL.GravarDepartamento(departamento);
                    return RedirectToAction(nameof(Index));
                }
            } catch (DbUpdateException) {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(departamento);
        }



        // GET e POST Edit
        public async Task<IActionResult> Edit(int? id) {
            ViewResult visaoDepartamento = (ViewResult)await ObterViewDepartamentoId(id);
            Departamento departamento = (Departamento)visaoDepartamento.Model;
            ViewBag.Instituicoes = new SelectList(instituicaoDAL.ObterInstituicoesClassificadasId(), "InstituicaoId", "Nome", departamento.InstituicaoId);
            return View(departamento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("DepartamentoId, Nome, InstituicaoId")] Departamento departamento) {
            if(id != departamento.DepartamentoId) {
                return NotFound();
            }
            if(ModelState.IsValid) {
                try {
                    await departamentoDAL.GravarDepartamento(departamento);
                } catch(DbUpdateConcurrencyException) {
                    if(!await DepartamentoExists(departamento.DepartamentoId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Instituicoes = new SelectList(instituicaoDAL.ObterInstituicoesClassificadasId(), "InstituicaoId", "Nome", departamento.InstituicaoId);
            return View(departamento);
        }
        private async Task<bool> DepartamentoExists(int? id) {
            return await departamentoDAL.ObterDepartamentoId((int) id) != null;
        }



        // GET Details
        public async Task<IActionResult> Details(int? id) {
            return await ObterViewDepartamentoId(id);
        }



        // GET e POST Delete
        public async Task<IActionResult> Delete(int? id) {
            return await ObterViewDepartamentoId(id);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id) {
            var departamento = await departamentoDAL.EliminarDepartamentoId((int)id);
            TempData["Message"] = "Departamento	" + departamento.Nome.ToUpper() + "	foi	removido"; // TempData para avisos
            return RedirectToAction(nameof(Index));
        }

    }
}
