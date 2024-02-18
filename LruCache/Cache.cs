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

    private int Capacity { get; }
    private LinkedList<string> Items { get; }
    private Dictionary<string, string> HashTable { get; }

    public string Get(string key)
    {
        return HashTable.GetValueOrDefault(key, Null);
    }

        return Null;
    }
}