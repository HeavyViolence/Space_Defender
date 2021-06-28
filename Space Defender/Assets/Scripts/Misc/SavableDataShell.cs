using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class SavableDataShell
{
    [DataMember] public Dictionary<string, int> IntData { get; }
    [DataMember] public Dictionary<string, float> FloatData { get; }
    [DataMember] public Dictionary<string, string> StringData { get; }
    [DataMember] public Dictionary<string, bool> BooleanData { get; }

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
