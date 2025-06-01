namespace GildedRoseKata;

public class QualityStrategy(int baseRateMultiplier = 1, int expiredMultiplier = 2, bool isEnhancing = false, bool isArtifact = false)
{
    private const int BaseRate = -1;
    
    private readonly int _isEnhancingModifier = isEnhancing ? -1 : 1;

    public void Apply(Item item)
    {
        if (isArtifact)
        {
            return;
        }

        var qualityDelta = BaseRate * _isEnhancingModifier * baseRateMultiplier;

        if (item.SellIn <= 0)
        {
            qualityDelta *= expiredMultiplier;
        }

        item.Quality += qualityDelta;
    }
}