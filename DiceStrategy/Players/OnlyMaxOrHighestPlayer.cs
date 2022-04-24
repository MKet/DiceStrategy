namespace DiceStrategy.Players;
public class OnlyMaxOrHighestPlayer : PlayerBase
{
    public OnlyMaxOrHighestPlayer(string name) : base($"{nameof(OnlyMaxOrHighestPlayer)}: {name}")
    {
    }
    public override void ChooseDice(DiceSet set)
    {
        if (set.TotalDicevalue >= 30)
        {
            set.ChooseAll();
            return;
        }

        IOrderedEnumerable<Die>? query = from die in set.Dice
                                         where die.IsChosen == false
                                         orderby die.CurrentValue descending
                                         select die;
        query.First().IsChosen = true;

        foreach (Die die in query)
        {
            if (die.CurrentValue == die.MaxValue)
            {
                die.IsChosen = true;
            }
            else
            {
                break;
            }
        }
    }
}
