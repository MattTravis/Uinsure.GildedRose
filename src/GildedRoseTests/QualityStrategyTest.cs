using GildedRoseKata.QualityStrategy;
using GildedRoseKata;

namespace GildedRoseTests;

public class QualityStrategyTest
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
    public void Apply_GivenItem_WhenSellByNotElapsed_ThenAltersQualityByRate(string itemName, int rate, int sellIn = 15)
    {
        const int InitialQuality = 5;
        Item item = new() { Name = itemName, SellIn = sellIn, Quality = InitialQuality };
        var strategy = QualityStrategyFactory.Create(item.Name, item.SellIn);
        strategy.Apply(item);
        Assert.Equal(InitialQuality + rate, item.Quality);
    }

    [Theory]
    [InlineData("+5 Dexterity Vest", -2)]
    [InlineData("Aged Brie", 2)]
    [InlineData("Elixir of the Mongoose", -2)]
    [InlineData("Sulfuras, Hand of Ragnaros", 0)]
    // Assumption: 
    // Base Item Degradation Rate = -1
    // Expired Item Degradation Rate = -2 --> 2x Base
    // Conjured Item Degradation Rate = -2 --> 2x Base
    // Expired Conjured Item Degradation Rate = -4 --> 2x Expired plus 2x Conjured 
    [InlineData("Conjured Mana Cake", -4)]
    public void Apply_GivenItem_WhenSellByElapsed_ThenAltersQualityByRate(string itemName, int rate)
    {
        const int InitialQuality = 5;
        Item item = new() { Name = itemName, SellIn = 0, Quality = InitialQuality };
        var strategy = QualityStrategyFactory.Create(item.Name, item.SellIn);
        strategy.Apply(item);
        Assert.Equal(InitialQuality + rate, item.Quality);
    }

    [Fact]
    public void Apply_GivenBackstagePass_WhenSellByElapsed_ThenQualityIsZero()
    {
        const string ItemName = "Backstage passes to a TAFKAL80ETC concert";
        Item item = new() { Name = ItemName, SellIn = 0, Quality = 50 };
        var strategy = QualityStrategyFactory.Create(item.Name, item.SellIn);
        strategy.Apply(item);
        Assert.Equal(0, item.Quality);
    }

    [Theory]
    [InlineData("+5 Dexterity Vest", 0)]
    [InlineData("Aged Brie", 50)]
    [InlineData("Elixir of the Mongoose", 0)]
    [InlineData("Sulfuras, Hand of Ragnaros", 80)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 50)]
    [InlineData("Conjured Mana Cake", 0)]
    public void Apply_GivenItem_WhenQualityIsAtLimit_ThenQualityLimitIsNotExceeded(string itemName, int initialQuality)
    {
        Item item = new() { Name = itemName, SellIn = 15, Quality = initialQuality };
        var strategy = QualityStrategyFactory.Create(item.Name, item.SellIn);
        strategy.Apply(item);
        Assert.Equal(initialQuality, item.Quality);
    }

    [Theory]
    [InlineData("+5 Dexterity Vest", 1, 0)]
    [InlineData("Aged Brie", 49, 50)]
    [InlineData("Elixir of the Mongoose", 1, 0)]
    [InlineData("Sulfuras, Hand of Ragnaros", 80, 80)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 49, 50, 1)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 49, 0)]
    [InlineData("Conjured Mana Cake", 1, 0)]
    public void Apply_GivenItem_WhenQualityIsNearLimit_ThenQualityLimitIsNotExceeded(string itemName, int initialQuality, int expectedQuality, int sellIn = 0)
    {
        Item item = new() { Name = itemName, SellIn = sellIn, Quality = initialQuality };
        var strategy = QualityStrategyFactory.Create(item.Name, item.SellIn);
        strategy.Apply(item);
        Assert.Equal(expectedQuality, item.Quality);
    }
}