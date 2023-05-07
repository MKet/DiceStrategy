using DiceStrategy.Game;
using System.Diagnostics;
using System.Text;

namespace DiceStrategy.Reporting;

public abstract class GameResultReporter
{
    private readonly Stopwatch _stopWatch;
    private readonly Timer _reportTimer;
    private readonly int reportInterval;

    private int _gameCount = 0;
    private readonly int gameTotal;

    private readonly Dictionary<string, PlayerReportData> playData;

    public GameResultReporter(int reportInterval, int gameTotal)
    {
        playData = new Dictionary<string, PlayerReportData>();
        _stopWatch = new Stopwatch();
        _reportTimer = new Timer((x) => PrintReport());
        this.reportInterval = reportInterval;
        this.gameTotal = gameTotal;
    }
    public void Start()
    {
        _ = _reportTimer.Change(0, reportInterval);
        _stopWatch.Start();
    }

    public void AddGameResults(GameResult gameResult)
    {
        _gameCount++;

        foreach (var player in gameResult.Players)
        {
            if (playData.ContainsKey(player.Name))
            {
                var playerData = playData[player.Name];
                playerData.AverageDiceTotal = ((playerData.AverageDiceTotal * (_gameCount - 1)) + player.AverageDiceTotal) / _gameCount;
            }
            else
            {
                playData[player.Name] = new PlayerReportData(player);
            }
        }

        playData[gameResult.Winner.Name].Wins++;
    }

    public void PrintReport()
    {
        PrintReport(BuildReport());
    }

    protected abstract void PrintReport(string report);

    private string BuildReport()
    {
        var stringBuilder = new StringBuilder();
        _ = stringBuilder.AppendLine("Dicegame Strategy Simulator");

        if (_gameCount == 0)
        {
            _ = stringBuilder.AppendLine("No games played yet.");
            return stringBuilder.ToString();
        }

        _ = _gameCount < gameTotal
            ? stringBuilder.AppendFormat("After {0:n0} games of {1:n0} played in {2}:", _gameCount, gameTotal, _stopWatch.Elapsed.ToString())
            : stringBuilder.AppendFormat("After all {0:n0} games have been played in {1}:", _gameCount, _stopWatch.Elapsed.ToString());
        _ = stringBuilder.AppendLine();

        var playDataQuery = from p in playData
                            let player = p.Value
                            select player;

        foreach (var playerData in playDataQuery)
        {
            var name = playerData.Name;
            double winAmount = playerData.Wins;
            var percentage = winAmount / _gameCount * 100;

            _ = stringBuilder.AppendFormat("{0} won {1:n0} times, that is {2:n}% with an average score of {3:n}", name, winAmount, percentage, playerData.AverageDiceTotal);
            _ = stringBuilder.AppendLine();
        }
        return stringBuilder.ToString();
    }

    public void Stop()
    {
        _stopWatch.Stop();
        _reportTimer.Dispose();
        PrintReport();
    }
}
