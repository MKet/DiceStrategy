namespace DiceStrategy.Game.Players;
public class OnlyNOrHigherPlayer : PlayerBase
{
    private readonly int _number;

    public OnlyNOrHigherPlayer(string name, int number, bool withTotalCheck) : base(name, withTotalCheck, $"Only{number}OrHigherPlayer")
    {
        _number = number;
    }

    protected override Func<int, bool> GetChooseDiePredicate(DiceModel dice) => (die) => die >= _number;
}
