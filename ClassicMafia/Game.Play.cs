public partial class Game
{
    private int talkShift = 0;

    private List<Stand> Talk(Round round, Player player)
    {
        var stands = new List<Stand>();
        // the player has all prev rounds with all round stands
        // нужно дать новые позиции по игрокам

        // вспоминает все раунды, все действия, все разговоры
        // выбирает самые значимые, на их основе дает позицию


        return stands;
    }

    private void PlayRound(int r)
    {
        var round = new Round() { id = r };

        var startTalkPlayer = players.RoundShift(talkShift).First(IsPlayerAlive);
        var startTalkPlayerIndex = players.IndexOf(startTalkPlayer);

        foreach (var player in players.RoundShift(startTalkPlayerIndex))
        {
            var stands = Talk(round, player);
            round.stands.Add(stands);
        }

        talkShift = (startTalkPlayer.id + 1) % 10;
    }

    public void Play()
    {
        var round = 0;

        while (!IsOver())
            PlayRound(round++);
    }
}