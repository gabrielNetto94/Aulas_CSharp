using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Persistencia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.Api
{
    public class LivrosController : Controller
    {

        private readonly IRepository<Livro> _repo;
        public LivrosController(IRepository<Livro> repository)
        {
            _repo = repository;
        }
       
        [HttpGet]
        public ActionResult Recuperar(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return Json(model.ToModel());
        }

        [HttpPost]
        public IActionResult Incluir([FromBody] LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                var livro = model.ToLivro();//converter LivroUpload para objeto Livro
                _repo.Incluir(livro);//cria o novo livro
                var uri = Url.Action("Recuperar", new { id = livro.Id }); //busca pelo id o livro que acabou de criar 
                return Created(uri, livro); //retorna o objeto livro e uri no cabeçalho da requisição  com status 201
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Alterar([FromBody] LivroUpload model) //FromBody especifíca que está vindo do corpo da request
        {
            if (ModelState.IsValid)
            {
                var livro = model.ToLivro();
                if (model.Capa == null)
                {
                    livro.ImagemCapa = _repo.All
                        .Where(l => l.Id == livro.Id)
                        .Select(l => l.ImagemCapa)
                        .FirstOrDefault();
                }
                _repo.Alterar(livro);
                return Ok(); //200
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _repo.Excluir(model);
            return NoContent(); //203
        }

    }
}
