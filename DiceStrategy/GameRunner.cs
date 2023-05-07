using DiceStrategy.Factories.Interfaces;
using DiceStrategy.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceStrategy.Game.Players;
using System.Diagnostics;
using DiceStrategy.Reporting;

namespace DiceStrategy
{
    public class GameRunner
    {
        private readonly IGameFactory gameFactory;
        private readonly GameResultReporter gameResultReporter;

        public GameRunner(IGameFactory gameFactory, GameResultReporter gameResultReporter)
        {
            this.gameFactory = gameFactory;
            this.gameResultReporter = gameResultReporter;
        }

        public async Task RunAsync(int gameAmount) 
        {
            var games = from i in ParallelEnumerable.Range(0, gameAmount)
                        select gameFactory.Create().PlayAsync();
            foreach (var gameTask in games)
            {
                (PlayerBase winner, IReadOnlyCollection<PlayerBase> players) = await gameTask;
                gameResultReporter.AddGame(winner, players);
            }
        }
    }
}
