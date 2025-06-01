namespace GildedRoseKata.QualityStrategy;

public static class QualityStrategyFactory
{
    public static IQualityStrategy Create(string itemName, int sellIn)
    {
        return itemName switch
        {
            "+5 Dexterity Vest" => new SimpleQualityStrategy(),
            "Aged Brie" => new ComplexQualityStrategy(isEnhancing: true),
            "Elixir of the Mongoose" => new SimpleQualityStrategy(),
            "Sulfuras, Hand of Ragnaros" => new ArtifactQualityStrategy(),
            "Backstage passes to a TAFKAL80ETC concert" => sellIn switch
            {
                < 6 => new ZeroOnExpiryQualityStrategy(3, isEnhancing: true),
                < 11 => new ZeroOnExpiryQualityStrategy(2, isEnhancing: true),
                _ => new ZeroOnExpiryQualityStrategy(isEnhancing: true)
            },
            "Conjured Mana Cake" => new ComplexQualityStrategy(2),
            _ => throw new ArgumentException($"Unknown item '{itemName}'", nameof(itemName))
        };
    }
}