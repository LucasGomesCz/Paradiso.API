namespace Paradiso.API.Domain.Dtos;

public class ExceptionDto : Exception
{
    public string Message { get; set; }
    public DateTime OccuredAt { get; set; }
}
