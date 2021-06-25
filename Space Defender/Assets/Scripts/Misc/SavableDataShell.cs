using System.Collections.Generic;

[System.Serializable]
public class SavableDataShell
{
    public Dictionary<string, int> IntData { get; }
    public Dictionary<string, float> FloatData { get; }
    public Dictionary<string, string> StringData { get; }
    public Dictionary<string, bool> BooleanData { get; }

    public SavableDataShell(Dictionary<string, int> intData,
                            Dictionary<string, float> floatData,
                            Dictionary<string, string> stringData,
                            Dictionary<string, bool> booleanData)
    {
        IntData = intData;
        FloatData = floatData;
        StringData = stringData;
        BooleanData = booleanData;
    }
}
