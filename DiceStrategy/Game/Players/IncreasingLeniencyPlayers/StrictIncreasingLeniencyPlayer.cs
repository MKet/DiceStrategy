namespace DiceStrategy.Game.Players.IncreasingLeniencyPlayers;
public class StrictIncreasingLeniencyPlayer : PlayerBase
{
    public StrictIncreasingLeniencyPlayer(string name, bool withTotalCheck) : base(name, withTotalCheck)
    {
    }

    protected override Func<int, bool> GetChooseDiePredicate(DiceModel dice)
    {
        return (die) => (die, dice.UnchosenDieAmount) switch
        {
            ( >= 6, _) => true,
            ( >= 5, <= 4) => true,
            ( >= 4, <= 3) => true,
            ( >= 3, <= 2) => true,
            (_, _) => false
        };
    }
}
