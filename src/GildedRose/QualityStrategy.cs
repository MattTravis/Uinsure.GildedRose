namespace GildedRoseKata;

public class QualityStrategy(int baseRateMultiplier = 1, bool isEnhancing = false, bool isArtifact = false)
{
    private const int BaseRate = -1;

    private readonly int _isEnhancingModifier = isEnhancing ? -1 : 1;

    public void Apply(Item item)
    {
        if (isArtifact)
        {
            return;
        }

        item.Quality += BaseRate * _isEnhancingModifier * baseRateMultiplier;
    }
}