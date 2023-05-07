namespace DiceStrategy.Players.IncreasingLeniencyPlayers;
public class IncreasingLeniencyPlayer : PlayerBase
{
    public IncreasingLeniencyPlayer(string name, bool withTotalCheck) : base(name, withTotalCheck)
    {
    }

    protected override Func<int, bool> GetChooseDiePredicate(DiceModel dice)
    {
        return (die) => (die, dice.UnchosenDieAmount) switch
        {
            ( >= 6, _) => true,
            ( >= 5, <= 3) => true,
            ( >= 4, <= 2) => true,
            ( >= 3, <= 1) => true,
            (_, _) => false
        };
    }
}
