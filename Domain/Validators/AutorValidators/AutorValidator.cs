using Domain.Models;
using FluentValidation;

namespace Domain.Validators.AutorValidators;
public class AutorValidator : AbstractValidator<AutorModel>
{
    public AutorValidator()
    {
        RuleFor(x => x.Nome)
            .NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
            .MaximumLength(100).WithMessage("O campo '{PropertyName}' deve ter até {MaxLenght} caracteres.");

        RuleFor(x => x.Nacionalidade)
            .NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
            .MaximumLength(50).WithMessage("O campo '{PropertyName}' deve ter até {MaxLenght} caracteres.");

        RuleFor(p => p.Data_Nascimento)
            .NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
            .Must(BeAValidDate).WithMessage("O campo '{PropertyName} deve ser uma data válida.")
            .LessThanOrEqualTo(
                DateTime.Today
                ).WithMessage("O campo '{PropertyName}' não pode ser uma data futura.");

        RuleFor(p => p.Genero)
            .NotEmpty().WithMessage("O campo Genero é obrigatório.")
            .Must(genero => genero == 'M' || genero == 'F')
            .WithMessage("O campo Genero deve ser 'M' (Masculino) ou 'F' (Feminino).");
    }

    // Método auxiliar para verificar se a data é válida
    private bool BeAValidDate(DateTime date)
    {
        return date != default; // Garante que a data não seja o valor padrão (01/01/0001)
    }
}
