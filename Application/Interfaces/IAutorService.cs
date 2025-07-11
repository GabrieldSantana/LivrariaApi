using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces;
public interface IAutorService
{
    Task<RetornoPaginado<AutorModel>>ListarAutoresPaginadoAsync(int pagina, int qtdPagina);
    Task<List<AutorModel>>ListarAutoresAsync();
    Task<AutorModel>BuscarAutorPorIdAsync(int id);
    Task<bool> AdicionarAutorAsync(AutorDto autorDto);
    Task<bool> AtualizarAutorAsync(AtualizarAutorDto autorDto);
    Task<bool> ExcluirAutorAsync(int id);
}
