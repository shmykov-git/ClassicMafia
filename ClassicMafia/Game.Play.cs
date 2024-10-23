public partial class Game
{
    private int talkShift = 0;

    private List<Act> Talk(Round round, Player player)
    {
        var acts = new List<Act>();

        // нужно дать новые позиции по игрокам

        // вспоминает все раунды, все действия, все разговоры
        // выбирает самые значимые, на их основе дает позицию

        // коммиссары и вскрытия

        // анализировать позиции текущего круга
        foreach (var pB in players)
        {
            var act = new Act()
            {
                pA = player,
                pB = pB,
                r = round.id,
                type = ActType.Talk,
                about = ActAbout.Read,
                force = 0.7,
                text = "красный игрок"
            };

            //if (HasPosition(pB))
            //{
            //    var pos = GetPosition(pB);
            //    var posOfMe = pos.acts.Where(a => a.pB == player).ToArray();

            //}
            //else
            //{

            //}

            acts.Add(act);
        }


        if (round.id == 0)
        {


        }
        else
        {
            // получить позицию стола по позиции последнего круга для текущего игрока
            // у каждого игрока есть позиция по другим игрокам, т.е. он назвал их цвета с той или иной вероятностью
        }

        return acts;
    }

    private void PlayRound(int r)
    {
        var round = new Round() { id = r };

        var startTalkPlayer = players.RoundShift(talkShift).First(IsAlive);
        var startTalkPlayerIndex = players.IndexOf(startTalkPlayer);

        foreach (var player in players.RoundShift(startTalkPlayerIndex))
        {
            var acts = Talk(round, player);
            round.acts.AddRange(acts);
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