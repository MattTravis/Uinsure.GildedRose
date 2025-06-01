namespace GildedRoseKata.QualityStrategy;

public class ComplexQualityStrategy(int baseRateMultiplier = 1, int expiredMultiplier = 2, bool isEnhancing = false) 
    : QualityStrategyBase(baseRateMultiplier, expiredMultiplier, isEnhancing);