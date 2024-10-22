public static class RoleExtensions
{
    public static bool IsRead(this Role role) => role == Role.Commissar || role == Role.Civilian;
    public static bool IsBlack(this Role role) => role == Role.Don || role == Role.Mafia;
}