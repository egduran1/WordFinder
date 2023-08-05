using System;
using System.Security.Cryptography;

namespace WordFinder;


/// <summary>
/// After some benchmarks with different algorithms, I decide to use this implementations.
/// </summary>
public class WordFinder
{
    private readonly char[,]? _matrix;
    private readonly int _rows;
    private readonly int _cols;
    private readonly List<string> _foundWords;

    public WordFinder(IEnumerable<string> matrix)
    {
        _foundWords= new List<string>();
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
        if (_matrix == null || !wordStream.Any()) return _foundWords;
        var matrixMaxLength = _cols > _rows ? _cols : _rows;
        // If any word in the word stream is found more than once within the stream, the search results should count it only once
        var wordStreamList = wordStream.Distinct().Where(x => x.Length <= matrixMaxLength).ToList();
        Find(wordStreamList, MatrixSearchDirection.HorizontalRightToLeft);
        Find(wordStreamList, MatrixSearchDirection.VerticalTopToBottom);
        return Helpers.Utils.GetTopTenRepeatedStrings(_foundWords);
    }

    /// <summary>
    /// Find words in a enumerable of strings
    /// </summary>
    /// <param name="word"></param>
    /// <param name="words"></param>
    /// <returns></returns>
    public static IEnumerable<string> FindWords(string word, IEnumerable<string> words)
    {
        foreach (var word1 in words)
        {
            for (var i = 0; i < word.Length - word1.Length; i++)
            {
                //using span.slice instead substring in order to have better performance
                var worToFind = word.AsSpan().Slice(i, word1.Length).ToString();
                if (worToFind == word1)
                {
                    yield return worToFind;
                }
            }
        }
    }

    /// <summary>
    /// Find words
    /// </summary>
    /// <param name="words"></param>
    /// <param name="direction"></param>
    public void Find(IEnumerable<string> words, MatrixSearchDirection direction)
    {
        var loop1 = direction == MatrixSearchDirection.HorizontalRightToLeft? _rows : _cols;
        var loop2 = direction == MatrixSearchDirection.HorizontalRightToLeft ? _cols : _rows;
        for (var r1 = 0; r1 < loop1; r1++)
        {
            var word = string.Empty;
            for (var r2 = 0; r2 < loop2; r2++)
            {
                word += direction == MatrixSearchDirection.HorizontalRightToLeft ? _matrix[r1, r2] : _matrix[r2,r1];
            }

            _foundWords.AddRange(FindWords(word,words));
        }
    }


    public enum MatrixSearchDirection
    {
        HorizontalRightToLeft = 1,
        VerticalTopToBottom = 2
    }
}