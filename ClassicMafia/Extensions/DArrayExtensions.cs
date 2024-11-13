namespace ClassicMafia.Extensions;

public static class CharExtensions
{
    public static bool IsRed(this char c) => c == 'К';
    public static bool IsBlack(this char c) => c == 'Ч';
}

public static class DArrayExtensions
{
    public static void Debug<T>(this T[][] values, int shift = 3)
    {
        System.Diagnostics.Debug.Write(" ".PadLeft(shift));
        for (var j = 0; j < values[0].Length; j++)
            System.Diagnostics.Debug.Write(j.ToString().PadLeft(shift));

        for (var i = 0; i < values.Length; i++)
        {
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.Write(i.ToString().PadLeft(shift));

            for (var j = 0; j < values[0].Length; j++)
            {
                System.Diagnostics.Debug.Write(values[i][j]?.ToString()?.PadLeft(shift));
            }
        }

        System.Diagnostics.Debug.WriteLine("");
    }
}
