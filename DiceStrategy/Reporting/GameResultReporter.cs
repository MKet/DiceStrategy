using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiceStrategy.Reporting
{
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
            _reportTimer.Change(0, reportInterval);
            _stopWatch.Start();
        }

        public void AddGame(Game.Players.PlayerBase winner, IReadOnlyCollection<Game.Players.PlayerBase> players)
        {
            _gameCount++;

            foreach (var player in players)
            {
                if (playData.ContainsKey(player.Name))
                {
                    var playerData = playData[player.Name];
                    playerData.AverageDiceTotal = (playerData.AverageDiceTotal * (_gameCount - 1) + player.AverageDiceTotal) / _gameCount;
                }
                else
                {
                    playData[player.Name] = new PlayerReportData(player.Name, player.AverageDiceTotal);
                }
            }

            playData[winner.Name].Wins++;
        }

        public void PrintReport()
        {
            PrintReport(BuildReport());
        }

        protected abstract void PrintReport(string report);

        private string BuildReport()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Dicegame Strategy Simulator");

            if (_gameCount == 0)
            {
                stringBuilder.AppendLine("No games played yet.");
                return stringBuilder.ToString();
            }

            if (_gameCount < gameTotal)
            {
                stringBuilder.AppendFormat("After {0:n0} games of {1:n0} played in {2}:", _gameCount, gameTotal, _stopWatch.Elapsed.ToString());
            }
            else
            {
                stringBuilder.AppendFormat("After all {0:n0} games have been played in {1}:", _gameCount, _stopWatch.Elapsed.ToString());
            }
            stringBuilder.AppendLine();

            var playDataQuery = from p in playData
                                let player = p.Value
                                select player;

            foreach (var playerData in playDataQuery)
            {
                string name = playerData.Name;
                double winAmount = playerData.Wins;
                double percentage = winAmount / (double)_gameCount * 100;

                stringBuilder.AppendFormat("{0} won {1:n0} times, that is {2:n}% with an average score of {3:n}", name, winAmount, percentage, playerData.AverageDiceTotal);
                stringBuilder.AppendLine();
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
}
