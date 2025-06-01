namespace GildedRoseKata.QualityStrategy;

public class ZeroOnExpiryQualityStrategy(IQualityStrategy nextStrategy) : IQualityStrategy
{
    public void Apply(Item item)
    {
        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }
        else
        {
            nextStrategy.Apply(item);
        }
    }
}