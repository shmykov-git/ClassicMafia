﻿namespace ClassicMafia.Extensions;

public static class ValueTupleExtensions
{
    public static IEnumerable<(int i, int j)> Range(this (int m, int n) range) =>
        range.SelectRange((i, j) => (i, j));

    public static IEnumerable<TRes> SelectPair<TItem, TRes>(this (TItem[] a, TItem[] b) items, Func<TItem, TItem, TRes> fn) =>
        items.a.SelectMany(x => items.b.Select(y => fn(x, y)));

    public static IEnumerable<(int i, int j)> UniquePairs(this (int m, int n) range) =>
        range.SelectRange((i, j) => (i, j)).Where(v => v.i < v.j);

    public static IEnumerable<T> SelectRange<T>(this (int m, int n) range, Func<int, int, T> selectFn)
    {
        return Enumerable.Range(0, range.m).SelectMany(i => Enumerable.Range(0, range.n).Select(j => selectFn(i, j)));
    }

    public static IEnumerable<T> SelectSnakeRange<T>(this (int m, int n) range, Func<int, int, T> selectFn)
    {
        return range.SelectRange((i, j) => i % 2 == 0 ? selectFn(i, j) : selectFn(i, range.n - j - 1));
    }

    public static IEnumerable<T> SelectRange<T>(this ((int start, int count) m, (int start, int count) n) range, Func<int, int, T> selectFn)
    {
        return Enumerable.Range(range.m.start, range.m.count).SelectMany(i => Enumerable.Range(range.n.start, range.n.count).Select(j => selectFn(i, j)));
    }

    public static IEnumerable<T> SelectMiddleRange<T>(this (int m, int n) range, Func<int, int, T> selectFn)
    {
        return Enumerable.Range(0, range.m).SelectMany(i =>
            Enumerable.Range(0, range.n).Select(j => selectFn(i - range.m / 2, j - range.n / 2)));
    }

    public static IEnumerable<T> SelectRange<T>(this (int m, int n, int l) range, Func<int, int, int, T> selectFn)
    {
        return Enumerable.Range(0, range.m).SelectMany(i => Enumerable.Range(0, range.n).SelectMany(j => Enumerable.Range(0, range.l).Select(k => selectFn(i, j, k))));
    }

    public static IEnumerable<int> Range(this int n, int from = 0) => n.SelectRange(from, i => i);
    public static IEnumerable<T> Range<T>(this int n, Func<int, T> fn) => n.SelectRange(fn);

    public static IEnumerable<T> SelectRange<T>(this int range, Func<int, T> selectFn)
    {
        return Enumerable.Range(0, range).Select(i => selectFn(i));
    }

    public static IEnumerable<T> SelectRange<T>(this int range, int from, Func<int, T> selectFn)
    {
        return Enumerable.Range(from, range).Select(i => selectFn(i));
    }

    public static IEnumerable<T> SelectInterval<T>(this int range, Func<double, T> selectFn, bool isClosed = false)
    {
        var divider = isClosed ? range : range == 1 ? 1 : range - 1;

        return Enumerable.Range(0, range).Select(i => selectFn((double)i / divider));
    }

    public static IEnumerable<T> SelectInterval<T>(this int range, Func<double, int, T> selectFn, bool isClosed = false)
    {
        var divider = isClosed ? range : range == 1 ? 1 : range - 1;

        return Enumerable.Range(0, range).Select(i => selectFn((double)i / divider, i));
    }

    public static IEnumerable<T> SelectClosedInterval<T>(this int range, double to, Func<double, T> selectFn) => range.SelectInterval(0, to, selectFn, true);
    public static IEnumerable<T> SelectClosedInterval<T>(this int range, double to, Func<double, int, T> selectFn) => range.SelectInterval(0, to, selectFn, true);

    public static IEnumerable<T> SelectInterval<T>(this int range, double to, Func<double, T> selectFn, bool isClosed = false)
    {
        var divider = isClosed ? range : range == 1 ? 1 : range - 1;

        return Enumerable.Range(0, range).Select(i => selectFn(i * to / divider));
    }

