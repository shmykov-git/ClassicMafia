using ClassicMafia.Extensions;

var rnd = new Random(0);

var names = Enumerable.Range(1, 10).Select(i => $"P{i}").ToArray();
Role[] roles = [Role.Don, Role.Mafia, Role.Mafia, Role.Commissar, Role.Civilian, Role.Civilian, Role.Civilian, Role.Civilian, Role.Civilian, Role.Civilian];

var shaffleRoles = roles.Shaffle(67, rnd);
var players = names.Select((n, i) => new Player() {id = i, name = n, role = shaffleRoles[i] }).ToArray();
var powers = Enumerable.Range(0, 10).Select(i => Enumerable.Range(0, 10).Select(j => 1.0).ToArray()).ToArray();

var game = new Game(rnd) { players0 = players, players = players.ToList(), powers = powers };

//game.Play();







