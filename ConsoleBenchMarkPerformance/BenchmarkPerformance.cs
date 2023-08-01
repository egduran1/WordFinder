using BenchmarkDotNet.Attributes;

namespace ConsoleBenchmarkPerformance
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class BenchmarkPerformance
    {
        private List<string>? _words;
        private List<string>? _matrixString;
        private List<string>? _resultsList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _words = new List<string> { "cold", "wind", "snow", "chill", "w" };
            _matrixString = new List<string> { "cold1wsc", "wwindino", "inow1nol", "nhilldwd", "d234524d" };
            _resultsList = new List<string> { "w", "cold", "wind", "snow", "snow", "snow", "snow", "snow", "snow", "snow", "snow", "snow", "w", "w", "w", "w", "w" };
        }

        [Benchmark]
        public void CreateMatrixTest()
        {
            if (_matrixString != null) WordFinder.Helpers.Utils.CreateMatrix(_matrixString, out _, out _);
        }

        [Benchmark]
        public void FindWordVertical()
        {
            if (_matrixString == null) return;
            var wordFinder = new WordFinder.WordFinder(_matrixString);
            if (_words != null) wordFinder.FindVertical(_words);
        }

        [Benchmark]
        public void FindWordHorizontal()
        {
            if (_matrixString == null) return;
            var wordFinder = new WordFinder.WordFinder(_matrixString);
            if (_words != null) wordFinder.FindHorizontal(_words);
        }

        [Benchmark]
        public void FindTopG()
        {
            if (_resultsList != null) WordFinder.Helpers.Utils.GetTopTenRepeatedStrings(_resultsList);
        }
    }
}
