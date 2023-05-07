using DiceStrategy.Game.Players;

namespace DiceStrategy.Game;
public readonly struct GameResult
{
    public PlayerBase Winner { get; }
    public IReadOnlyCollection<PlayerBase> Players { get; }

    public GameResult(PlayerBase winner, IReadOnlyCollection<PlayerBase> players)
    {
        Winner = winner;
        Players = players;
    }
}
