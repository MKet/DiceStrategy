using DiceStrategy.Game.Players;

namespace DiceStrategy.Reporting;

public class PlayerReportData
{
    public string Name { get; set; }

    public double AverageDiceTotal { get; set; }

    public int Wins { get; set; } = 0;

    public PlayerReportData(string name, double averageDiceTotal)
    {
        Name = name;
        AverageDiceTotal = averageDiceTotal;
    }

    public PlayerReportData(string name, double averageDiceTotal, int wins) : this(name, averageDiceTotal)
    {
        Wins = wins;
    }

    public PlayerReportData(PlayerBase player) : this(player.Name, player.AverageDiceTotal)
    {
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) ||
                (obj is PlayerReportData data &&
               Name == data.Name &&
               AverageDiceTotal == data.AverageDiceTotal &&
               Wins == data.Wins);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, AverageDiceTotal, Wins);
    }
}
