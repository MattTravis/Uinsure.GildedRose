namespace GildedRoseKata;

public static class QualityStrategyFactory
{
    public static QualityStrategy Create(string itemName)
    {
        return itemName switch
        {
            "+5 Dexterity Vest" => new QualityStrategy(),
            "Aged Brie" => new QualityStrategy(isEnhancing: true),
            "Elixir of the Mongoose" => new QualityStrategy(),
            "Sulfuras, Hand of Ragnaros" => new QualityStrategy(isArtifact: true),
            "Backstage passes to a TAFKAL80ETC concert" => new QualityStrategy(isEnhancing: true),
            "Conjured Mana Cake" => new QualityStrategy(2),
            _ => throw new ArgumentException($"Unknown item '{itemName}'", nameof(itemName))
        };
    }
}