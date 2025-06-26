using Application.Interfaces;
using Application.Interfaces.IMainService;
using Application.Interfaces.IMainService.IMainService;
using Application.Services;
using Application.Services.MainService;
using Domain.Validators.AutorValidators;
using Domain.Validators.LivroValidators;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace LivrariaApi.Config;

public static class DependencyInjection
{
    public static IServiceCollection DependenciesInjections(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);

        services.AddScoped<INotificador, Notificador>();

        services.AddScoped<IMainService, MainService>();

        services.AddScoped<IAutorRepository, AutorRepository>();
        services.AddScoped<IAutorService, AutorService>();
        services.AddScoped<AutorValidator>();

        services.AddScoped<ILivroRepository, LivroRepository>();
        services.AddScoped<ILivroService, LivroService>();
        services.AddScoped<LivroValidator>();

        return services;
    }
}
