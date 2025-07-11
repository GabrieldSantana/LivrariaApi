using FluentValidation;

namespace Application.Interfaces.IMainService.IMainService;
public interface IMainService
{
    // Substituir o Task por void em caso de erro
    Task ValidacaoAsync<TInputModel, TValidator>(TInputModel model, TValidator validator)
        where TInputModel : class
        where TValidator : AbstractValidator<TInputModel>;
}