using DiceStrategy;
using DiceStrategy.Factories;
using DiceStrategy.Factories.Interfaces;
using DiceStrategy.Players;
using System.Diagnostics;
using System.Linq;
using System.Text;

Console.CursorVisible = false;

var playData = new Dictionary<string, PlayerReportData>();

int gameTotal = 10_000_000;
int gameCount = 0;
int reportInterval = 5000;

IGameFactory gameFactory = new AllPlayerGameFactory();

Stopwatch stopWatch = new();

Console.WriteLine("Dicegame Strategy Simulator");
Console.WriteLine("No games played yet");

var reportTimer = new Timer((x) => ReportOnGames());

reportTimer.Change(reportInterval, reportInterval);
stopWatch.Start();
foreach (var gameTask in from i in ParallelEnumerable.Range(0, gameTotal)
                         select gameFactory.Create().PlayAsync())
{
    (PlayerBase winner, IReadOnlyCollection<PlayerBase> players) = await gameTask;
    gameCount++;

    foreach (var player in players)
    {
        if (playData.ContainsKey(player.Name))
        {
            var playerData = playData[player.Name];
            playerData.AverageDiceTotal = (playerData.AverageDiceTotal * (gameCount-1) + player.AverageDiceTotal) / gameCount;
        } 
        else
        {
            playData[player.Name] = new PlayerReportData(player.Name, player.Score);
        }

    }

    playData[winner.Name].Wins++;

}
stopWatch.Stop();

reportTimer.Dispose();

ReportOnGames();

void ReportOnGames()
{
    Console.SetCursorPosition(0, 0);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("Dicegame Strategy Simulator");
    stringBuilder.AppendLine();
    if (gameCount < gameTotal)
    {
        stringBuilder.AppendFormat("After {0:n0} games of {1:n0} played in {2}:", gameCount, gameTotal, stopWatch.Elapsed.ToString());
    } 
    else
    {
        stringBuilder.AppendFormat("After all {0:n0} games have been played in {1}:", gameCount, stopWatch.Elapsed.ToString());
    }
    stringBuilder.AppendLine();

    var playDataQuery = from p in playData
            let player = p.Value
            orderby player.Wins descending
            select p.Value ;

    foreach (var playerData in playDataQuery)
    {
        string name = playerData.Name;
        double winAmount = playerData.Wins;
        double percentage = winAmount / (double)gameCount * 100;

        stringBuilder.AppendFormat("{0} won {1:n0} times, that is {2:n}% with an average score of {3:n}", name, winAmount, percentage, playerData.AverageDiceTotal);
        stringBuilder.AppendLine();
    }
    Console.Write(stringBuilder.ToString());
    Console.ReadKey();
}