namespace EasyTranslate.Domain.Comparers;

/// <summary>
///     Allows comparing two strings for their similarity
/// </summary>
public interface IStringSimilarityComparer
{
    /// <summary>
    ///     Compares the strings
    /// </summary>
    /// <param name="a">The first string to compare</param>
    /// <param name="b">The second string to compare</param>
    /// <returns>
    ///     A value representing the similarity of the strings. The lower the value, the more similar they are to each
    ///     other. A value of 0 means that they are identical
    /// </returns>
    public int Compare(string a, string b);
}
