public static class ArrayExtension
{
    public static TItem[] Shaffle<TItem>(this TItem[] items, int count, Random rnd)
    {
        var res = items.ToArray();
        
        for (int k = 0; k < count; k++)
        {
            var (i, j) = (rnd.Next(items.Length), rnd.Next(items.Length));
            (res[i], res[j]) = (res[j], res[i]);
        }

        return res;
    }

    public static IEnumerable<TItem> RoundShift<TItem>(this TItem[] items, int shift) 
    {
        return Enumerable.Range(0, items.Length).Select(i => items[(i + shift) % items.Length]);
    }

    public static IEnumerable<TItem> RoundShift<TItem>(this List<TItem> items, int shift)
    {
        return Enumerable.Range(0, items.Count).Select(i => items[(i + shift) % items.Count]);
    }
}
