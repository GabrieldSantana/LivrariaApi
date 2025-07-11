using System.Data;
using Application.Interfaces;
using Application.Interfaces.IMainService.IMainService;
using Application.Services.Exceptions;
using Application.Services.Global;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Domain.Validators.LivroValidators;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;

namespace Application.Services;
public class LivroService : ILivroService
{
    private readonly IDbConnection _connection;
    private readonly IMapper _mapper;
    private readonly IMainService _mainService;
    private readonly ILivroRepository _repository;
    private readonly IAutorRepository _autorRepository;

    private readonly LivroValidator _validator;

    public LivroService(IDbConnection connection, IMapper mapper, IMainService mainService, ILivroRepository repository, IAutorRepository autorRepository, LivroValidator validator)
    {
        _connection = connection;
        _mapper = mapper;
        _mainService = mainService;
        _repository = repository;
        _autorRepository = autorRepository;
        _validator = validator;
    }

    public async Task<bool> AdicionarLivroAsync(LivroDto livroDto)
    {
        _connection.Open();

        try
        {
            var livro = _mapper.Map<LivroModel>(livroDto);

            await _mainService.ValidacaoAsync(livro, _validator);

            var autorExistente = await _autorRepository.BuscarAutorPorIdAsync((SqlConnection)_connection, livro.Autor_Id);

            if (autorExistente == null) throw new NotFoundException("O autor informado não foi encontrado.");

            // Busca todos os livros associados ao autor do novo livro que será inserido
            var listaLivros = await _repository.ListarLivrosPorIdAutorAsync((SqlConnection)_connection, livro.Autor_Id);

            // Verifica se há algum livro daquele autor com o mesmo título
            var nomeExistente = listaLivros
                .Any(l => string.Equals(l.Titulo, livro.Titulo, StringComparison.OrdinalIgnoreCase));

            if (nomeExistente) throw new Exception("O livro já está associado a este autor.");

            using SqlTransaction transaction = (SqlTransaction)_connection.BeginTransaction();

            var livroAdicionado = await _repository.AdicionarLivroAsync((SqlConnection)_connection, transaction, livro);

            transaction.Commit();
            return livroAdicionado;
        }
        catch (Exception) { throw; }
    }

    public async Task<bool> AtualizarLivroAsync(atualizarLivroDto livroDto)
    {
        _connection.Open();

        try
        {
            var livro = _mapper.Map<LivroModel>(livroDto);

            if (!UtilityService.ValidaMenorIgualZero(livro.Livro_Id, out string erro)) throw new Exception(erro);

            var livroExistente = await _repository.BuscarLivroPorIdAsync((SqlConnection)_connection, livro.Livro_Id);
            if (livroExistente == null) throw new NotFoundException("Livro informado não foi encontrado.");
            
            var autorExistente = await _repository.BuscarLivroPorIdAsync((SqlConnection)_connection, livro.Livro_Id);
            if (autorExistente == null) throw new NotFoundException("Autor informado não foi encontrado.");

            await _mainService.ValidacaoAsync(livro, _validator);

            using SqlTransaction transaction = (SqlTransaction)_connection.BeginTransaction();

            bool livroAtualizado = await _repository.AtualizarLivroAsync((SqlConnection)_connection, transaction, livro);

            transaction.Commit();
            return livroAtualizado;
        }
        catch (Exception) { throw; }
    }
    
    public async Task<LivroModel> BuscarLivroPorIdAsync(int id)
    {
        _connection.Open();

        try
        {
            if (!UtilityService.ValidaMenorIgualZero(id, out string erro)) throw new Exception(erro);

            var livro = await _repository.BuscarLivroPorIdAsync((SqlConnection)_connection, id);

            if (livro == null) throw new NotFoundException("Nenhum registro foi encontrado.");

            return livro;
        }
        catch (Exception) { throw; }
    }

    public async Task<bool> ExcluirLivroAsync(int id)
    {
        _connection.Open();

        try
        {
            if (!UtilityService.ValidaMenorIgualZero(id, out string erro)) throw new Exception(erro);

            var livro = await _repository.BuscarLivroPorIdAsync((SqlConnection)_connection, id);

            if (livro == null) throw new NotFoundException("Nenhum registro foi encontrado.");

            using SqlTransaction transaction = (SqlTransaction)_connection.BeginTransaction();

            bool livroExcluido = await _repository.ExcluirLivroAsync((SqlConnection)_connection, transaction, id);

            transaction.Commit();
            return livroExcluido;
        }
        catch (Exception) { throw; }
    }

    public async Task<List<LivroModel>> ListarLivrosAsync()
    {
        _connection.Open();

        try
        {
            var listaLivros = await _repository.ListarLivrosAsync((SqlConnection)_connection);

            if (listaLivros.Count() == 0) throw new NotFoundException("Nenhum registro foi encontrado.");

            return listaLivros;
        }
        catch (Exception) { throw; }
    }

    public async Task<RetornoPaginado<LivroModel>> ListarLivrosPaginadoAsync(int pagina, int qtdPagina)
    {
        _connection.Open();

        string erro = null;
        try
        {
            if (!UtilityService.ValidaMenorIgualZero(pagina, out erro)) throw new Exception(erro);
            if (!UtilityService.ValidaMenorIgualZero(qtdPagina, out erro)) throw new Exception(erro);

            var listaLivrosPaginado = await _repository.ListarLivrosPaginadoAsync((SqlConnection)_connection, pagina, qtdPagina);

            if (listaLivrosPaginado.Registros.Count() == 0) throw new NotFoundException("Nenhum registro foi encontrado.");

            return listaLivrosPaginado;
        }
        catch (Exception) { throw; }
    }
}
