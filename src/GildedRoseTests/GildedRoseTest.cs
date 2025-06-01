using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Theory]
    [InlineData("+5 Dexterity Vest", -1)]
    [InlineData("Aged Brie", 1)]
    [InlineData("Elixir of the Mongoose", -1)]
    [InlineData("Sulfuras, Hand of Ragnaros", 0)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 11)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 2, 10)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 2, 6)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 3, 5)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 3, 1)]
    [InlineData("Conjured Mana Cake", -2)]
    public void UpdateQuality_GivenItem_WhenSellByNotElapsed_ThenAltersQualityByRate(string itemName, int rate, int sellIn = 15)
    {
        const int InitialQuality = 5;
        List<Item> items = [new() { Name = itemName, SellIn = sellIn, Quality = InitialQuality }];
        GildedRose app = new(items);
        app.UpdateQuality();
        Assert.Single(items);
        Assert.Equal(itemName, items[0].Name);
        Assert.Equal(InitialQuality + rate, items[0].Quality);
    }

    [Theory]
    [InlineData("+5 Dexterity Vest", -2)]
    [InlineData("Aged Brie", 2)]
    [InlineData("Elixir of the Mongoose", -2)]
    [InlineData("Sulfuras, Hand of Ragnaros", 0)]
    [InlineData("Conjured Mana Cake", -4)]
    public void UpdateQuality_GivenItem_WhenSellByElapsed_ThenAltersQualityByRate(string itemName, int rate)
    {
        const int InitialQuality = 5;
        List<Item> items = [new() { Name = itemName, SellIn = 0, Quality = InitialQuality }];
        GildedRose app = new(items);
        app.UpdateQuality();
        Assert.Single(items);
        Assert.Equal(itemName, items[0].Name);
        Assert.Equal(InitialQuality + rate, items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_GivenBackstagePass_WhenSellByElapsed_ThenQualityIsZero()
    {
        const string ItemName = "Backstage passes to a TAFKAL80ETC concert";
        List<Item> items = [new() { Name = ItemName, SellIn = 0, Quality = 5 }];
        GildedRose app = new(items);
        app.UpdateQuality();
        Assert.Single(items);
        Assert.Equal(ItemName, items[0].Name);
        Assert.Equal(0, items[0].Quality);
    }

    [Theory]
    [InlineData("+5 Dexterity Vest")]
    [InlineData("Aged Brie")]
    [InlineData("Elixir of the Mongoose")]
    [InlineData("Sulfuras, Hand of Ragnaros", 0)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert")]
    [InlineData("Conjured Mana Cake")]
    public void UpdateQuality_GivenItem_ThenAltersSellInByRate(string itemName, int rate = -1)
    {
        const int InitialSellIn = 5;
        List<Item> items = [new() { Name = itemName, SellIn = 5, Quality = 1 }];
        GildedRose app = new(items);
        app.UpdateQuality();
        Assert.Single(items);
        Assert.Equal(itemName, items[0].Name);
        Assert.Equal(InitialSellIn + rate, items[0].SellIn);
    }

    [Theory]
    [InlineData("+5 Dexterity Vest", 0)]
    [InlineData("Aged Brie", 50)]
    [InlineData("Elixir of the Mongoose", 0)]
    [InlineData("Sulfuras, Hand of Ragnaros", 80)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 50)]
    [InlineData("Conjured Mana Cake", 0)]
    public void UpdateQuality_GivenItem_WhenQualityIsAtLimit_ThenQualityLimitIsNotExceeded(string itemName, int initialQuality)
    {
        List<Item> items = [new() { Name = itemName, SellIn = 15, Quality = initialQuality }];
        GildedRose app = new(items);
        app.UpdateQuality();
        Assert.Single(items);
        Assert.Equal(itemName, items[0].Name);
        Assert.Equal(initialQuality, items[0].Quality);
    }

    [Theory]
    [InlineData("+5 Dexterity Vest", 1, 0)]
    [InlineData("Aged Brie", 49, 50)]
    [InlineData("Elixir of the Mongoose", 1, 0)]
    [InlineData("Sulfuras, Hand of Ragnaros", 80, 80)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 49, 50, 1)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 49, 0)]
    [InlineData("Conjured Mana Cake", 1, 0)]
    public void UpdateQuality_GivenItem_WhenQualityIsNearLimit_ThenQualityLimitIsNotExceeded(string itemName, int initialQuality, int expectedQuality, int sellIn = 0)
    {
        List<Item> items = [new() { Name = itemName, SellIn = sellIn, Quality = initialQuality }];
        GildedRose app = new(items);
        app.UpdateQuality();
        Assert.Single(items);
        Assert.Equal(itemName, items[0].Name);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
}