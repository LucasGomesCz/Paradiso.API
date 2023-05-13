using System.ComponentModel;

namespace Paradiso.API.Domain.Enums;

public enum EException : byte
{
    [Description("Success")]
    Success,

    [Description("User not found")]
    UserNotFound,

    [Description("Movie(s) not found")]
    MovieNotFound,

    [Description("Photo(s) not found")]
    PhotoNotFound,

    [Description("Script(s) not found")]
    ScriptNotFound,

    [Description("SoundTrack(s) not found")]
    SoundTrackNotFound,

    [Description("Invalid value(s), string guid expected (Ex: 'guid,guid')")]
    InvalidValue,

    [Description("Invalid value, string guid expected (Ex: 'guid')")]
    InvalidUniqueValue,

    [Description("File not selected or empty")]
    FileNotSelected,

    [Description("File already exist")]
    FileExist,

    [Description("File not found")]
    FileNotFound
}

public static class EExceptionExtension
{
    public static string DisplayName(this EException value)
    {
        var fi = value.GetType().GetField(value.ToString());

        if (fi!.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Length > 0)
            return attributes[0].Description;

        return value.ToString();
    }
}