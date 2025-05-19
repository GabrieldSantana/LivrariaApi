using Application.Interfaces;
using Application.Services;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace LivrariaApi.Config;

public static class DependencyInjection
{
    public static IServiceCollection DependenciesInjections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAutorRepository, AutorRepository>();
        services.AddScoped<IAutorService, AutorService>();

        //services.AddScoped<ILivroRepository, LivroRepository>();
        //services.AddScoped<ILivroService, LivroService>();

        return services;
    }
}
