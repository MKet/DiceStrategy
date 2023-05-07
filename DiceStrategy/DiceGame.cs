using DiceStrategy.Players;

namespace DiceStrategy;
public class DiceGame
{
    private readonly int _goalScore = 30;

    private readonly DiceModel dice;

    private IEnumerable<PlayerBase> AlivePlayers => from player in Players
                                                    where player.Score > 0
                                                    select player;

    public IReadOnlyCollection<PlayerBase> Players { get; }

    public DiceGame(Random random, params PlayerBase[] players)
    {
        dice = new DiceModel(random);
        Players = players.OrderBy(x => random.Next(100)).ToList().AsReadOnly();
    }

    public Task<(PlayerBase, IReadOnlyCollection<PlayerBase>)> PlayAsync() => Task.FromResult(Play());

    public (PlayerBase, IReadOnlyCollection<PlayerBase>) Play()
    {
        int damageToNext = 0;
        while (true)
        {
            foreach (PlayerBase? player in AlivePlayers)
            {
                var alivePlayer = AlivePlayers.ToArray();
                if (alivePlayer.Length == 1)
                {
                    return (alivePlayer[0], Players);
                }
                player.Score -= damageToNext;
                damageToNext = 0;
                if (player.Score <= 0)
                {
                    continue;
                }

                dice.Reset();
                int totalDiceValue = player.Play(dice).TotalDicevalue;

                damageToNext = RollNumber(totalDiceValue - _goalScore);

                player.Score -= (totalDiceValue < _goalScore) ? _goalScore - totalDiceValue : 0;
            }
        }
    }

    public int RollNumber(int number) {
        if (number <= 0)
            return 0;
        dice.Reset();
        while (true)
        {
            bool numberRolled = false;
            dice.RollDice();
            foreach (var die in dice.DiceResults)
            {
                if (die == number)
                {
                    dice.Choose(die);
                    numberRolled = true;
                }
            }
            if (!numberRolled)
            {
                int damage = dice.TotalDicevalue;
                return (damage / number == 6) ? _goalScore : damage;
            }
        }
    }
}
