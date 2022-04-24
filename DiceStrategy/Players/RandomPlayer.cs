namespace DiceStrategy.Players;
public class RandomPlayer : PlayerBase
{
    private readonly Random _random;

    public RandomPlayer(string name, Random random) : base($"{nameof(RandomPlayer)}: {name}") => _random = random;

    public override void ChooseDice(DiceSet set)
    {
        IEnumerable<Die>? query = from die in set.Dice
                                  where die.IsChosen == false
                                  select die;

        query.First().IsChosen = true;

        foreach (Die die in query)
        {
            die.IsChosen = _random.Next() % 3 == 0;
        }
    }
}
