using Bogus;

namespace LruCache.Test.Unit;

public class LruCacheTests
{
    private static Faker FakeData = new();
    
    [Fact]
    public void Get_ReturnsNull_WhenKeyNotFound()
    {
        // Arrange
        var cache = new Cache(3);
        
        // Act
        var result = cache.Get(FakeData.Random.AlphaNumeric(10));
        
        // Assert
        result.Should().BeNull();
    }
    
    [Fact]
    public void Get_ReturnsValue_WhenFound()
    {
        // Arrange
        var key = FakeData.Random.AlphaNumeric(10);
        var value = FakeData.Lorem.Sentence();
        var cache = new Cache(3);
        cache.Set(key, value);
        
        // Act
        var result = cache.Get(key);
        
        // Assert
        result.Should().Be(value);
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
        result.Should().BeNull();
    }
    
    [Fact]
    public void Get_ShouldPrevent_ItemBeingRemoved()
    {
        // Arrange
        var firstKey = FakeData.Random.AlphaNumeric(10);
        var value = FakeData.Lorem.Sentence();
        var cacheSize = FakeData.Random.Number(10);
        var cache = new Cache(cacheSize);
        
        cache.Set(firstKey, value);

        for (var i = 1; i < cacheSize * 2 - 1; i++)
        {
            if (i == cacheSize) cache.Get(firstKey);
            
            cache.Set(FakeData.Random.AlphaNumeric(10),
                FakeData.Random.AlphaNumeric(10));
        }
        
        // Act
        var result = cache.Get(firstKey);
        
        // Assert
        result.Should().Be(value);
    }
}