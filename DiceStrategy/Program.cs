using DiceStrategy;
using System.Diagnostics;

var winRecord = new Dictionary<string, int>();

int gameTotal = 1_000_000;

var gameFactory = new GameFactory();

var query = from i in ParallelEnumerable.Range(0, gameTotal)
            select gameFactory.Create().PlayAsync();
Console.WriteLine("Dicegame Strategy Simulator");

Stopwatch stopWatch = new();
stopWatch.Start();
foreach (Task<PlayerBase> gameTask in query)
{
    PlayerBase winner = await gameTask;
    if (winRecord.ContainsKey(winner.Name))
        winRecord[winner.Name]++;
    else
        winRecord[winner.Name] = 1;
}
stopWatch.Stop();

Console.WriteLine("After {0} games played in {1}:", gameTotal, stopWatch.Elapsed.ToString());
foreach (var winsPerPlayer in winRecord)
{
    string name = winsPerPlayer.Key;
    double winAmount = winsPerPlayer.Value;
    int percentage = (int)Math.Round(winAmount / gameTotal * 100);

    Console.WriteLine("{0} won {1} times, that is {2}%", name, winAmount, percentage);
}
Console.ReadKey();

