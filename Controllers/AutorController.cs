using Application.Interfaces;
using Application.Interfaces.IMainService;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutorController : MainController
{
    private readonly IAutorService _service;

    public AutorController(INotificador notificador, IAutorService service) : base(notificador)
	{
        _service = service;
	}

    [HttpGet("{pagina}/{qtdPagina}")] 
    public async Task<IActionResult> ListarAutoresPaginadoAsync(int pagina, int qtdPagina)
    {
        try
        {
            var resultado = await _service.ListarAutoresPaginadoAsync(pagina, qtdPagina);
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarAutoresAsync()
    {
        try
        {
            var resultado = await _service.ListarAutoresAsync();
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> BuscarAutorPorIdAsync(int id)
    {
        try
        {
            var resultado = await _service.BuscarAutorPorIdAsync(id);
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarAutorAsync([FromBody] AutorDto autorDto)
    {
        try
        {
            var resultado = await _service.AdicioanrAutorAsync(autorDto);
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpPut]
    public async Task<IActionResult> AtualizarAutorAsync([FromBody] AtualizarAutorDto autorDto)
    {
        try
        {
            var resultado = await _service.AtualizarAutorAsync(autorDto);
            return CustomResponse(resultado);
        }
        catch(Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpDelete("/{id}")]
    public async Task<IActionResult> ExcluirAutorAsync(int id)
    {
        try
        {
            var resultado = await _service.ExcluirAutorAsync(id);
            return CustomResponse(resultado);
        }
        catch(Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
}
