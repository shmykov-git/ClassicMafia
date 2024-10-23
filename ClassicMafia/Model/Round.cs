public class Round
{
    public required int id;
    public List<Act> acts = new();
    public Player? mafiaKill;
    public List<Player> cityKill = new();
}

