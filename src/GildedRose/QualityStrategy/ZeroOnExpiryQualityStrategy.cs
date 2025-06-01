namespace GildedRoseKata.QualityStrategy;

public class ZeroOnExpiryQualityStrategy : QualityStrategyBase
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