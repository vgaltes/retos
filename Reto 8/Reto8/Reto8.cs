namespace Reto8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class AlphabeticalOrder
    {
        public static IEnumerable<string> GetShortestConcatString()
        {
            return new Collection<string>();
        }

        public static string GetShortestConcatString(string input)
        {
            if (input == null) throw new ArgumentNullException("input");

            var splittedString = input.Split(new[] {' '});


            var result = ShortestConcatString(splittedString.ToList());
            return string.Concat(result);
        }

        public static IEnumerable<string> GetShortestConcatString(params string[] inputStrings)
        {
            if (inputStrings == null) throw new ArgumentNullException("inputStrings");

            if (inputStrings.Any(input => input == null))
            {
                throw new ArgumentNullException("inputStrings");
            }

            foreach (var input in inputStrings)
            {
                var splittedString = input.Split(new[] {' '});
                yield return string.Concat(ShortestConcatString(splittedString.ToList()));
            }
        }

        public static IEnumerable<string> GetShortestConcatString(IEnumerable<string> input)
        {
            if (input == null) throw new ArgumentNullException("input");

            return ShortestConcatString(input.ToList());
        }

        private static IEnumerable<string> ShortestConcatString(List<string> input)
        {
            var numIterations = input.Count();
            var nextWord = string.Empty;

            for (var i = 0; i < numIterations; i++)
            {
                input.Remove(nextWord);
                nextWord = CalculateNextWord(input);

                yield return nextWord;
            }
        }

        private static string CalculateNextWord(IReadOnlyList<string> input)
        {
            if (input.Count() == 1)
                return input.First();

            var wordToWorkWith = input.OrderBy(w => w).First();

            return wordToWorkWith + input.Select(w => w.Replace(wordToWorkWith, "") + wordToWorkWith)
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .OrderBy(w => w)
                .Select(w => w.Substring(0,w.Length - wordToWorkWith.Length))
                .FirstOrDefault();
        }
    }
}