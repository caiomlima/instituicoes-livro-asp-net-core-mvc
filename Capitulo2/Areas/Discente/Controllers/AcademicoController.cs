using Capitulo2.Data;
using Capitulo2.Data.DAL.Discente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Modelo.Discente;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2.Areas.Discente.Controllers {
	[Area("Discente")]
	[Authorize]
	public class AcademicoController : Controller {

		private readonly IESContext _context;
		private IHostingEnvironment _env;
		private readonly AcademicoDAL academicoDAL;

		public AcademicoController(IESContext context, IHostingEnvironment env) {
			_context = context;
			_env = env;
			academicoDAL = new AcademicoDAL(context);
		}

		private async Task<IActionResult> ObterViewAcademicoId(int? id) {
			if (id == null) {
				return NotFound();
			}
			var academico = await academicoDAL.ObterAcademicoId((int)id);
			if (academico == null) {
				return NotFound();
			}
			return View(academico);
		}



		public async Task<IActionResult> Index() {
			return View(await academicoDAL.ObterAcademicosClassId().ToListAsync());
		}



		//	GET e POST Create
		public IActionResult Create() {
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Nome, RegistroAcademico, Nascimento")] Academico academico, IFormFile foto)

		{
			try {
				if (ModelState.IsValid) {
					var stream = new MemoryStream();
					await foto.CopyToAsync(stream);
					academico.Foto = stream.ToArray();
					academico.FotoMimeType = foto.ContentType;
					await academicoDAL.GravarAcademico(academico);
					return RedirectToAction(nameof(Index));
				}
			} catch (DbUpdateException) {
				ModelState.AddModelError("", "Não foi possível inserir os dados.");
			}
			return View(academico);
		}



		// GET e POST Edit
		public async Task<IActionResult> Edit(int? id) {
			return await ObterViewAcademicoId(id);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, [Bind("AcademicoId, Nome, RegistroAcademico, Nascimento")] Academico	academico, IFormFile foto, string checkRemoverFoto)

		{
			if (id != academico.AcademicoId) {
				return NotFound();
												}
			if (ModelState.IsValid) {
				try {
					var stream = new MemoryStream();
					if(checkRemoverFoto != null) {
						academico.Foto = null;
                    } else {
						await foto.CopyToAsync(stream);
						academico.Foto = stream.ToArray();
						academico.FotoMimeType = foto.ContentType;
					}
					await academicoDAL.GravarAcademico(academico);
				} catch (DbUpdateConcurrencyException) {
					if (!await AcademicoExists(academico.AcademicoId)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(academico);
		}



		// GET Details
		public async Task<IActionResult> Details(int? id) {
			return await ObterViewAcademicoId(id);
		}



		// GET e POST Delete
		public async Task<IActionResult> Delete(int? id) {
			return await ObterViewAcademicoId(id);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int? id) {
			var academico = await academicoDAL.EliminarAcademicoId((int)id);
			TempData["Message"] = "Acadêmico(a) " + academico.Nome.ToUpper() + " foi removido(a)";
			return RedirectToAction(nameof(Index));
		}
		private async Task<bool> AcademicoExists(int? id) {
			return await academicoDAL.ObterAcademicoId((int)id) != null;
		}



		// GetFoto
		public async Task<FileContentResult> GetFoto(int id) {
			Academico academico = await academicoDAL.ObterAcademicoId(id);
			if(academico != null) {
				return File(academico.Foto, academico.FotoMimeType);
            }
			return null;
        }

		// DownloadFoto
		public async Task<FileResult> DownloadFoto(int id) {
			Academico academico = await academicoDAL.ObterAcademicoId(id);
			string nomeArquivo = "Foto" + academico.AcademicoId.ToString().Trim() + ".jpg";
			FileStream fileStream = new FileStream(System.IO.Path.Combine(_env.WebRootPath, nomeArquivo), FileMode.Create, FileAccess.Write);
			fileStream.Write(academico.Foto, 0, academico.Foto.Length);
			fileStream.Close();
			IFileProvider provider = new PhysicalFileProvider(_env.WebRootPath);
			IFileInfo fileInfo = provider.GetFileInfo(nomeArquivo);
			var readStream = fileInfo.CreateReadStream();
			return File(readStream, academico.FotoMimeType, nomeArquivo);
		}

	}
}
