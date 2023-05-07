using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceStrategy.Reporting
{
    public class PlayerReportData
    {
        public PlayerReportData(string name, double averageDiceTotal, int wins)
        {
            Name = name;
            AverageDiceTotal = averageDiceTotal;
            Wins = wins;
        }

        public PlayerReportData(string name, double averageDiceTotal)
        {
            Name = name;
            AverageDiceTotal = averageDiceTotal;
        }

        public string Name { get; set; }

        public double AverageDiceTotal { get; set; }

        public int Wins { get; set; } = 0;

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) ||
                    obj is PlayerReportData data &&
                   Name == data.Name &&
                   AverageDiceTotal == data.AverageDiceTotal &&
                   Wins == data.Wins;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, AverageDiceTotal, Wins);
        }
    }
}
