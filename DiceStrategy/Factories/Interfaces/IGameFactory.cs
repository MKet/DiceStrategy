using DiceStrategy.Game;

namespace DiceStrategy.Factories.Interfaces;

public interface IGameFactory
{
    DiceGame Create();
}
