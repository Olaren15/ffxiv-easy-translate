namespace EasyTranslate.DalamudPlugin.Localisation;

using System;
using Domain.Entities;
using Resources;

public static class ContentTypeTranslations
{
    public static string LocalisedName(this ContentType contentType)
    {
        return contentType switch
        {
            ContentType.Achievement => Strings.Achievement,
            ContentType.Action => Strings.Action,
            ContentType.Emote => Strings.Emote,
            ContentType.Fate => Strings.Fate,
            ContentType.Instance => Strings.Instance,
            ContentType.Item => Strings.Item,
            ContentType.LeveQuest => Strings.LeveQuest,
            ContentType.Minion => Strings.Minion,
            ContentType.Mount => Strings.Mount,
            ContentType.Npc => Strings.Npc,
            ContentType.Orchestrion => Strings.Orchestrion,
            ContentType.Place => Strings.Place,
            ContentType.Quest => Strings.Quest,
            ContentType.Status => Strings.Status,
            ContentType.Title => Strings.Title,
            ContentType.Trait => Strings.Trait,
            ContentType.Weather => Strings.Weather,
            _ => throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null),
        };
    }
}
