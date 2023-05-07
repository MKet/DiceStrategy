using System.Linq;

namespace DiceStrategy.Game.Players;
public abstract class PlayerBase
{
    public int Health { get; set; } = 30;
    public string Name { get; }
    public bool WithTotalCheck { get; }
    private int _turnAmount = 0;

    public double AverageDiceTotal { get; private set; } = 0;

    protected PlayerBase(string name, bool withTotalCheck, string? typeName = null)
    {
        typeName ??= GetType().Name;
        if (withTotalCheck)
        {
            typeName += "WithTotalCheck";
        }
        Name = $"{typeName}: {name}";
        WithTotalCheck = withTotalCheck;
    }

    public DiceModel Play(DiceModel dice)
    {
        _turnAmount++;
        while (dice.UnchosenDieAmount > 0)
        {
            dice.RollDice();
            ChooseDice(dice);
        }

        if (AverageDiceTotal == 0)
        {
            AverageDiceTotal = dice.TotalDicevalue;
        }
        else
        {
            int diceValue = dice.TotalDicevalue;

            AverageDiceTotal = (AverageDiceTotal * (_turnAmount - 1) + diceValue) / _turnAmount;
        }
        return dice;
    }

    private void ChooseDice(DiceModel dice)
    {
        if (WithTotalCheck && dice.TotalDicevalue >= 30)
        {
            dice.ChooseAll();
            return;
        }

        dice.ChooseHighest();

        foreach (var die in dice.DiceResults.Where(GetChooseDiePredicate(dice)))
        {
            dice.Choose(die);
        }
    }

    protected abstract Func<int, bool> GetChooseDiePredicate(DiceModel dice);
}
