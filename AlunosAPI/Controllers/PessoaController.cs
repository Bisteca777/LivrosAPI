using AlunosAPI.Models;
using AlunosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MySqlX.XDevAPI;
using Swashbuckle.AspNetCore.Annotations;

namespace AlunosAPI.Controllers
{
    [Route("api/pessoas")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaRepository _pessoaRepository;

        public PessoaController(PessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        // GET: api/<PessoaController>
        [HttpGet]
        [Route("listar")]
        [SwaggerOperation(Summary = "Listar todas as pessoas", Description = "Este endpoint retorna um listagem de pessoas cadastradas.")]
        public async Task<IEnumerable<Pessoa>> Listar([FromQuery] bool? ativo = null)
        {
            return await _pessoaRepository.ListarTodasPessoas(ativo);
        }

        // GET api/<PessoaController>/5
        [HttpGet("detalhes/{id}")]
        [SwaggerOperation(
            Summary = "Obtém dados de uma pessoa pelo ID",
            Description = "Este endpoint retorna todos os dados de uma pessoa cadastrada filtrando pelo ID.")]
        public async Task<Pessoa> BuscarPorId(int id)
        {
            return await _pessoaRepository.BuscarPorId(id);
        }

        // POST api/<PessoaController>
        [HttpPost]
        public async void Post([FromBody] Pessoa dados)
        {
            await _pessoaRepository.Salvar(dados);
        }

        // PUT api/<PessoaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pessoa dados)
        {
            dados.Id = id;
            await _pessoaRepository.Atualizar(dados);
            return Ok();
        }

        // DELETE api/<PessoaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pessoaRepository.DeletarPorId(id);
            return Ok();
        }
    }

}
