public class Player
{
    public required int id;
    public required string name;
    public required Role role;

    public ActColor color => role.ToColor();
}

