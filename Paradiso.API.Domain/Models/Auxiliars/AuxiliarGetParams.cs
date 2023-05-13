namespace Paradiso.API.Domain.Models.Auxiliars;

public class AuxiliarGetParams
{
    public class AreaGetParams
    {
        /// <summary>
        /// Id(s) de Area. Uma string de Guids separada por virgula (Ex: 'guid,guid')
        /// </summary>
        public string? Area { get; set; }
    }

    public class CityGetParams
    {
        /// <summary>
        /// Id(s) de Cidade. Uma string de Guids separada por virgula (Ex: 'guid,guid')
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Id(s) do Estado. Uma string de Guids separada por virgula (Ex: 'guid,guid')
        /// </summary>
        public string? State { get; set; }
    }

    public class GenreGetParams
    {
        /// <summary>
        /// Id(s) do Gênero. Uma string de Guids separada por virgula (Ex: 'guid,guid')
        /// </summary>
        public string? Genre { get; set; }
    }

    public class KindMovieGetParams
    {
        /// <summary>
        /// Id(s) do Tipo de Filme. Uma string de Guids separada por virgula (Ex: 'guid,guid')
        /// </summary>
        public string? KindMovie { get; set; }
    }

    public class StateGetParams
    {
        /// <summary>
        /// Id(s) do Estado. Uma string de Guids separada por virgula (Ex: 'guid,guid')
        /// </summary>
        public string? State { get; set; }
    }
}
