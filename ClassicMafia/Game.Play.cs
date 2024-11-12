using ClassicMafia.Extensions;

public class ActArgs
{
    public ActColor myColor;
    public ActColor givenColor;
    public bool? isGoodTalk;
}

public partial class Game
{
    private int talkShift = 0;


    // нужно дать новые позиции по игрокам

    // вспоминает все раунды, все действия, все разговоры
    // выбирает самые значимые, на их основе дает позицию

    // коммиссары и вскрытия

    // анализировать позиции текущего круга

    // получить позицию стола по позиции последнего круга для текущего игрока
    // у каждого игрока есть позиция по другим игрокам, т.е. он назвал их цвета с той или иной вероятностью

    private (ActColor, double, string?) Act(ActArgs args)
    {

        return (ActColor.Red, 0.7, "красный");
    }

    private List<Act> Talk(Round round, Player pA)
    {
        Act CreateAct(Player pB, ActColor color, double force, string? text) => new Act()
        {
            pA = pA,
            pB = pB,
            r = round.id,
            type = ActType.Talk,
            color = color,
            force = force,
            text = text
        };

        var acts = new List<Act>();

        foreach (var pB in players)
        {
            if (pB == pA)
            {
                acts.Add(CreateAct(pA, ActColor.Red, 1, "я красный игрок"));
                continue;
            }

            //var act = GetAct(pB, ActColor.Red, ActWhy.Talk, 0.7, "поговорил как красный");

            // facts
            var arguments = new ActArgs
            {
                myColor = pA.color,
                isGoodTalk = HasGoodTalk(pB),
                givenColor = ActColor.None
            };

            if (HasPosition(pB))
            {
                var pos = GetPosition(pB);
                var posOfMe = pos.acts.Where(a => a.pB == pA).ToArray();

                if (posOfMe.Any())
                    arguments.givenColor = posOfMe[^1].color;
            }

            var (bColor, force, text) = Act(arguments);
            var act = CreateAct(pB, bColor, force, text);

            acts.Add(act);
        }

        return acts;
    }

    private void PlayRound(int r)
    {
        var round = new Round() { id = r };
        rounds.Add(round);

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