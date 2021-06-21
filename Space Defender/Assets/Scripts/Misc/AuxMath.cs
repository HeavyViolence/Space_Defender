using System.Collections.Generic;
using UnityEngine;

public static class AuxMath
{
    public const float E = 2.71828182f;
    public const float Phi = 1.61803398f;

    public static float Randomize(float value, float factor)
    {
        float clampedFactor = Mathf.Clamp01(factor);
        float seed = Random.Range(1f - clampedFactor, 1f + clampedFactor);

        return value * seed;
    }

    public static float RandomizeUp(float value, float factor)
    {
        float clampedFactor = Mathf.Clamp01(factor);
        float seed = Random.Range(1f, 1f + clampedFactor);

        return value * seed;
    }

    public static float RandomizeDown(float value, float factor)
    {
        float clampedFactor = Mathf.Clamp01(factor);
        float seed = Random.Range(1f - clampedFactor, 1f);

        return value * seed;
    }

    public static float GetHighestRandom(float value, float factor)
    {
        float clampedFactor = Mathf.Clamp01(factor);

        return value * (1f + clampedFactor);
    }

    public static float GetLowestRandom(float value, float factor)
    {
        float clampedFactor = Mathf.Clamp01(factor);

        return value * (1f - clampedFactor);
    }

    public static int GetRandomInRangeWithExceptions(int minInclusive, int maxExclusive, params int[] exceptions)
    {
        HashSet<int> excludedNumbers = new HashSet<int>(exceptions);

        int availableNumbersAmount = maxExclusive - minInclusive - excludedNumbers.Count;
        List<int> availableNumbers = new List<int>(availableNumbersAmount);

        for (int i = minInclusive; i < maxExclusive; i++)
        {
            if (excludedNumbers.Contains(i)) continue;
            
            availableNumbers.Add(i);
        }

        int index = Random.Range(0, availableNumbers.Count);

        return availableNumbers[index];
    }

    public static bool ValueWithinRange(float value, float min, float max)
    {
        if (value < max - Mathf.Epsilon && value > min + Mathf.Epsilon) return true;
        else return false;
    }

    public static float Remap(float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        float clampedValue = Mathf.Clamp(value, oldMin, oldMax);
        float t = Mathf.InverseLerp(oldMin, oldMax, clampedValue);

        return Mathf.Lerp(newMin, newMax, t);
    }

    public static float RandomSign => Random.Range(0, int.MaxValue) % 2 == 0 ? 1f : -1f;

    public static bool RandomBoolean => Random.Range(0, int.MaxValue) % 2 == 0;
}
