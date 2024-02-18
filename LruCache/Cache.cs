namespace LruCache;

public class Cache
{
    public Cache(int capacity)
    {
        Capacity = capacity;
        LastUsed = new LinkedList<string>();
        Items = new Dictionary<string, string>(capacity);
    }

    private int Capacity { get; }
    private LinkedList<string> LastUsed { get; }
    private Dictionary<string, string> Items { get; }

    public string? Get(string key)
    {
        var value = Items.GetValueOrDefault(key);
        if (value is null) return null;
        
        LastUsed.Remove(key);
        LastUsed.AddFirst(key);

        return value;
    }

    public void Set(string key, string value)
    {
        if (Items.Count >= Capacity)
        {
            Items.Remove(LastUsed.Last!.Value);
            LastUsed.RemoveLast();
        }

        Items.Add(key, value);
        LastUsed.AddFirst(key);
    }
}