using EasyTranslate.Domain.Comparers;
using Xunit;

namespace EasyTranslate.Domain.Tests.Comparers;

public class LongestCommonSubstringComparerTest
{
    private readonly LongestCommonSubstringComparer _longestCommonSubstringComparer = new();

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("This is a longer string")]
    public void EqualStrings_Compare_ReturnsZero(string value)
    {
        int result = _longestCommonSubstringComparer.Compare(value, value);

        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData("", "potato", 6)]
    [InlineData("potato", "", 6)]
    [InlineData("potato", "potatoes", 2)]
    [InlineData("potatoes", "potato", 2)]
    [InlineData("potatoes", "ta", 6)]
    [InlineData("ta", "potatoes", 6)]
    [InlineData("pot", "potato", 3)]
    [InlineData("potato", "pot", 3)]
    [InlineData("PotatO", "potato", 2)]
    [InlineData("potato", "PotatO", 2)]
    [InlineData("ABCDEFG", "HIJK", 7)]
    [InlineData("HIJK", "ABCDEFG", 7)]
    public void DifferentStrings_Compare_ReturnsDifferenceBetweenLongestStringAndMaxSubstringLength(
        string a,
        string b,
        int expected
    )
    {
        int result = _longestCommonSubstringComparer.Compare(a, b);

        Assert.Equal(expected, result);
    }
}
