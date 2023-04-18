using System.Collections.ObjectModel;

namespace DiceStrategy;
public class DiceSet
{
    public IReadOnlyCollection<Die> Dice { get; }
    public IEnumerable<Die> UnchosenDice => from die in Dice
                                           where die.IsChosen == false
                                           select die;

    public IEnumerable<Die> ChosenDice => from die in Dice
                                         where die.IsChosen == true
                                         select die;

    public int TotalDicevalue => Dice.Aggregate(0, (a, b) => a + b.CurrentValue);

    public DiceSet(Random random, int dieAmount = 6, int DiceSize = 6)
    {
        var dice = new List<Die>(dieAmount);
        for (int i = 0; i < dieAmount; i++)
        {
            dice.Add(new Die(random, DiceSize));
        }
        Dice = new ReadOnlyCollection<Die>(dice);
    }

    public void Reset()
    {
        foreach (Die die in Dice)
        {
            die.IsChosen = false;
        }
    }

    public void ChooseAll()
    {
        foreach (Die die in Dice)
        {
            die.IsChosen = true;
        }
    }

    public void RollDice()
    {
        foreach (Die die in UnchosenDice)
        {
            die.Roll();
        }
    }
}
