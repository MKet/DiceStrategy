using DiceStrategy;
using DiceStrategy.Factories;
using DiceStrategy.Reporting;

Console.CursorVisible = false;

const int gameTotal = 5_000_000;
const int reportInterval = 5000;

var gameFactory = new AllPlayerGameFactory();
var reporter = new ConsoleResultReporter(reportInterval, gameTotal);

var runner = new GameRunner(gameFactory, reporter);

reporter.Start();
await runner.RunAsync(gameTotal);
reporter.Stop();

