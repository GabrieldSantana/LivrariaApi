using Application.Interfaces.IMainService.IMainService;
using FluentValidation;

namespace Application.Services.MainService;
public class MainService : IMainService
{
    public Task ValidacaoAsync<TInputModel, TValidator>(TInputModel model, TValidator validator)
        where TInputModel : class
        where TValidator : AbstractValidator<TInputModel>
    {
        try
        {
            validator = Activator.CreateInstance<TValidator>();
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(error => new string(error.ErrorMessage));
                var errorString = string.Join(Environment.NewLine, errors);

                throw new Exception(errorString);
            }
            else
            {
                return Task.CompletedTask;
            }
        }
        catch (Exception) { throw; }
    }
}
