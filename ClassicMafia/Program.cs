using System.Diagnostics;
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

var s = (10).SelectRange(i => (10).SelectRange(j => '_').ToArray()).ToArray();

Debug.Write($"{" ",3}");
for (var j = 0; j < 10; j++)
    Debug.Write($"{j,3}");

for (var i = 0; i < 10; i++)
{
    Debug.WriteLine("");
    Debug.Write($"{i,3}");

    for (var j = 0; j < 10; j++)
    {
        if (i == j)
            s[i][j] = '~';
        else
            if (rnd.NextDouble() > 0.3)
                s[i][j] = rnd.NextDouble() > 0.333 ? 'Ч' : 'К';


        Debug.Write($"{s[i][j],3}");
    }
}

Debug.WriteLine("");

var together = pairs.Where(p => s[p.i][p.j] == 'К' && s[p.j][p.i] == 'К').ToArray();
var split = pairs.Where(p => s[p.i][p.j] == 'Ч' && s[p.j][p.i] == 'Ч').ToArray();

var g = new Graph(together);
var teams = g.FullVisit().Select(ns => ns.Select(n => n.i).ToArray()).Where(vs=>vs.Length > 1).ToArray();
var smeared = ps.Except(teams.SelectMany(vs => vs)).ToArray();

var a = 1;




