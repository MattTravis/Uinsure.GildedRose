namespace GildedRoseKata.QualityStrategy;

public static class QualityStrategyFactory
{
    public static IQualityStrategy Create(string itemName)
    {
        return itemName switch
        {
            "+5 Dexterity Vest" => new QualityStrategy(),
            "Aged Brie" => new QualityStrategy(),
            "Elixir of the Mongoose" => new QualityStrategy(),
            "Sulfuras, Hand of Ragnaros" => new ArtifactQualityStrategy(),
            "Backstage passes to a TAFKAL80ETC concert" => new ZeroOnExpiryQualityStrategy(),
            "Conjured Mana Cake" => new QualityStrategy(),
            _ => throw new ArgumentException($"Unknown item '{itemName}'", nameof(itemName))
        };
    }
}