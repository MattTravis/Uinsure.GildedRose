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
}