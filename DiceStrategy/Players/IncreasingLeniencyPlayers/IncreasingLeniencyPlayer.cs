namespace DiceStrategy.Players.IncreasingLeniencyPlayers;
public class IncreasingLeniencyPlayer : PlayerBase
{
    public IncreasingLeniencyPlayer(string name) : base($"{nameof(IncreasingLeniencyPlayer)}: {name}")
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
