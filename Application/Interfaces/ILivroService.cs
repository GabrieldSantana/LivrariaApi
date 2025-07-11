using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces;
public interface ILivroService
{
    Task<RetornoPaginado<LivroModel>> ListarLivrosPaginadoAsync(int pagina, int qtdPagina);
    Task<List<LivroModel>> ListarLivrosAsync();
    Task<LivroModel> BuscarLivroPorIdAsync(int id);
    Task<bool> AdicionarLivroAsync(LivroDto livroDto);
    Task<bool> AtualizarLivroAsync(atualizarLivroDto livroDto);
    Task<bool> ExcluirLivroAsync(int id);
}