using Bogus;

namespace LruCache.Test.Unit;

public class LruCacheGetTests
{
    private static readonly Faker FakeData = new();
    
    [Fact]
    public void Get_ReturnsNegativeOne_WhenEmpty()
    {
        // Arrange
        var cacheSize = FakeData.Random.Number(10);
        var cache = new Cache(cacheSize);
        
        // Act
        var result = cache.Get(FakeData.Random.AlphaNumeric(10));
        
        // Assert
        result.Should().Be(-1);
    }
    
    [Fact]
    public void Get_ReturnsNegativeOne_WhenKeyNotInserted()
    {
        // Arrange
        var cacheSize = FakeData.Random.Number(10);
        var cache = new Cache(cacheSize);
        while (cacheSize-- >= 0)
        {
            cache.Set(FakeData.Random.AlphaNumeric(10),
                FakeData.Random.AlphaNumeric(10));
        }
        
        // Act
        var result = cache.Get(FakeData.Random.AlphaNumeric(10));
        
        // Assert
        result.Should().Be(-1);
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
    public void Get_ShouldMoveItem_ToBottomOfRemovalList()
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