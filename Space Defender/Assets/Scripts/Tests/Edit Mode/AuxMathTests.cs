using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AuxMathTests
{
    [Test]
    public void ValueWithinRange_ValueInsideOfRangeReturnsTrue()
    {
        float value = 2f;
        float min = 1f;
        float max = 3f;

        bool withinRange = AuxMath.ValueWithinRange(value, min, max);

        Assert.AreEqual(true, withinRange);
    }

    [Test]
    public void ValueWithinRange_ValueOutsideOfRangeReturnsFalse()
    {
        float value = 2f;
        float min = 3f;
        float max = 4f;

        bool withinRange = AuxMath.ValueWithinRange(value, min, max);

        Assert.AreEqual(false, withinRange);
    }

    [Test]
    public void ValueWithinRange_BoundaryValueReturnsFalse()
    {
        float value = 2f;
        float min = 2f;
        float max = 3f;

        bool withinRange = AuxMath.ValueWithinRange(value, min, max);

        Assert.AreEqual(false, withinRange);
    }

    [Test]
    public void ValueWithinRange_SingularityReturnsFalse()
    {
        float value = 2f;
        float min = 2f;
        float max = 2f;

        bool withinRange = AuxMath.ValueWithinRange(value, min, max);

        Assert.AreEqual(false, withinRange);
    }

    [Test]
    public void ValueWithinRange_InvertedRangeReturnsFalse()
    {
        float value = 2f;
        float min = 3f;
        float max = 1f;

        bool withinRange = AuxMath.ValueWithinRange(value, min, max);

        Assert.AreEqual(false, withinRange);
    }

    [Test]
    public void Randomize_RandomFactorClampsTo01()
    {
        float value = 2f;
        float factor = Random.Range(float.MinValue, float.MaxValue);
        float result = AuxMath.Randomize(value, factor);

        bool clamped = AuxMath.ValueWithinRange(result, 0f, 4f);

        Assert.AreEqual(true, clamped);
    }

    [Test]
    public void Randomize_NegativeFactorClampsTo0()
    {
        float value = 2f;
        float factor = -67.12f;

        float result = AuxMath.Randomize(value, factor);

        Assert.AreEqual(value, result);
    }

    [Test]
    public void Randomize_FactorGreaterThen1ClampsTo1()
    {
        float value = 2f;
        float factor = Random.Range(1f + Mathf.Epsilon, float.MaxValue);
        float result = AuxMath.Randomize(value, factor);

        bool clamped = AuxMath.ValueWithinRange(result, 0f, 4f);

        Assert.AreEqual(true, clamped);
    }

    [Test]
    public void Remap_LinearRemappingIsCorrect()
    {
        float value = 15f;
        float oldMin = 10f;
        float oldMax = 20f;
        float newMin = 100f;
        float newMax = 200f;

        float remappedValue = AuxMath.Remap(value, oldMin, oldMax, newMin, newMax);

        Assert.AreEqual(150f, remappedValue);
    }
}
