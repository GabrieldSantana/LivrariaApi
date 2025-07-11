namespace Domain.Dtos;
public class LivroDto
{
    public string Titulo { get; set; }
    public int Autor_Id { get; set; }
    public string Genero { get; set; }
    public float Preco { get; set; }
    public int Qtd_Estoque { get; set; }
}public class atualizarLivroDto
{
    public int Livro_Id { get; set; }
    public string Titulo { get; set; }
    public int Autor_Id { get; set; }
    public string Genero { get; set; }
    public float Preco { get; set; }
    public int Qtd_Estoque { get; set; }
}
