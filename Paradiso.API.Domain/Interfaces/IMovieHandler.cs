using Paradiso.API.Domain.Dtos;
using Paradiso.API.Domain.Entities;
using Paradiso.API.Domain.Models.Movies;
using Paradiso.API.Domain.Models.Shared;

namespace Paradiso.API.Domain.Interfaces;

public interface IMovieHandler
{
    Task<List<Movie>> GetAsync(MovieGetParams @params);

    Task<MessageDto> UploadAsync(MoviePostParams @params);

    Task<MessageDto> UpdateAsync(MoviePutParams @params);

    Task<MessageDto> DeleteAsync(DeleteParams @params);
}
