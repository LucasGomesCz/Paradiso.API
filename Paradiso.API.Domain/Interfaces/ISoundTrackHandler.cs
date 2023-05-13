using Paradiso.API.Domain.Dtos;
using Paradiso.API.Domain.Entities;
using Paradiso.API.Domain.Models.Shared;
using Paradiso.API.Domain.Models.SoundTracks;

namespace Paradiso.API.Domain.Interfaces;

public interface ISoundTrackHandler
{
    Task<List<SoundTrack>> GetAsync(SoundTrackGetParams @params);

    Task<MessageDto> UploadAsync(SoundTrackPostParams @params);

    Task<MessageDto> UpdateAsync(SoundTrackPutParams @params);

    Task<MessageDto> DeleteAsync(DeleteParams @params);
}
