using DiceStrategy.Players.IncreasingLeniencyPlayers;
using DiceStrategy.Players;
using DiceStrategy.Factories.Interfaces;

namespace DiceStrategy.Factories;
public class AllPlayerGameFactory : IGameFactory
{
    private readonly Random _random = new();

    public DiceGame Create()
    {
        var random = new Random(_random.Next());
        return new DiceGame(
            random,
            new Only5OrHigherPlayerNoTotalCheck("Delta"),
            new IncreasingLeniencyPlayer("Alice"),
            new IncreasingLeniencyPlayerNoTotalCheck("Bob"),
            new StrictIncreasingLeniencyPlayer("Charlie"),
            new Only5OrHigherPlayer("Edward"),
            new OnlyMaxOrHighestPlayer("Fred"));
    }
}