    public static IEnumerable<T> SelectClosedInterval<T>(this int range, double from, double to, Func<double, T> selectFn) => range.SelectInterval(from, to, selectFn, true);
    public static IEnumerable<T> SelectInterval<T>(this int range, double from, double to, Func<double, T> selectFn, bool isClosed = false)
    {
        var divider = isClosed ? range : range == 1 ? 1 : range - 1;

        return Enumerable.Range(0, range).Select(i => selectFn(from + (to - from) * i / divider));
    }

    public static IEnumerable<T> SelectInterval<T>(this int range, double from, double to, Func<double, int, T> selectFn, bool isClosed = false)
    {
        var divider = isClosed ? range : range == 1 ? 1 : range - 1;

        return Enumerable.Range(0, range).Select(i => selectFn(from + (to - from) * i / divider, i));
    }

    public static IEnumerable<T> SelectInterval<T>(this (int r1, int r2) range, double from1, double to1, double from2, double to2, Func<double, double, T> selectFn, bool isClosed1 = false, bool isClosed2 = false)
    {
        return range.r1.SelectInterval(from1, to1, v => v, isClosed1).SelectMany(x =>
               range.r2.SelectInterval(from2, to2, v => v, isClosed2).Select(y => selectFn(x, y)));
    }

    public static IEnumerable<T> SelectSquarePoints<T>(this int range, Func<int, double, double, T> selectFn)
    {
        var size = (int)Math.Sqrt(range - 1) + 1;
        double sizeD = size;

        return Enumerable.Range(0, range).Select(i => selectFn(i, i % size + 0.5 - 0.5 * sizeD, size - i / size - 0.5 - 0.5 * sizeD));
    }

    public static IEnumerable<T> SelectCircleAngle<T>(this int range, Func<int, double, T> selectFn)
    {
        return range.SelectRange(i => selectFn(i, 2 * Math.PI * i / range));
    }

    public static IEnumerable<T> SelectCirclePoints<T>(this int range, Func<int, double, double, double, T> selectFn)
    {
        return range.SelectCircleAngle((i, fi) => selectFn(i, fi, Math.Cos(fi), Math.Sin(fi)));
    }

    public static IEnumerable<T> SelectCirclePoints<T>(this int range, Func<int, double, double, T> selectFn)
    {
        return range.SelectCircleAngle((i, fi) => selectFn(i, Math.Cos(fi), Math.Sin(fi)));
    }

    public static void ForEach(this int range, Action<int> action)
    {
        Enumerable.Range(0, range).ForEach(action);
    }

    public static void ForEach(this int range, Action action)
    {
        Enumerable.Range(0, range).ForEach(_ => action());
    }

    public static IEnumerable<T> SelectMiddleRange<T>(this int range, Func<int, T> selectFn)
    {
        return Enumerable.Range(0, range).Select(i => selectFn(i - range / 2));
    }

    public static (int i, int j) OrderedEdge(this (int i, int j) e)
    {
        return e.i < e.j ? e : (e.j, e.i);
    }

    public static int[] EdgeToArray(this (int i, int j) e)
    {
        return new[] { e.i, e.j };
    }

    public static (int i, int j) Add(this (int i, int j) a, (int i, int j) b)
    {
        return (a.i + b.i, a.j + b.j);
    }

    public static (int i, int j) Sub(this (int i, int j) a, (int i, int j) b)
    {
        return (a.i - b.i, a.j - b.j);
    }

    public static int Mult(this (int i, int j) a, (int i, int j) b)
    {
        return a.i * b.i + a.j * b.j;
    }

    public static (int i, int j) Normal(this (int i, int j) a)
    {
        return (a.j, -a.i);
    }

    public static (int i, int j) ReversedEdge(this (int i, int j) e)
    {
        return (e.j, e.i);
    }

    public static (int i, int j) Reverse(this (int i, int j) e)
    {
        return (e.j, e.i);
    }

    public static int Another(this (int i, int j) e, int k)
    {
        return e.i == k ? e.j : e.i;
    }

    public static string ToStr(this ((double r, double i) c, double k)[] args)
    {
        return string.Join(", ", args.Select(a => $"(({a.c.r}, {a.c.i}), {a.k})"));
    }
}
