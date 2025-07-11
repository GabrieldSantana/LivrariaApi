namespace Domain.Dtos;
public class AutorDto
{
    public string Nome { get; set; }
    public string Nacionalidade { get; set; }
    public DateTime Data_Nascimento { get; set; }
    public char Genero { get; set; }
}
public class AtualizarAutorDto
{
    public int Autor_Id { get; set; }
    public string Nome { get; set; }
    public string Nacionalidade { get; set; }
    public DateTime Data_Nascimento { get; set; }
    public char Genero { get; set; }
}
