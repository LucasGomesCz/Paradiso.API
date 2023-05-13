using Paradiso.API.Domain.Dtos;
using Paradiso.API.Domain.Entities;
using Paradiso.API.Domain.Models.Scripts;
using Paradiso.API.Domain.Models.Shared;

namespace Paradiso.API.Domain.Interfaces;

public interface IScriptHandler
{
    Task<List<Script>> GetAsync(ScriptGetParams @params);

    Task<MessageDto> UploadAsync(ScriptPostParams @params);

    Task<MessageDto> UpdateAsync(ScriptPutParams @params);

    Task<MessageDto> DeleteAsync(DeleteParams @params);
}
