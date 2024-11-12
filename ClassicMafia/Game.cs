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

    public bool HasPosition(Player player) => rounds.SelectMany(r => r.acts).Any(act => act.pA == player);
    public (Player p, Act[] acts) GetPosition(Player player) =>
        rounds.SelectMany(r => r.acts)
        .Where(act => act.pA == player)
        .GroupBy(act => act.pB)
        .Select(ga => (p: ga.Key, a: ga.OrderByDescending(v => v.count).ToArray()))
        .First();

    // position contrast
    // аргументированная контрастная позиция - хорошо

    public bool IsAlive(Player player) => players.Contains(player);
    public bool IsOver() => players.Count(p => p.role.IsBlack()) >= players.Count(p => p.role.IsRed());

    public bool HasGoodTalk(Player p) => rnd.NextDouble() > 0.5;
}
