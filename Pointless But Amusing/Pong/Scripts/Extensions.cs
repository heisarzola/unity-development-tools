using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static class Extensions
{
    // e.g. 42.Range().ForEach...
    public static IEnumerable<int> Range(this int to, int from = 0)
    {
        return Enumerable.Range(from, to);
    }

    public static void ForEach<T>(this IEnumerable<T> enumeration, System.Action<T> action)
    {
        foreach (T item in enumeration)
        {
            action(item);
        }
    }

    public static Vector2 Rotate(this Vector2 v, float angle)
    {
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float tx = v.x;
        v.x = cos * tx - sin * v.y;
        v.y = sin * tx + cos * v.y;
        return v;
    }

    public static Vector2 MoveAngle(this Vector2 v, float angle, float speed)
    {
        v.x += Mathf.Cos(angle * Mathf.Deg2Rad) * speed;
        v.y += Mathf.Sin(angle * Mathf.Deg2Rad) * speed;
        return v;
    }

    public static float Wrap(this float value, float min = 0, float max = 1)
    {
        var w = max - min;
        var om = value - min;
        return om >= 0 ? om % w + min : w + om % w + min;
    }

    public static int Wrap(this int value, int min = 0, int max = 1)
    {
        var w = max - min;
        var om = value - min;
        return om >= 0 ? om % w + min : w + om % w + min;
    }

    public static float Rand(this float to, float from = 0)
    {
        return Random.value * (to - from) + from;
    }

    public static float RandPM(this float to)
    {
        return Random.value * to * 2 - to;
    }

    public static int Rand(this int to, int from = 0)
    {
        return Mathf.FloorToInt(Random.value * (to - from)) + from;
    }
}
