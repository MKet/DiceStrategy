namespace DiceStrategy.Players;
public abstract class PlayerBase
{
    public int Score { get; set; } = 30;
    public string Name { get; }

    private int _turnAmount = 0;

    public double AverageDiceTotal { get; private set; } = 0;

    protected PlayerBase(string name) => Name = name;

    public DiceSet Play(DiceSet set)
    {
        set.Reset();
        _turnAmount++;
        while (true)
        {
            set.RollDice();
            ChooseDice(set);

            if (!set.UnchosenDice.Any())
            {
                break;
            }
        }

        if (AverageDiceTotal == 0)
        {
            AverageDiceTotal = set.TotalDicevalue;
        }
        else
        {
            int diceValue = set.TotalDicevalue;

            AverageDiceTotal = (AverageDiceTotal * (_turnAmount-1) + diceValue) / _turnAmount;
        }
        return set;
    }

    public abstract void ChooseDice(DiceSet set);
}
