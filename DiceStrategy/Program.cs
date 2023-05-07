using DiceStrategy;
using DiceStrategy.Factories;
using DiceStrategy.Reporting;

const int gameTotal = 10_000_000;
const int reportInterval = 5000;

var gameFactory = new AllPlayerGameFactory();
var reporter = new ConsoleResultReporter(reportInterval, gameTotal);

var runner = new GameRunner(gameFactory, reporter);

reporter.Start();
runner.Run(gameTotal);
reporter.Stop();