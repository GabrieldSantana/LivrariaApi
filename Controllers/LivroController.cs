using Application.Interfaces;
using Application.Interfaces.IMainService;
using Application.Services.Exceptions;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivroController : MainController
{
    private readonly ILivroService _service;

    public LivroController(INotificador notificador ,ILivroService service) : base(notificador)
    {
        _service = service;
    }

    [HttpGet("{pagina}/{qtdPagina}")]
    public async Task<IActionResult> ListarLivrosPaginadoAsync(int pagina, int qtdPagina)
    {
        try
        {
            var livrosPaginado = await _service.ListarLivrosPaginadoAsync(pagina, qtdPagina);
            return CustomResponse(livrosPaginado);
        }
        catch (NotFoundException ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse(null, 404);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarLivrosAsync()
    {
        try
        {
            var listaLivros = await _service.ListarLivrosAsync();
            return CustomResponse(listaLivros);
        }
        catch (NotFoundException ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse(null, 404);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarLivroPorIdAsync(int id)
    {
        try
        {
            var livro = await _service.BuscarLivroPorIdAsync(id);
            return CustomResponse(livro);
        }
        catch (NotFoundException ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse(null, 404);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarLivroAsync([FromBody] LivroDto livroDto)
    {
        try
        {
            var resultadolivroAdicionado = await _service.AdicionarLivroAsync(livroDto);
            return CustomResponse("Livro adicionado com sucesso.");
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarLivroAsync([FromBody] atualizarLivroDto livroDto)
    {
        try
        {
            var resultadolivroAtualizado = await _service.AtualizarLivroAsync(livroDto);
            return CustomResponse("Livro atualizado com sucesso.");
        }
        catch (NotFoundException ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse(null, 404);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
            throw;
        }
    }

    [HttpDelete("{id}")] 
    public async Task<IActionResult> ExcluirLivroAsync(int id)
    {
        try
        {
            var resultadoLivroExcluido = await _service.ExcluirLivroAsync(id);
            return CustomResponse("Livro excluído com sucesso.");
        }
        catch (NotFoundException ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse(null, 404);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
}
