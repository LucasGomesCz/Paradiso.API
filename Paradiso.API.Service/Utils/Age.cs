namespace Paradiso.API.Service.Utils;

public static class Age
{
    public static int Calculate(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
            age--;

        return age;
    }
}
