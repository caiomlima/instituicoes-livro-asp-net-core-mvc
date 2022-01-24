using Capitulo2.Data;
using Modelo.Cadastros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Capitulo2.Areas.Cadastros.Controllers {
    [Area("Cadastros")]
    [Authorize]
    public class InstituicaoController : Controller {

        private readonly IESContext _context;
        private readonly InstituicaoDAL instituicaoDAL;

        public InstituicaoController(IESContext context) {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
        }

        // Instancia do DAL para redução de redundancia GET (Trazer item por id)
        private async Task<IActionResult> ObterViewInstituicaoId(int? id) {
            // Verifica se o item possui ou não um id
            if (id == null) {
                return NotFound();
            }
            var instituicao = await instituicaoDAL.ObterInstituicaoId((int)id);
            // Verifica se o id é existente
            if (instituicaoDAL == null) {
                return NotFound();
            }
            return View(instituicao);
        }

        public async Task<IActionResult> Index() {
            return View(await instituicaoDAL.ObterInstituicoesClassificadasId().ToListAsync());
        }



        // GET e POST Create
        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstituicaoId,Nome,Endereco")] Instituicao instituicao) {
            try {
                if (ModelState.IsValid) {
                    await instituicaoDAL.GravarInstituicao(instituicao);
                    return RedirectToAction(nameof(Index));
                }
            } catch (DbUpdateException) {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(instituicao);
        }



        // GET e POST Edit
        public async Task<IActionResult> Edit(int? id) {
            return await ObterViewInstituicaoId(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("InstituicaoId, Nome, Endereco")] Instituicao instituicao) {
            if (id != instituicao.InstituicaoId) {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try {
                    await instituicaoDAL.GravarInstituicao(instituicao);
                } catch (DbUpdateConcurrencyException) {
                    if (!await InstituicaoExists(instituicao.InstituicaoId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instituicao);
        }
        private async Task<bool> InstituicaoExists(long? id) {
            return await instituicaoDAL.ObterInstituicaoId((int)id)!= null;
        }



        // GET Details
        public async Task<IActionResult> Details(int? id) {
            return await ObterViewInstituicaoId(id);
        }



        // GET e POST Delete
        public async Task<IActionResult> Delete(int? id) {
            return await ObterViewInstituicaoId(id);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id) {
            var instituicao = await instituicaoDAL.EliminarInstituicaoId((int) id);
            TempData["Message"] = "Instituição	" + instituicao.Nome.ToUpper() + "	foi	removida"; // TempData para avisos
            return RedirectToAction(nameof(Index));
        }

    }
}
