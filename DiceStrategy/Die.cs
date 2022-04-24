namespace DiceStrategy;
public class Die : IComparable<Die>
{
    private readonly Random _random;

    public int MaxValue { get; }
    public int CurrentValue { get; private set; }
    public bool IsChosen { get; set; }

    public Die(int maxValue = 6)
    {
        MaxValue = maxValue;
        _random = new Random();
    }

    public Die(Random random, int maxValue = 6) : this(maxValue) => _random = random;

    public int Roll()
    {
        CurrentValue = _random.Next(1, MaxValue + 1);
        return CurrentValue;
    }

    public override bool Equals(object? obj) => obj is Die die && MaxValue == die.MaxValue && CurrentValue == die.CurrentValue;
    public override int GetHashCode() => HashCode.Combine(MaxValue, CurrentValue);
    public int CompareTo(Die? other) => CurrentValue.CompareTo(other?.CurrentValue);
}
