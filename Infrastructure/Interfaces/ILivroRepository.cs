using System.Data;
using Domain.Dtos;
using Domain.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Interfaces;
public interface ILivroRepository
{
    Task<RetornoPaginado<LivroModel>> ListarLivrosPaginadoAsync(SqlConnection connection, int pagina, int qtdPagina);
    Task<List<LivroModel>> ListarLivrosAsync(SqlConnection connection);
    Task<LivroModel> BuscarLivroPorIdAsync(SqlConnection connection, int id);
    Task<List<LivroModel>> ListarLivrosPorIdAutorAsync(SqlConnection connection, int id);
    Task<bool> AdicionarLivroAsync(SqlConnection connection, SqlTransaction transac, LivroModel livro);
    Task<bool> AtualizarLivroAsync(SqlConnection connection, SqlTransaction transac, LivroModel livro);
    Task<bool> ExcluirLivroAsync(SqlConnection connection, SqlTransaction transac, int id);
}
