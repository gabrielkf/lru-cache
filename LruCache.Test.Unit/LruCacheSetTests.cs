using Bogus;

namespace LruCache.Test.Unit;

public class LruCacheSetTests
{
    private static readonly Faker FakeData = new();
    
    [Fact]
    public void Set_AddsItem_ToTopOfRemovalList()
    {
        // Arrange
        var cacheSize = FakeData.Random.Number(10);
        var cache = new Cache(cacheSize);
        for (var i = 1; i < cacheSize; i++)
        {
            cache.Set(FakeData.Random.AlphaNumeric(10),
                FakeData.Random.AlphaNumeric(10));
        }
        var key = FakeData.Random.AlphaNumeric(10);
        cache.Set(key, FakeData.Random.AlphaNumeric(10));
        cache.Set(FakeData.Random.AlphaNumeric(10),
            FakeData.Random.AlphaNumeric(10));
        
        // Act
        var result = cache.Get(key);
        
        // Assert
        result.Should().Be(-1);
    }
    
    [Fact]
    public void Set_RemovesOlderRecord_WhenCacheIsFull()
    {
        // Arrange
        var firstKey = FakeData.Random.AlphaNumeric(10);
        var cacheSize = FakeData.Random.Number(10);
        var cache = new Cache(cacheSize);
        
        cache.Set(firstKey, FakeData.Random.AlphaNumeric(10));

        for (var i = 1; i <= cacheSize; i++)
        {
            cache.Set(FakeData.Random.AlphaNumeric(10),
                FakeData.Random.AlphaNumeric(10));
        }
        
        // Act
        var result = cache.Get(firstKey);
        
        // Assert
        result.Should().Be(-1);
    }
}