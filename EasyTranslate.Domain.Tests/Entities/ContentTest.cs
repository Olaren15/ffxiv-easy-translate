using System.ComponentModel;
using EasyTranslate.Domain.Entities;
using Xunit;

namespace EasyTranslate.Domain.Tests.Entities;

public class ContentTest
{
    [Fact]
    public void English_NameForLanguage_ReturnsEnglishName()
    {
        Content content = GivenContent();

        string result = content.NameForLanguage(Language.English);

        Assert.Equal(content.EnglishName, result);
    }

    [Fact]
    public void French_NameForLanguage_ReturnsFrenchName()
    {
        Content content = GivenContent();

        string result = content.NameForLanguage(Language.French);

        Assert.Equal(content.FrenchName, result);
    }

    [Fact]
    public void German_NameForLanguage_ReturnsGermanName()
    {
        Content content = GivenContent();

        string result = content.NameForLanguage(Language.German);

        Assert.Equal(content.GermanName, result);
    }

    [Fact]
    public void Japanese_NameForLanguage_ReturnsJapaneseName()
    {
        Content content = GivenContent();

        string result = content.NameForLanguage(Language.Japanese);

        Assert.Equal(content.JapaneseName, result);
    }

    [Fact]
    public void UnknownLanguage_NameForLanguage_ThrowsArgumentOutOfRangeException()
    {
        Content content = GivenContent();

        Assert.Throws<InvalidEnumArgumentException>(() => content.NameForLanguage((Language)100));
    }

    private static Content GivenContent()
    {
        return new Content(
            ContentType.Achievement,
            null,
            "EnglishName",
            "FrenchName",
            "GermanName",
            "JapaneseName"
        );
    }
}
