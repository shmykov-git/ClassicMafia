public class Round
{
    public required int id;
    public List<List<Stand>> stands = new();
    public Player? mafiaKill;
    public List<Player> cityKill = new();
}

