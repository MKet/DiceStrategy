namespace DiceStrategy.Players;
public class Only5OrHigherPlayer : PlayerBase
{
    public Only5OrHigherPlayer(string name) : base($"{nameof(Only5OrHigherPlayer)}: {name}")
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
            if (die.CurrentValue >= 5)
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
