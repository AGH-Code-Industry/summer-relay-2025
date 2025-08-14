using System;
using System.Drawing;
using UnityEngine;

static class UtilFunctions
{
    static public int GetDigitAtIndex(int number, int index)
    {
        number = Math.Abs(number);

        int length = (int)Math.Log10(number) + 1;

        if (index < 0 || index >= length)
            return -1;

        int divisor = 1;
        for (int i = 0; i < length - index - 1; i++)
        {
            divisor *= 10;
        }

        return (number / divisor) % 10;
    }

    static public bool Contains2D(UnityEngine.Bounds b, Vector2 p)
    {
        Vector3 min = b.min;
        Vector3 max = b.max;

        return p.x >= min.x && p.x <= max.x &&
                        p.y >= min.y && p.y <= max.y;
    }
}