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
        UpdateQualityPreSellIn();
        UpdateSellIn();
        UpdateQualityPostSellIn();
    }

    private void UpdateQualityPreSellIn()
    {
        foreach (var item in Items)
        {
            if (item.Name == "Conjured Mana Cake")
            {
                if (item.Quality > 0)
                {
                    item.Quality -= 1;
                }
            }

            if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                if (item.Quality > 0)
                {
                    if (item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        item.Quality -= 1;
                    }
                }
            }
            else
            {
                if (item.Quality < 50)
                {
                    item.Quality += 1;

                    if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (item.SellIn < 11)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality += 1;
                            }
                        }

                        if (item.SellIn < 6)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality += 1;
                            }
                        }
                    }
                }
            }
        }
    }

    private void UpdateSellIn()
    {
        foreach (var item in Items)
        {
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn -= 1;
            }
        }
    }

    private void UpdateQualityPostSellIn()
    {
        foreach (var item in Items)
        {
            if (item.SellIn < 0)
            {
                if (item.Name == "Conjured Mana Cake")
                {
                    if (item.Quality > 0)
                    {
                        item.Quality -= 1;
                    }
                }

                if (item.Name != "Aged Brie")
                {
                    if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (item.Quality > 0)
                        {
                            if (item.Name != "Sulfuras, Hand of Ragnaros")
                            {
                                item.Quality -= 1;
                            }
                        }
                    }
                    else
                    {
                        item.Quality -= item.Quality;
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality += 1;
                    }
                }
            }
        }
    }
}
