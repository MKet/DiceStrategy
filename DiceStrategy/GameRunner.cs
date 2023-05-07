using DiceStrategy.Factories.Interfaces;
using DiceStrategy.Reporting;

namespace DiceStrategy;

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
            (var winner, var players) = await gameTask;
            gameResultReporter.AddGameResults(winner, players);
        }
    }
}
