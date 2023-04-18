using DiceStrategy.Players;

namespace DiceStrategy;
public class DiceGame
{
    private readonly int _goalScore = 30;
    private readonly IEnumerable<PlayerBase> _players;

    private readonly DiceSet _diceSet;

    private IEnumerable<PlayerBase> AlivePlayers => from player in _players
                                                    where player.Score > 0
                                                    select player;

    public IReadOnlyCollection<PlayerBase> Players => _players.ToList().AsReadOnly();

    public DiceGame(Random random, params PlayerBase[] players)
    {
        _diceSet = new DiceSet(random);
        _players = players.OrderBy(x => random.Next(100));
    }

    public async Task<(PlayerBase, IReadOnlyCollection<PlayerBase>)> PlayAsync() => await Task.Run(() => Play());

    public (PlayerBase, IReadOnlyCollection<PlayerBase>) Play()
    {
        int damageToNext = 0;
        while (true)
        {
            foreach (PlayerBase? player in AlivePlayers)
            {
                if (AlivePlayers.Count() == 1)
                {
                    return (AlivePlayers.First(), Players);
                }
                player.Score -= damageToNext;
                damageToNext = 0;
                if (player.Score <= 0)
                {
                    continue;
                }

                int totalDiceValue = player.Play(_diceSet).TotalDicevalue;

                damageToNext = RollNumber(totalDiceValue - _goalScore);
                //damageToNext = (totalDiceValue > _goalScore) ? totalDiceValue - _goalScore: 0;

                player.Score -= (totalDiceValue < _goalScore) ? _goalScore - totalDiceValue : 0;
            }
        }
    }

    public int RollNumber(int number) {
        if (number <= 0)
            return 0;
        _diceSet.Reset();
        while (true)
        {
            _diceSet.RollDice();
            if (!_diceSet.UnchosenDice.Any(x => x.CurrentValue != number))
                break;
            foreach (var die in _diceSet.UnchosenDice)
            {
                if (die.CurrentValue == number)
                    die.IsChosen = true;
            }
        }

        int damage = _diceSet.ChosenDice.Sum(x => x.CurrentValue);

        return (damage / number == 6) ? _goalScore : damage; 
    }
}
