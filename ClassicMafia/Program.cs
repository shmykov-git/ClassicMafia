using System.Diagnostics;
using System.Linq;
using ClassicMafia.Extensions;
using ClassicMafia.Graphs;

var rnd = new Random(0);

var names = Enumerable.Range(1, 10).Select(i => $"P{i}").ToArray();
Role[] roles = [Role.Don, Role.Mafia, Role.Mafia, Role.Commissar, Role.Civilian, Role.Civilian, Role.Civilian, Role.Civilian, Role.Civilian, Role.Civilian];

var shaffleRoles = roles.Shaffle(67, rnd);
var players = names.Select((n, i) => new Player() { id = i, name = n, role = shaffleRoles[i] }).ToArray();
var powers = Enumerable.Range(0, 10).Select(i => Enumerable.Range(0, 10).Select(j => 1.0).ToArray()).ToArray();

var game = new Game(rnd) { players0 = players, players = players.ToList(), powers = powers };

//game.Play();

var ps = (10).SelectRange(i => i).ToArray();
var pairs = (10).SelectRange(i => (10-i-1).SelectRange(i + 1, j => (i, j))).SelectMany(v => v).ToArray();

var moves = (10).SelectRange(i => (10).SelectRange(j => '_').ToArray()).ToArray();

(10, 10).ForEach((i, j) =>
{
    if (i == j)
        moves[i][j] = '◯';
    else if (rnd.NextDouble() > 0.25)
        moves[i][j] = rnd.NextDouble() > 0.6 ? 'Ч' : 'К';
});

moves.Debug();

var together = pairs.Where(p => moves[p.i][p.j].IsRed() && moves[p.j][p.i].IsRed()).ToArray();
var split = pairs.Where(p => moves[p.i][p.j].IsBlack() && moves[p.j][p.i].IsBlack()).ToArray();

var g = new Graph(together);
var teams = g.FullVisit().Select(ns => ns.Select(n => n.i).ToHashSet()).Where(vs=>vs.Count > 1).ToArray();
var smeared = ps.Except(teams.SelectMany(vs => vs)).ToHashSet();

// построить команду для одного человека

teams.ForEach((t, iT) =>
{
    t.ForEach(a => moves[a].ForEach((ab, b) =>
    {
        if (t.Contains(b) && ab.IsBlack())
        {
            // игрок a и b не в команде t
            t.Remove(a);
            t.Remove(b);
        }
    }
    ));
}
);


var a = 1;




