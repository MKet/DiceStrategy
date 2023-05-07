namespace DiceStrategy.Game;
public class DiceModel
{
    private readonly int[] _dieResults;
    private readonly Random _random;
    private readonly int _totalDieAmount;

    public int UnchosenDieAmount { get; private set; }

    public int TotalDicevalue { get; private set; } = 0;

    public DiceModel(Random random, int dieAmount = 6, int DiceSize = 6)
    {
        _dieResults = new int[DiceSize];
        _random = random;
        _totalDieAmount = dieAmount;
        UnchosenDieAmount = dieAmount;

        Reset();
    }

    public void Reset()
    {
        ResetDiceResults();
        TotalDicevalue = 0;
        UnchosenDieAmount = _totalDieAmount;
    }

    public IEnumerable<int> DiceResults
    {
        get
        {
            for (var i = _dieResults.Length - 1; i > 0; i--)
            {
                for (var j = 0; j < _dieResults[i]; j++)
                {
                    yield return i + 1;
                }
            }
        }
    }

    public void ResetDiceResults()
    {
        for (var i = 0; i < _dieResults.Length; i++)
        {
            _dieResults[i] = 0;
        }
    }

    public void Choose(int value)
    {
        if (_dieResults[value - 1] == 0)
        {
            throw new ArgumentException("Dice value does not exist");
        }

        TotalDicevalue += value;
        UnchosenDieAmount--;
    }

    public void ChooseAll()
    {
        for (var i = 0; i < _dieResults.Length; i++)
        {
            TotalDicevalue += (i + 1) * _dieResults[i];
            UnchosenDieAmount -= _dieResults[i];
        }
    }

    public void RollDice()
    {
        ResetDiceResults();
        for (var i = 0; i < UnchosenDieAmount; i++)
        {
            var roll = _random.Next(0, _dieResults.Length);
            _dieResults[roll]++;
        }
    }

    public int? ChooseHighest()
    {
        for (var i = _dieResults.Length - 1; i > 0; i--)
        {
            if (_dieResults[i - 1] > 0)
            {
                Choose(i);
                return i;
            }
        }
        return null;
    }
}
