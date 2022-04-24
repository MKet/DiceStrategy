namespace DiceStrategy;
public class DiceGame
{
    private readonly PlayerBase[] _players;

    private readonly DiceSet _diceSet;

    private IEnumerable<PlayerBase> AlivePlayers => from player in _players
                                                    where player.Score > 0
                                                    select player;

    public DiceGame(Random random, params PlayerBase[] players)
    {
        _diceSet = new DiceSet(random);
        _players = players;
    }

    public async Task<PlayerBase> PlayAsync() => await Task.Run(() => Play());

    public PlayerBase Play()
    {
        int damageToNext = 0;
        while (true)
        {
            foreach (PlayerBase? player in AlivePlayers)
            {
                if (AlivePlayers.Count() is 1)
                {
                    return AlivePlayers.First();
                }
                player.Score -= damageToNext;
                damageToNext = 0;
                if (player.Score <= 0)
                {
                    continue;
                }

                int totalDiceValue = player.Play(_diceSet).TotalDicevalue;

                damageToNext = (totalDiceValue > 30) ? totalDiceValue - 30 : 0;

                player.Score -= (totalDiceValue < 30) ? 30 - totalDiceValue : 0;
            }
        }
    }
}
