public static class RoleExtensions
{
    public static bool IsRed(this Role role) => role == Role.Commissar || role == Role.Civilian;
    public static bool IsBlack(this Role role) => role == Role.Don || role == Role.Mafia;
    public static ActColor ToColor(this Role role) => role.IsRed() ? ActColor.Red : ActColor.Black;
}
