namespace DiceStrategy.Reporting;

public class ConsoleResultReporter : GameResultReporter
{
    public ConsoleResultReporter(int reportInterval, int gameTotal) : base(reportInterval, gameTotal)
    {
        Console.CursorVisible = false;
    }

    protected override void PrintReport(string report)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(report);
        _ = Console.ReadKey();
    }
}
