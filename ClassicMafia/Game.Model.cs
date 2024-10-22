public partial class Game
{
    public Game(Random rnd)
    {
        this.rnd = rnd;
    }

    public required Player[] players0;
    public required List<Player> players;
    public required double[][] powers; // what players think about each other before the game
    public List<Round> rounds = new();
    private readonly Random rnd;

    public bool IsPlayerAlive(Player player) => players.Contains(player);
    public bool IsOver() => players.Count(p => p.role.IsBlack()) >= players.Count(p => p.role.IsRead());
}
