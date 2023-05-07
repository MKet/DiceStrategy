using DiceStrategy.Game.Players;

namespace DiceStrategy.Game;
public class DiceGame
{
    private readonly int _goalScore = 30;
    private readonly DiceModel _dice;
    private readonly List<PlayerBase> _players;
    private readonly List<PlayerBase> _alivePlayers;

    public IReadOnlyCollection<PlayerBase> Players => _players.AsReadOnly();

    public DiceGame(Random random, params PlayerBase[] players)
    {
        _dice = new DiceModel(random);
        _players = players.OrderBy(x => random.Next(100)).ToList();
        _alivePlayers = new List<PlayerBase>(_players);
    }

    public GameResult Play()
    {
        var damageToNext = 0;
        var i = 0;
        while (true)
        {
            var currentPlayer = _alivePlayers[i];

            currentPlayer.Health -= damageToNext;
            if (currentPlayer.Health <= 0)
            {
                _alivePlayers.RemoveAt(i);

                if (_alivePlayers.Count == 1)
                {
                    return new GameResult(currentPlayer, Players);
                }
            }

            _dice.Reset();
            var totalDiceValue = currentPlayer.Play(_dice).TotalDicevalue;

            damageToNext = RollForDamage(totalDiceValue);
            currentPlayer.Health -= totalDiceValue < _goalScore ? _goalScore - totalDiceValue : 0;
            i++;
            if (i >= _alivePlayers.Count)
            {
                i = 0;
            }
        }
    }

    public int RollForDamage(int score)
    {
        score -= _goalScore;
        if (score <= 0)
        {
            return 0;
        }

        _dice.Reset();
        while (true)
        {
            var numberRolled = false;
            _dice.RollDice();
            foreach (var die in _dice.DiceResults)
            {
                if (die == score)
                {
                    _dice.Choose(die);
                    numberRolled = true;
                }
            }
            if (!numberRolled)
            {
                var damage = _dice.TotalDicevalue;
                return damage / score == 6 ? _goalScore : damage;
            }
        }
    }
}
