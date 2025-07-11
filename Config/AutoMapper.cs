using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace LivrariaApi.Config;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<AutorModel, AutorDto>().ReverseMap();
        CreateMap<AutorModel, AtualizarAutorDto>().ReverseMap();

        CreateMap<LivroModel, LivroDto>().ReverseMap();
        CreateMap<LivroModel, atualizarLivroDto>().ReverseMap();
    }
}
