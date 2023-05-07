using DiceStrategy.Factories.Interfaces;
using DiceStrategy.Game;
using DiceStrategy.Game.Players.IncreasingLeniencyPlayers;

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
