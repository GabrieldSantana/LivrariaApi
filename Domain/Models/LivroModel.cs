
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Models;
public class LivroModel
{
    public int Livro_Id { get; set; }
    public string Titulo { get; set; }
    public int Autor_Id { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public GeneroEnum Genero { get; set; }

    public float Preco { get; set; }
    public int Qtd_Estoque { get; set; }
}