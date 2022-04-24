namespace DiceStrategy;
public abstract class PlayerBase
{
    public int Score { get; set; } = 30;
    public string Name { get; }

    protected PlayerBase(string name) => Name = name;

    public DiceSet Play(DiceSet set)
    {
        set.Reset();

        while (true)
        {
            set.RollDice();
            ChooseDice(set);

            if (!set.UnchosenDie.Any())
            {
                return set;
            }
        }
    }

    public abstract void ChooseDice(DiceSet set);
}
