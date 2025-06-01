namespace GildedRoseKata.QualityStrategy;

public class QualityStrategy : IQualityStrategy
{
    private const int ExpiredMultiplier = 2;

    private static int GetBaseRate(Item item)
    {
        return item.Name switch
        {
            "Backstage passes to a TAFKAL80ETC concert" => item.SellIn switch
            {
                < 6 => 3,
                < 11 => 2,
                _ => 1
            },
            "Conjured Mana Cake" => -2,
            "Aged Brie" => 1,
            _ => -1
        };
    }

    public void Apply(Item item)
    {
        var qualityDelta = GetBaseRate(item);

        if (item.SellIn <= 0)
        {
            qualityDelta *= ExpiredMultiplier;
        }

        item.Quality = Math.Clamp(item.Quality + qualityDelta, 0, 50);
    }
}