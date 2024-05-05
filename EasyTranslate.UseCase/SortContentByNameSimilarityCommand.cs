namespace EasyTranslate.UseCase;

using Domain.Entities;

public class SortContentByNameSimilarityCommand
{
    public IEnumerable<Content> Execute(
        string searchName,
        Language searchLanguage,
        IEnumerable<Content> contentList
    )
    {
        return contentList.OrderBy(content => Compare(searchName, content.NameForLanguage(searchLanguage)));
    }

    /// <summary>
    ///     Compare the similarity of two string using the Levenshtein distance algorith.
    ///     More information on the algorith <a href="https://en.wikipedia.org/wiki/Levenshtein_distance">here</a>.
    /// </summary>
    /// <param name="a">The first string to compare</param>
    /// <param name="b">The second string to compare</param>
    /// <returns>The edit distance between the two strings. The lower the value, the more similar they are to each other</returns>
    private static int Compare(string a, string b)
    {
        if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
        {
            return 0;
        }

        if (string.IsNullOrEmpty(a))
        {
            return b.Length;
        }

        if (string.IsNullOrEmpty(b))
        {
            return a.Length;
        }

        var lengthA = a.Length;
        var lengthB = b.Length;
        var distances = new int[lengthA + 1, lengthB + 1];

        for (var i = 0; i <= lengthA; i++)
        {
            distances[i, 0] = i;
        }
        for (var j = 0; j <= lengthB; j++)
        {
            distances[0, j] = j;
        }

        for (var i = 1; i <= lengthA; i++)
        {
            for (var j = 1; j <= lengthB; j++)
            {
                var cost = b[j - 1] == a[i - 1] ? 0 : 1;

                distances[i, j] = Math.Min(
                    Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                    distances[i - 1, j - 1] + cost
                );
            }
        }

        return distances[lengthA, lengthB];
    }
}
