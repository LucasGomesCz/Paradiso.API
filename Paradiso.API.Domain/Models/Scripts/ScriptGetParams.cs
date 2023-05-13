using System.ComponentModel.DataAnnotations;

namespace Paradiso.API.Domain.Models.Scripts;

public class ScriptGetParams
{
    /// <summary>
    /// Id(s) do Script. Uma string de Guids separada por virgula (Ex: 'guid,guid')
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Nome(s) de Script. Uma string de nome(s) separada por virgula (Ex: 'nome,nome')
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Ano mínimo do lançamento, não passar máxima irá trazer maior ou igual a mínima
    /// </summary>
    public string? MinYear { get; set; }

    /// <summary>
    ///  Ano máximo do lançamento, não passar mínima irá trazer menor ou igual a máxima
    /// </summary>
    public string? MaxYear { get; set; }

    /// <summary>
    /// True para os scrits completos, false para os nãoa terminados
    /// </summary>
    public bool? IsComplete { get; set; }

    /// <summary>
    /// True para os que têm direitos autorais, false para os que não têm
    /// </summary>
    public bool? HasCopyright { get; set; }

    /// <summary>
    /// Gênero do contéudo
    /// </summary>
    public string? Genre { get; set; }

    /// <summary>
    /// Id(s) do usuário. Uma string de Guids separada por virgula (Ex: 'guid,guid')
    /// </summary>
    public string? User { get; set; }

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
