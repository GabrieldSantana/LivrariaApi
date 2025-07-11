using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories;
public class LivroRepository : ILivroRepository
{
    public async Task<bool> AdicionarLivroAsync(SqlConnection connection, SqlTransaction transac, LivroModel livro)
    {
        try
        {
            string sql = "INSERT INTO LIVROS VALUES (@TITULO, @AUTOR_ID, @GENERO, @PRECO, @QTD_ESTOQUE)";

            var parametros = new
            {
                TITULO = livro.Titulo,
                AUTOR_ID = livro.Autor_Id,
                GENERO = livro.Genero,
                PRECO = livro.Preco,
                QTD_ESTOQUE = livro.Qtd_Estoque
            };

            var resultado = await connection.ExecuteAsync(sql, parametros, transac);

            //return resultado == 1 ? "Livro adicionado com sucesso" : throw new Exception("");
            return resultado == 1 ? true : false;
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível adicionar livro.");
        }
    }

    public async Task<bool> AtualizarLivroAsync(SqlConnection connection, SqlTransaction transac, LivroModel livro)
    {
        try
        {
            string sql = "UPDATE LIVROS SET TITULO = @TITULO, AUTOR_ID = @AUTOR_ID, GENERO = @GENERO, PRECO = @PRECO, QTD_ESTOQUE = @QTD_ESTOQUE WHERE LIVRO_ID = @ID";

            var parametros = new
            {
                TITULO = livro.Titulo,
                AUTOR_ID = livro.Autor_Id,
                GENERO = livro.Genero,
                PRECO = livro.Preco,
                QTD_ESTOQUE = livro.Qtd_Estoque,
                ID = livro.Livro_Id
            };

            var livroAtualizado = await connection.ExecuteAsync(sql, parametros, transac);

            //return livroAtualizado == 1 ? "Livro atualizado com sucesso." : throw new Exception("");
            return livroAtualizado >= 1 ? true : false;

        }
        catch (Exception)
        {
            throw new Exception("Não foi possível atualizar o livro.");
        }
    }

    public async Task<LivroModel> BuscarLivroPorIdAsync(SqlConnection connection, int id)
    {
        try
        {
            string sql = "SELECT * FROM LIVROS WHERE LIVRO_ID = @ID";

            var parametros = new
            {
                ID = id,
            };

            var livro = await connection.QueryFirstOrDefaultAsync<LivroModel>(sql, parametros);

            return livro;
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível realizar busca de livro.");
        }
    }

    public async Task<bool> ExcluirLivroAsync(SqlConnection connection, SqlTransaction transac, int id)
    {
        try
        {
            string sql = $"DELETE FROM LIVROS WHERE AUTOR_ID = @ID";

            var parametros = new
            {
                ID = id,
            };

            var livroExcluido = await connection.ExecuteAsync(sql, parametros, transac);

            //return livroExcluido == 1 ? "Livro excluído com sucesso." : throw new Exception("");
            return livroExcluido >= 1 ? true : false;

        }
        catch (Exception)
        {
            throw new Exception("Não foi possível excluir livro.");
        }
    }

    public async Task<List<LivroModel>> ListarLivrosAsync(SqlConnection connection)
    {
        try
        {
            string sql = "SELECT * FROM LIVROS";

            var listaLivros = await connection.QueryAsync<LivroModel>(sql);

            return listaLivros.ToList();
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível realizar busca de livros.");
        }
    }

    public async Task<RetornoPaginado<LivroModel>> ListarLivrosPaginadoAsync(SqlConnection connection, int pagina, int qtdPagina)
    {
        try
        {
            var sql = @"SELECT * FROM LIVROS 
                    ORDER BY LIVRO_ID
                    OFFSET @OFFSET ROWS 
                    FETCH NEXT @FETCHNEXT ROWS ONLY";

            var parametros = new
            {
                OFFSET = (pagina - 1) * qtdPagina,
                FETCHNEXT = qtdPagina
            };

            var listaPaginada = await connection.QueryAsync<LivroModel>(sql, parametros);

            string sqlContagem = "SELECT COUNT(*) FROM LIVROS";

            var totalLivros = await connection.QueryFirstOrDefaultAsync<int>(sqlContagem);

            return new RetornoPaginado<LivroModel>
            {
                TotalRegistros = totalLivros,
                Registros = listaPaginada.ToList()
            };
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível realizar busca dos livros.");
        }
    }

    public async Task<List<LivroModel>> ListarLivrosPorIdAutorAsync(SqlConnection connection, int id)
    {
        try
        {
            var sql = $"SELECT * FROM LIVROS WHERE AUTOR_ID = {id}";

            var listarLivros = await connection.QueryAsync<LivroModel>(sql);

            return listarLivros.ToList();
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível realizar busca de livros baseado no autor.");
        }
    }
}
