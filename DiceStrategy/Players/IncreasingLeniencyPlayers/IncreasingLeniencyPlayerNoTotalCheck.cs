namespace DiceStrategy.Players.IncreasingLeniencyPlayers;
public class IncreasingLeniencyPlayerNoTotalCheck : PlayerBase
{
    public IncreasingLeniencyPlayerNoTotalCheck(string name) : base($"{nameof(IncreasingLeniencyPlayerNoTotalCheck)}: {name}")
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
            die.IsChosen = (die.CurrentValue, set.UnchosenDice.Count()) switch
            {
                ( >= 6, _) => true,
                ( >= 5, <= 3) => true,
                ( >= 4, <= 2) => true,
                ( >= 3, <= 1) => true,
                (_, _) => false
            };

            if (!die.IsChosen)
            {
                break;
            }
        }
    }
}
