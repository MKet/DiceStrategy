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

    public void Run(int gameAmount)
    {
        var games = ParallelEnumerable.Range(0, gameAmount)
                    .Select((i) => gameFactory.Create().Play());
        foreach (var gameResult in games)
        {
            gameResultReporter.AddGameResults(gameResult);
        }
    }
}
