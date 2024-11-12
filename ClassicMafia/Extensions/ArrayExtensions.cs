namespace ClassicMafia.Extensions;

public static class ArrayExtensions
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

    public static TItem?[] Stretch<TItem>(this TItem[] values, int len) where TItem : struct
    {
        var res = new TItem?[len];

        if (values.Length == 0)
            return res;

        if (values.Length == 1)
        {
            res[len / 2] = values[0];
            return res;
        }

        values.Select((v, i) => (v, j: (int)Math.Round((len - 1.0) * i / (values.Length - 1))))
        .ForEach(v => res[v.j] = v.v);

        return res;
    }

    public static void Shaffle<TItem>(this TItem[] values, Random rnd) => values.Shaffle(3 * values.Length + 7, rnd);

    public static int[] ReverseReverses(this int[] reverses)
    {
        return reverses.Length.Range(j => (i: reverses[j], j)).OrderBy(v => v.i).Select(v => v.j).ToArray();
    }

    /// <summary>
    /// From what ordered array these values can be get by forward reverses
    /// </summary>
    public static int[] ToBackReverses(this IEnumerable<int> values)
    {
        return values.Select((v, i) => (i, v)).OrderBy(v => v.v).Select(v => v.i).ToArray();
    }

    /// <summary>
    /// How to reverse values to get ordered array
    /// </summary>
    public static int[] ToReverses(this IEnumerable<int> values)
    {
        return values.ToBackReverses().ReverseReverses();
    }

    public static int[] JoinReverses(this int[] reversesA, int[] reversesB)
    {
        return reversesB.Select(i => reversesA[i]).ToArray();
    }

    public static int[] ToTranslateReverses(this IEnumerable<int> valuesA, IEnumerable<int> valuesB)
    {
        return valuesA.ToBackReverses().JoinReverses(valuesB.ToReverses());
    }

    public static int[] ToBackTranslateReverses(this IEnumerable<int> valuesA, IEnumerable<int> valuesB)
    {
        return valuesB.ToBackReverses().JoinReverses(valuesA.ToBackReverses().ReverseReverses());
    }

    /// <summary>
    /// What order values should be placed
    /// </summary>
    public static TItem[] ReverseForward<TItem>(this TItem[] values, int[] reverses)
    {
        return reverses.Select(i => values[i]).ToArray();
    }

    /// <summary>
    /// From what order values were placed to this
    /// </summary>
    public static TItem[] ReverseBack<TItem>(this TItem[] values, int[] backReverses)
    {
        return values.ReverseForward(backReverses.ReverseReverses());
    }
}
