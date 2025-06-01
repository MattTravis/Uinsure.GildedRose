namespace GildedRoseKata;

public class QualityStrategy(bool isArtifact = false)
{
    private const int BaseRate = -1;

    public void Apply(Item item)
    {
        if (isArtifact)
        {
            return;
        }

        item.Quality += BaseRate;
    }
}