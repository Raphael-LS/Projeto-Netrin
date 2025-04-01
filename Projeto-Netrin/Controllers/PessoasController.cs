using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Netrin.DTO;
using Projeto_Netrin.Models;
using Projeto_Netrin.ViewModels;

namespace Projeto_Netrin.Controllers
{
    public class PessoasController : Controller
    {
        private readonly PessoaContext _context;

        public PessoasController(PessoaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pessoas = await _context.Pessoas.ToListAsync();
            var pessoasViewModel = new List<PessoaViewModel>();
            foreach (var pessoa in pessoas)
            {
                pessoasViewModel.Add(new PessoaViewModel { Id = pessoa.Id, Nome = pessoa.Nome, CPF = Convert.ToUInt64(pessoa.CPF).ToString(@"000\.000\.000\-00"), Idade = pessoa.Idade });
            }

            return View(pessoasViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Idade,CPF")] PessoaDTO pessoaDto)
        {
            if (ModelState.IsValid)
            {
             var pessoa = new Pessoa(pessoaDto.Nome, pessoaDto.Idade, pessoaDto.CPF);
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoaDto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(new PessoaDTO { Id = pessoa.Id, CPF = pessoa.CPF.ToString(@"000\.000\.000\-00"), Idade = pessoa.Idade, Nome = pessoa.Nome});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Idade,CPF")] PessoaDTO pessoaDto)
        {
            if (id != pessoaDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pessoa = await _context.Pessoas.FindAsync(id);
                    if (pessoa == null)
                        return NotFound();

                    pessoa.Nome = pessoaDto.Nome;
                    pessoa.Idade = pessoaDto.Idade;
                    pessoa.CPF = long.Parse(pessoaDto.CPF.Trim().Replace(".", "").Replace("-", ""));

                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoaDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoaDto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
