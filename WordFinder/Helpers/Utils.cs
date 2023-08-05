namespace WordFinder.Helpers
{
    public static class Utils
    {
        /// <summary>
        /// Create matrix of characters from a enumerable strings
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="matrixColsLength"></param>
        /// <param name="matrixRowsLength"></param>
        /// <returns>matrix of characters and columns and rows length</returns>

        public static char[,]? CreateMatrix(IEnumerable<string> matrix, out int matrixColsLength, out int matrixRowsLength)
        {
            var matrixList = matrix.ToList();
            matrixRowsLength = matrixList.Count;
            matrixColsLength = matrixList.FirstOrDefault()?.Length ?? 0;
            var colsLength = matrixColsLength;
            // The matrix size does not exceed 64x64, all strings contain the same number of characters.
            if (matrixRowsLength == 0 || matrixColsLength == 0 || matrixRowsLength > 64 || matrixColsLength > 64 || matrixList.Any(x => x.Length != colsLength)) return null;
            var matrixData = new char[matrixRowsLength, matrixColsLength];
            for (var i = 0; i < matrixRowsLength; i++)
            {
                var chars = matrixList[i].ToCharArray();
                for (var j = 0; j < chars.Length; j++)
                {
                    matrixData[i, j] = chars[j];
                }
            }
            return matrixData;
        }

        /// <summary>
        /// Get Top Ten Sorted List of repeated strings
        /// </summary>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetTopTenRepeatedStrings(List<string> inputList)
        {
            var stringOccurrences = new Dictionary<string, int>();

            // Count occurrences of each string using the dictionary
            foreach (var str in inputList)
            {
                if (stringOccurrences.TryGetValue(str, out var count))
                {
                    stringOccurrences[str] = count + 1;
                }
                else
                {
                    stringOccurrences[str] = 1;
                }
            }

            // Sort the dictionary by value in descending order
            var sortedOccurrences = new List<KeyValuePair<string, int>>(stringOccurrences);
            sortedOccurrences.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            // Take the top ten repeated strings
            for (var i = 0; i < Math.Min(10, sortedOccurrences.Count); i++)
            {
                yield return sortedOccurrences[i].Key;
            }
        }
    }
}
