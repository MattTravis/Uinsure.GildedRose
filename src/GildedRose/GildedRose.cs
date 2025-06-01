using GildedRoseKata.QualityStrategy;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;
    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateItems()
    {
        foreach (var item in Items)
        {
            var strategy = QualityStrategyFactory.Create(item.Name, item.SellIn);
            strategy.Apply(item);

            UpdateSellIn(item);
        }
    }

    private static void UpdateSellIn(Item item)
    {
        if (item.Name != "Sulfuras, Hand of Ragnaros")
        {
            item.SellIn -= 1;
        }
    }
}
