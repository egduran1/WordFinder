using System;

namespace WordFinder;


/// <summary>
/// After some benchmarks with different algorithms, I decide to use this implementations.
/// </summary>
public class WordFinder
{
    private readonly char[,]? _matrix;
    private readonly int _rows;
    private readonly int _cols;

    public WordFinder(IEnumerable<string> matrix)
    {
        this._matrix = Helpers.Utils.CreateMatrix(matrix, out this._cols, out this._rows);
    }

    /// <summary>
    /// Find words vertically and horizontally in a matrix of characters
    /// </summary>
    /// <param name="wordStream"></param>
    /// <returns>top 10 most repeated words from the word stream found in the matrix of characters, If no words are found will return an empty set of strings.</returns>
    public IEnumerable<string> Find(IEnumerable<string> wordStream)
    {
        // If no words are found, the "Find" method should return an empty set of strings.
        var topFindings = new List<string>();
        if (_matrix == null) return topFindings;
        var matrixMaxLength = _cols > _rows ? _cols : _rows;
        // If any word in the word stream is found more than once within the stream, the search results should count it only once
        var wordStreamList = wordStream.Distinct().Where(x => x.Length <= matrixMaxLength).ToList();
        topFindings.AddRange(FindHorizontal(wordStreamList));
        topFindings.AddRange(FindVertical(wordStreamList));
        return Helpers.Utils.GetTopTenRepeatedStrings(topFindings);
    }

    /// <summary>
    /// Find words horizontally in a matrix of characters
    /// </summary>
    /// <param name="words"></param>
    /// <returns>Enumerable of string</returns>
    public IEnumerable<string> FindHorizontal(IEnumerable<string> words)
    {
        var foundWords = new List<string>();
        for (var row = 0; row < _rows; row++)
        {
            var wordMatrix = string.Empty;
            for (var col = 0; col < _cols; col++)
            {
                if (_matrix != null) wordMatrix += _matrix[row, col];
            }

            foundWords.AddRange(FindWords(wordMatrix, words));
        }

        return foundWords;
    }

    /// <summary>
    /// Find words vertically in a matrix of characters
    /// </summary>
    /// <param name="words"></param>
    /// <returns>Enumerable of string</returns>
    public IEnumerable<string> FindVertical(IEnumerable<string> words)
    {
        var foundWords = new List<string>();
        for (var col = 0; col < _cols; col++)
        {
            var word = string.Empty;
            for (var row = 0; row < _rows; row++)
            {
                if (_matrix != null) word += _matrix[row, col];
            }

            foundWords.AddRange(FindWords(word, words));
        }

        return foundWords;
    }

    /// <summary>
    /// Find words in a enumerable of strings
    /// </summary>
    /// <param name="word"></param>
    /// <param name="words"></param>
    /// <returns></returns>
    public static IEnumerable<string> FindWords(string word, IEnumerable<string> words)
    {
        var listWords = new List<string>();
        foreach (var word1 in words)
        {
            for (var i = 0; i < word.Length - word1.Length; i++)
            {
                //using span.slice instead substring in order to have better performance
                var worToFind = word.AsSpan().Slice(i, word1.Length).ToString();
                if (worToFind == word1)
                {
                    listWords.Add(worToFind);
                }
            }
        }

        return listWords;
    }
}