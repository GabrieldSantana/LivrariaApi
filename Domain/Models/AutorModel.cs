namespace Domain.Models;
public class AutorModel
{
    public int Autor_Id { get; set; }
    public string Nome {  get; set; }
    public string Nacionalidade { get; set; }
    public DateTime Data_Nascimento { get; set; }
    public char Genero { get; set; }
}
