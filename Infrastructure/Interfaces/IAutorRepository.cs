using Domain.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Interfaces;
public interface IAutorRepository
{
    Task<RetornoPaginado<AutorModel>> ListarAutoresPaginadoAsync(SqlConnection connection, int pagina, int qtdPagina);
    Task<List<AutorModel>> ListarAutoresAsync(SqlConnection connection);
    Task<AutorModel> BuscarAutorPorIdAsync(SqlConnection connection, int id);
    Task<bool> AdicionarAutorAsync(SqlConnection connection, SqlTransaction transac, AutorModel autor);
    Task<bool> AtualizarAutorAsync(SqlConnection connection, SqlTransaction transac,AutorModel autor);
    Task<bool> ExcluirAutorAsync(SqlConnection connection, SqlTransaction transac, int id);
}
