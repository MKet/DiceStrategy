using DiceStrategy.Factories.Interfaces;
using DiceStrategy.Game;
using DiceStrategy.Game.Players;
using DiceStrategy.Game.Players.IncreasingLeniencyPlayers;

namespace DiceStrategy.Factories;
public class AllPlayerGameFactory : IGameFactory
{
    private readonly Random _random = new();

    public DiceGame Create()
    {
        var random = new Random(_random.Next());
        return new DiceGame(
            random,
            new OnlyNOrHigherPlayer("Delta", 5, true),
            new IncreasingLeniencyPlayer("Alice", false),
            new IncreasingLeniencyPlayer("Bob", true),
            new StrictIncreasingLeniencyPlayer("Charlie", true),
            new OnlyNOrHigherPlayer("Edward", 5, false),
            new OnlyNOrHigherPlayer("Fred", 6, true));
    }
}
