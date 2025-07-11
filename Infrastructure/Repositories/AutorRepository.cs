using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories;
public class AutorRepository : IAutorRepository
{
    public async Task<RetornoPaginado<AutorModel>> ListarAutoresPaginadoAsync(SqlConnection connection, int pagina, int qtdPagina)
    {
        try
        {
            var sql = @"SELECT * FROM AUTORES 
                    ORDER BY AUTOR_ID
                    OFFSET @OFFSET ROWS 
                    FETCH NEXT @FETCHNEXT ROWS ONLY";

            var parametros = new
            {
                OFFSET = (pagina - 1) * qtdPagina,
                FETCHNEXT = qtdPagina
            };

            var autoresPaginados = await connection.QueryAsync<AutorModel>(sql, parametros);

            var sqlContagem = "SELECT COUNT(*) FROM AUTORES";

            var totalAutores = await connection.QueryFirstOrDefaultAsync<int>(sqlContagem);

            return new RetornoPaginado<AutorModel>
            {
                TotalRegistros = totalAutores,
                Registros = autoresPaginados.ToList()
            };
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível realizar busca dos autores.");
        }
    }
    public async Task<List<AutorModel>> ListarAutoresAsync(SqlConnection connection)
    {
        try
        {
            var sql = "SELECT * FROM AUTORES";

            var autores = await connection.QueryAsync<AutorModel>(sql);

            return autores.ToList();
        }
        catch (Exception) 
        { 
            throw new Exception("Não foi possível realizar busca dos autores."); 
        }
    }
    public async Task<AutorModel> BuscarAutorPorIdAsync(SqlConnection connection, int id)
    {
        try
        {
            string sql = $"SELECT * FROM AUTORES WHERE AUTOR_ID = {id}";

            var autor = await connection.QueryFirstOrDefaultAsync<AutorModel>(sql);

            return autor;
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível realizar busca do autor.");
        }
    }
    public async Task<bool> AdicionarAutorAsync(SqlConnection connection, SqlTransaction transac, AutorModel autor)
    {
        try
        {
            string sql = $"INSERT INTO AUTORES VALUES (@NOME, @NACIONALIDADE, @DATA_NASCIMENTO, @GENERO)";

            var parametros = new
            {
                NOME = autor.Nome,
                NACIONALIDADE = autor.Nacionalidade,
                DATA_NASCIMENTO = autor.Data_Nascimento,
                GENERO = autor.Genero
            };

            var autorInserido = await connection.ExecuteAsync(sql, parametros, transac);

            return autorInserido == 1 ? true : throw new Exception("");
        }
        catch (Exception)
        {
            throw new Exception("Não foi pssível inserir autor.");
        }
    }
    public async Task<bool> AtualizarAutorAsync(SqlConnection connection, SqlTransaction transac, AutorModel autor)
    {
        try
        {
            string sql = $"UPDATE AUTORES SET NOME = @NOME, NACIONALIDADE = @NACIONALIDADE, DATA_NASCIMENTO = @DATA_NASCIMENTO, GENERO = @GENERO WHERE AUTOR_ID = @ID";

            var parametros = new
            {
                ID = autor.Autor_Id,
                NOME = autor.Nome,
                NACIONALIDADE = autor.Nacionalidade,
                DATA_NASCIMENTO = autor.Data_Nascimento,
                GENERO = autor.Genero
            };

            var resultadoAutorAtualizado = await connection.ExecuteAsync(sql, parametros, transac);

            return resultadoAutorAtualizado == 1 ? true : throw new Exception("");
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível atualizar o autor.");
        }
    }
    public async Task<bool> ExcluirAutorAsync(SqlConnection connection, SqlTransaction transac, int id)
    {
        try
        {
            string sql = $"DELETE FROM AUTORES WHERE AUTOR_ID = @ID";

            var parametros = new
            {
                ID = id,
            };

            var resultadoAutorExcluído = await connection.ExecuteAsync(sql, parametros, transac);

            return resultadoAutorExcluído == 1 ? true : throw new Exception("");
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível excluir autor.");
        }
    }
}
