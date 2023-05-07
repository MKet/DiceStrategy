using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceStrategy.Reporting
{
    public class ConsoleResultReporter : GameResultReporter
    {
        public ConsoleResultReporter(int reportInterval, int gameTotal) : base(reportInterval, gameTotal)
        {
        }

        protected override void PrintReport(string report)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(report);
            Console.ReadKey();
        }
    }
}
