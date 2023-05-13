using Paradiso.API.Domain.Enums;

namespace Paradiso.API.Domain.Models.Users;

public class UserPostParams
{
    public string Name { get; set; }
    public EGender Gender { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public bool IsCreator { get; set; }
    public string? Telephone { get; set; }
    public string? Description { get; set; }


    public Guid AreaId { get; set; }
    public Guid CityId { get; set; }
}
