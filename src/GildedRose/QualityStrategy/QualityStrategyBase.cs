namespace GildedRoseKata.QualityStrategy;

public abstract class QualityStrategyBase(int baseRateMultiplier = 1, int expiredMultiplier = 2, bool isEnhancing = false) : IQualityStrategy
{
    private const int BaseRate = -1;

    private readonly int _isEnhancingModifier = isEnhancing ? -1 : 1;

    public virtual void Apply(Item item)
    {
        var qualityDelta = BaseRate * _isEnhancingModifier * baseRateMultiplier;

        if (item.SellIn <= 0)
        {
            qualityDelta *= expiredMultiplier;
        }

        item.Quality = Math.Clamp(item.Quality + qualityDelta, 0, 50);
    }
}