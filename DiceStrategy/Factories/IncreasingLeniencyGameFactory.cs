using DiceStrategy.Players.IncreasingLeniencyPlayers;
using DiceStrategy.Players;
using DiceStrategy.Factories.Interfaces;

namespace DiceStrategy.Factories;
public class IncreasingLeniencyGameFactory : IGameFactory
{
    private readonly Random _random = new();

    public DiceGame Create()
    {
        var random = new Random(_random.Next());
        return new DiceGame(
            random,
            new IncreasingLeniencyPlayer("Delta", true),
            new IncreasingLeniencyPlayer("Charlie", false)
            );
    }
}
