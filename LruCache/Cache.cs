namespace LruCache;

public class Cache
{
    private const string Null = "null";
    
    public Cache(int capacity)
    {
        Capacity = capacity;
        Items = new LinkedList<string>();
        HashTable = new Dictionary<int, string>(capacity);
    }

    public int Capacity { get; }
    public LinkedList<string> Items { get; }
    public Dictionary<int, string> HashTable { get; }

    public string Get(string key)
    {
        if (HashTable.TryGetValue(key.GetHashCode(), out var value))
        {
            return value;
        }

        return Null;
    }
}