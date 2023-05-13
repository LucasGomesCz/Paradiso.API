using Paradiso.API.Domain.Dtos;
using Paradiso.API.Domain.Entities;
using Paradiso.API.Domain.Models.Shared;
using Paradiso.API.Domain.Models.Users;

namespace Paradiso.API.Domain.Interfaces;

public interface IUserHandler
{
    Task<List<User>> GetAsync(UserGetParams @params);

    Task<MessageDto> UploadAsync(UserPostParams @params);

    Task<MessageDto> UpdateAsync(UserPutParams @params);

    Task<MessageDto> DeleteAsync(DeleteParams @params);
}
