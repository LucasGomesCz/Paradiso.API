using Paradiso.API.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Paradiso.API.Domain.Models.Users;

public class UserGetParams
{
    /// <summary>
    /// Id(s) do usuário. Uma string de Guids separada por virgula (Ex: 'guid,guid')
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Nome(s) de usuário. Uma string de nome(s) de usuário separada por virgula (Ex: 'nome,nome')
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// O gênero de usuário
    /// </summary>
    public EGender? Gender { get; set; }

    /// <summary>
    /// Idade minima de usuário, não passar idade máxima irá trazer idade maior ou igual a mínima
    /// </summary>
    [Range(16, 100, ErrorMessage = "De 16 a 100")]
    public short? MinAge { get; set; }

    /// <summary>
    /// Idade máxima de usuário, não passar idade mínima irá trazer idade menor ou igual
    /// </summary>
    [Range(16, 100, ErrorMessage = "De 16 a 100")]
    public short? MaxAge { get; set; }

    /// <summary>
    /// True para criador, false para produtor
    /// </summary>
    public bool? IsCreator { get; set; }

    /// <summary>
    /// Área(s) de atuação de usuário. Uma string de Guids separada por virgula (Ex: 'guid,guid')
    /// </summary>
    public string? Area { get; set; }

    /// <summary>
    /// Cidade. Uma string de Guids separada por virgula (Ex: 'guid,guid')
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Estado. Uma string de Guids separada por virgula (Ex: 'guid,guid')
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Quantidade de objetos na requisição
    /// </summary>
    [Range(1, 100, ErrorMessage = "Entre 1 e 100")]
    public int? Rows { get; set; }

    /// <summary>
    /// Página da requisição
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Apenas números positivos")]
    public int? Page { get; set; }
}
