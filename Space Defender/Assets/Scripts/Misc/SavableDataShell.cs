using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class SavableDataShell
{
    [DataMember] public Dictionary<string, int> IntData { get; private set; }
    [DataMember] public Dictionary<string, float> FloatData { get; private set; }
    [DataMember] public Dictionary<string, string> StringData { get; private set; }
    [DataMember] public Dictionary<string, bool> BooleanData { get; private set; }

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
