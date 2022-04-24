using DiceStrategy.Players;

namespace DiceStrategy;
public class GameFactory
{
    private readonly Random _random = new();

    public DiceGame Create()
    {
        var random = new Random(_random.Next());
        return new DiceGame(
            random,
            new RandomPlayer("Alice", random),
            new Only5OrHigherPlayer("Bob"),
            new OnlyMaxOrHighestPlayer("Charlie"),
            new IncreasingLeniencyPlayer("Delta"),
            new Only5OrHigherPlayerNoTotalCheck("Kevin")
            );
    }
}
