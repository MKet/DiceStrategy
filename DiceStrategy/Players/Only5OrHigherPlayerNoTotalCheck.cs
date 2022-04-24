namespace DiceStrategy.Players;
public class Only5OrHigherPlayerNoTotalCheck : PlayerBase
{
    public Only5OrHigherPlayerNoTotalCheck(string name) : base($"{nameof(Only5OrHigherPlayerNoTotalCheck)}: {name}")
    {
    }

    public override void ChooseDice(DiceSet set)
    {
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
