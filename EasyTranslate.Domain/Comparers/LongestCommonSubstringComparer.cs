namespace EasyTranslate.Domain.Comparers;

public class LongestCommonSubstringComparer : IStringSimilarityComparer
{
    public int Compare(string a, string b)
    {
        if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
        {
            return 0;
        }

        var substringLengths = new int[a.Length, b.Length];
        var maxLength = 0;

        for (var i = 0; i < a.Length; i++)
        {
            for (var j = 0; j < b.Length; j++)
            {
                var previousLength = GetPreviousLength(substringLengths, i, j);
                var currentLen = CalculateCurrentLength(a[i], b[j], previousLength);

                if (currentLen > maxLength)
                {
                    maxLength = currentLen;
                }

                substringLengths[i, j] = currentLen;
            }
        }

        return Math.Max(a.Length, b.Length) - maxLength;
    }

    private static int CalculateCurrentLength(char a, char b, int previousLength)
    {
        if (a != b)
        {
            return 0;
        }

        return 1 + previousLength;
    }

    private static int GetPreviousLength(int[,] substringLengths, int i, int j)
    {
        return i == 0 || j == 0 ? 0 : substringLengths[i - 1, j - 1];
    }
}
