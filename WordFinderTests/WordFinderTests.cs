using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordFinderTests
{
    [TestClass()]
    public class WordFinderTests
    {
        [TestMethod()]
        public void WordFinder_ValidMatrixWithSomeFindings_Find()
        {
            // arrange
            var words = new List<string> { "cold", "wind", "snow", "chill", "w" };
            var matrixStrings = new List<string> { "cold1wsc", "wwindino", "inow1nol", "nhilldwd", "d234524d" };
            var resultsList = new List<string> { "w", "cold", "wind", "snow" };
            var matrix = new WordFinder.WordFinder(matrixStrings);

            // act

            var findings = matrix.Find(words).ToList();

            //assert
            CollectionAssert.AreEqual(resultsList, findings);
        }

        [TestMethod()]
        public void WordFinder_ValidMatrixWithNoFindings_Find()
        {
            // arrange
            var words = new List<string> { "colde", "winde", "snowe", "chille" };
            var matrixStrings = new List<string> { "cold1wsc", "wwindino", "inow1nol", "nhilldwd", "d234524d" };
            var resultsList = new List<string>();
            var matrix = new WordFinder.WordFinder(matrixStrings);

            // act

            var findings = matrix.Find(words).ToList();

            //assert
            CollectionAssert.AreEqual(resultsList, findings);
        }

        [TestMethod()]
        public void WordFinder_ValidMatrixWithEmptyWordsToFind_Find()
        {
            // arrange
            var words = new List<string>();
            var matrixStrings = new List<string> { "cold1wsc", "wwindino", "inow1nol", "nhilldwd", "d234524d" };
            var resultsList = new List<string>();
            var matrix = new WordFinder.WordFinder(matrixStrings);

            // act

            var findings = matrix.Find(words).ToList();

            //assert
            CollectionAssert.AreEqual(resultsList, findings);
        }

        [TestMethod()]
        public void WordFinder_ValidMatrixWithInvalidWordsToFind_Find()
        {
            // arrange
            var words = new List<string> { "dasdasdasdasdasdasdasdadad", "sadas" };
            var matrixStrings = new List<string> { "cold1wsc", "wwindino", "inow1nol", "nhilldwd", "d234524d" };
            var resultsList = new List<string>();
            var matrix = new WordFinder.WordFinder(matrixStrings);

            // act

            var findings = matrix.Find(words).ToList();

            //assert
            CollectionAssert.AreEqual(resultsList, findings);
        }

        [TestMethod()]
        public void WordFinder_ValidMatrixWithInvalidAndValidWordsToFind_Find()
        {
            // arrange
            var words = new List<string> { "dasdasdasdasdasdasdasdadad", "cold" };
            var matrixStrings = new List<string> { "cold1wsc", "wwindino", "inow1nol", "nhilldwd", "d234524d" };
            var resultsList = new List<string> { "cold" };
            var matrix = new WordFinder.WordFinder(matrixStrings);

            // act

            var findings = matrix.Find(words).ToList();

            //assert
            CollectionAssert.AreEqual(resultsList, findings);
        }

        [TestMethod()]
        public void WordFinder_InvalidMatrix_Find()
        {
            // arrange
            var words = new List<string> { "colde", "winde", "snowe", "chille" };
            var matrixStrings = new List<string> { "cold1wsc1", "ino", "inow1nol", "nhilererwldwd", "d234524d" };
            var resultsList = new List<string>();
            var matrix = new WordFinder.WordFinder(matrixStrings);

            // act

            var findings = matrix.Find(words).ToList();

            //assert
            CollectionAssert.AreEqual(resultsList, findings);
        }
    }
}