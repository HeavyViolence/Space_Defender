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

    public static float RandomCeil(float value, float factor)
    {
        float clampedFactor = Mathf.Clamp01(factor);

        return value * (1f + clampedFactor);
    }

    public static float RandomFloor(float value, float factor)
    {
        float clampedFactor = Mathf.Clamp01(factor);

        return value * (1f - clampedFactor);
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
}
