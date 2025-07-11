namespace Domain.Models;
public class RetornoPaginado<T> where T : class
{
    public int TotalRegistros { get; set; }
    public List<T> Registros { get; set; }
}
