namespace EasyTranslate.Domain.Comparers;

public class LongestCommonSubstringComparer : IStringSimilarityComparer
{
    public int Compare(string a, string b)
    {
        if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
        {
            return 0;
        }

        int[,] substringLengths = new int[a.Length, b.Length];
        int maxLength = 0;

        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < b.Length; j++)
            {
                int previousLength = GetPreviousLength(substringLengths, i, j);
                int currentLen = CalculateCurrentLength(a[i], b[j], previousLength);

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
