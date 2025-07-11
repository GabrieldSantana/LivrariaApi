using System.Data;
using Application.Interfaces;
using Application.Interfaces.IMainService.IMainService;
using Application.Services.Exceptions;
using Application.Services.Global;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Domain.Validators.AutorValidators;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;

namespace Application.Services;
public class AutorService : IAutorService
{
    private readonly IAutorRepository _repository;
    private readonly IMainService _mainService;
    private readonly IMapper _mapper;
    private readonly IDbConnection _connection;

    private readonly AutorValidator _validator;

    public AutorService(
        IAutorRepository repository,
        IMainService mainService,
        IMapper mapper,
        IDbConnection connection,
        AutorValidator validator
        )
    {
        _repository = repository;
        _mainService = mainService;
        _connection = connection;
        _mapper = mapper;
        _validator = validator;
    }

    #region GETS
    public async Task<RetornoPaginado<AutorModel>> ListarAutoresPaginadoAsync(int pagina, int qtdPagina)
    {
        _connection.Open();

        string erro = null;

        try
        {
            if (!UtilityService.ValidaMenorIgualZero(pagina, out erro)) throw new Exception(erro);
            if (!UtilityService.ValidaMenorIgualZero(qtdPagina, out erro)) throw new Exception(erro);

            var resultado = await _repository.ListarAutoresPaginadoAsync((SqlConnection)_connection, pagina, qtdPagina);

            if (resultado.Registros.Count() == 0) throw new NotFoundException("Nenhum registros foi encontrado."); 

            return resultado;
        }
        catch (Exception) { throw; }
    }

    public async Task<List<AutorModel>> ListarAutoresAsync()
    {
        _connection.Open();

        try
        {
            var resultado = await _repository.ListarAutoresAsync((SqlConnection)_connection);

            if (resultado.Count() == 0) throw new NotFoundException("Nenhum registros foi encontrado.");

            return resultado;
        }
        catch (Exception) { throw; }
    }

    public async Task<AutorModel> BuscarAutorPorIdAsync(int id)
    {
        _connection.Open();

        string erro = null; 

        try
        {
            if (!UtilityService.ValidaMenorIgualZero(id, out erro)) throw new Exception(erro);

            var resultado = await _repository.BuscarAutorPorIdAsync((SqlConnection)_connection, id);

            if (resultado == null) throw new NotFoundException("Nenhum registro foi encontrado.");

            return resultado;
        }
        catch (Exception) { throw; }
    }

    #endregion

    #region POSTS
    public async Task<bool> AdicionarAutorAsync(AutorDto autorDto)
    {
        _connection.Open();

        try
        {
            var autor = _mapper.Map<AutorModel>(autorDto);
            await _mainService.ValidacaoAsync(autor, _validator);

            // Verificar se já existe no banco
            var autoresExistentes = await _repository.ListarAutoresAsync((SqlConnection)_connection);

            using var transaction = (SqlTransaction)_connection.BeginTransaction();

            var nomeExistente = autoresExistentes
                .Any(a => string.Equals(a.Nome, autor.Nome, StringComparison.OrdinalIgnoreCase));

            if (nomeExistente) throw new Exception($"Já existe um autor com o nome: {autor.Nome}.");

            var resultado = await _repository.AdicionarAutorAsync((SqlConnection)_connection, transaction, autor);

            transaction.Commit();
            return resultado;
        }
        catch (Exception) { throw; }
    }

    #endregion

    #region PUTS
    public async Task<bool> AtualizarAutorAsync(AtualizarAutorDto autorDto)
    {
        _connection.Open();

        try
        {
            var autor = _mapper.Map<AutorModel>(autorDto);

            if (!UtilityService.ValidaMenorIgualZero(autor.Autor_Id, out string erro)) throw new Exception(erro);

            // Verificar se o autor já existe no banco para atualização
            var autorExistente = await _repository.BuscarAutorPorIdAsync((SqlConnection)_connection, autor.Autor_Id) ?? throw new NotFoundException("Autor não encontrado.");

            var autoresExistentes = await _repository.ListarAutoresAsync((SqlConnection)_connection); // Buscando todos os autores

            // Verificar se o nome já existe no banco
            var nomeExistente = autoresExistentes
                .Any(a => a.Autor_Id != autor.Autor_Id && string.Equals(a.Nome, autor.Nome, StringComparison.OrdinalIgnoreCase));

            if (nomeExistente) throw new Exception($"Já existe um autor com o nome: {autor.Nome}.");

            await _mainService.ValidacaoAsync(autor, _validator);

            // Inicializando a transação
            using var transaction = (SqlTransaction)_connection.BeginTransaction();

            var resultado = await _repository.AtualizarAutorAsync((SqlConnection)_connection, transaction, autor);

            transaction.Commit(); // Confirmando a transação em caso de sucesso
            return resultado;

        }
        catch (Exception) { throw; }
    }

    #endregion

    #region DELETES
    public async Task<bool> ExcluirAutorAsync(int id)
    {
        _connection.Open();

        try
        {
            if (!UtilityService.ValidaMenorIgualZero(id, out string erro)) throw new Exception(erro);

            var autorExistente = await _repository.BuscarAutorPorIdAsync((SqlConnection)_connection, id) ?? throw new NotFoundException("Autor não encontrado.");

            using var transaction = (SqlTransaction)_connection.BeginTransaction();

            var resultado = await _repository.ExcluirAutorAsync((SqlConnection)_connection, transaction, id);

            transaction.Commit();
            return resultado;
        }
        catch (Exception) { throw; }
    }

    #endregion
}