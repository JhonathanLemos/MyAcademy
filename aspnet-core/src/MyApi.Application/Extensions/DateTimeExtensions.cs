using MyApi.Authorization.Users;
using System;

public static class DateTimeExtensions
{
    public static bool ConvertToDateOnly(this User user, DateTime? date)
    {
        return user.CreationTime.Year == date.Value.Year && user.CreationTime.Month == date.Value.Month && user.CreationTime.Day == date.Value.Day;
    }
}
