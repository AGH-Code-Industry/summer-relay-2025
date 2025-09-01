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

    public static int ReplaceDigitAtIndex(int number, int index, int newDigit)
    {
        int length = (int)Math.Floor(Math.Log10(number)) + 1;
        int posFromRight = length - 1 - index;
        int pow = (int)Math.Pow(10, posFromRight);

        // Current digit at that position
        int currentDigit = (number / pow) % 10;

        // Remove current digit and add new one
        number -= currentDigit * pow;
        number += newDigit * pow;

        return number;
    }
    public static T GetRandomEnumValue<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    public static T GetRandomEnumValue<T>(int startFrom)
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(startFrom, values.Length));
    }


    static public bool Contains2D(UnityEngine.Bounds b, Vector2 p)
    {
        Vector3 min = b.min;
        Vector3 max = b.max;

        return p.x >= min.x && p.x <= max.x &&
                        p.y >= min.y && p.y <= max.y;
    }
}