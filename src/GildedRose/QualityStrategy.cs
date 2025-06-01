namespace GildedRoseKata;

public class QualityStrategy(bool isArtifact = false)
{
    public void Apply(Item item)
    {
        if (isArtifact)
        {
            return;
        }
    }
}