using Paradiso.API.Domain.Dtos;
using Paradiso.API.Domain.Entities;
using Paradiso.API.Domain.Models.Photos;
using Paradiso.API.Domain.Models.Shared;

namespace Paradiso.API.Domain.Interfaces;

public interface IPhotoHandler
{
    Task<List<Photo>> GetAsync(PhotoGetParams @params);

    Task<MessageDto> UploadAsync(PhotoPostParams @params);

    Task<MessageDto> UpdateAsync(PhotoPutParams @params);

    Task<MessageDto> DeleteAsync(DeleteParams @params);
}
