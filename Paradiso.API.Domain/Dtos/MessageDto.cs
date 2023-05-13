using Paradiso.API.Domain.Enums;

namespace Paradiso.API.Domain.Dtos;

public class MessageDto
{
    public string Message { get; set; } = EException.Success.DisplayName();
    public DateTime OccuredAt { get; set; } = DateTime.UtcNow;
}
