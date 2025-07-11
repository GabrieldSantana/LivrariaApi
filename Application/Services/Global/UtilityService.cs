namespace Application.Services.Global;
public static class UtilityService
{
    public static bool ValidaMenorIgualZero(int numero, out string erro)
    {
        erro = null;

        if (numero <= 0)
        {
            erro = "O valor inserido não podem ser menor ou igual a zero.";
            return false;
        }

        return true;
    }
}
