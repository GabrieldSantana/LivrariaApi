using Application.Interfaces.IMainService;
using Domain.Notificacao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotificador _notificador;

    public MainController(INotificador notificador)
    {
        _notificador = notificador;

    }

    protected bool OperacaoValida()
    {
        return !_notificador.TemNotificacao();
    }

    protected ActionResult CustomResponse(object? result = null, int? codigo = null)
    {
        if (OperacaoValida())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        if (codigo == 404)
        {
            return NotFound(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }
        else
        {
            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!ModelState.IsValid) NotificarErroModelInvalida(ModelState);
        return CustomResponse();
    }

    protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotificarErro(errorMsg);
        }
    }

    protected void NotificarErro(string mensagem)
    {
        _notificador.Handle(new Notificacao(mensagem));
    }
}
