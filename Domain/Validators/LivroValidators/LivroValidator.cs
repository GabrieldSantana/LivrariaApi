using Domain.Models;
using FluentValidation;

namespace Domain.Validators.LivroValidators;
public class LivroValidator : AbstractValidator<LivroModel>
{
    public LivroValidator()
    {
        RuleFor(x => x.Titulo)
            .NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório.")
            .MaximumLength(100).WithMessage("O campo '{PropertyName}' deve ter até {MaxLenght} caracteres.");

        RuleFor(x => x.Preco)
            .GreaterThan(0).WithMessage("O campo Preço deve ser maior que 0");

        RuleFor(x => x.Qtd_Estoque)
            .GreaterThan(0).WithMessage("O campo Quantidade Estoque deve ser maior que 0");

        RuleFor(p => p.Genero)
            .IsInEnum().WithMessage("O valor inserido deve ser válido.")
            .NotEmpty().WithMessage("O campo Genero é obrigatório.");
    }
}
