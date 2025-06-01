namespace GildedRoseKata.QualityStrategy;

public class ZeroOnExpiryQualityStrategy(int baseRateMultiplier = 1, int expiredMultiplier = 2, bool isEnhancing = false)
    : QualityStrategyBase(baseRateMultiplier, expiredMultiplier, isEnhancing)
{
    public override void Apply(Item item)
    {
        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }
        else
        {
            base.Apply(item);
        }
    }
}