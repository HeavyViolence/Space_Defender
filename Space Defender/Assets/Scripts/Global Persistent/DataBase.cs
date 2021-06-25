using System.Collections.Generic;

public class DataBase : GlobalSingleton<DataBase>
{
    private Dictionary<string, int> _intData = new Dictionary<string, int>();
    private Dictionary<string, float> _floatData = new Dictionary<string, float>();
    private Dictionary<string, string> _stringData = new Dictionary<string, string>();
    private Dictionary<string, bool> _booleanData = new Dictionary<string, bool>();

    public void SaveInt(string key, int value)
    {
        if (_intData.ContainsKey(key))
            _intData.Remove(key);

        _intData.Add(key, value);
    }

    public bool TryGetInt(string key, out int value) =>
        _intData.TryGetValue(key, out value);

    public void SaveFloat(string key, float value)
    {
        if (_floatData.ContainsKey(key))
            _floatData.Remove(key);

        _floatData.Add(key, value);
    }

    public bool TryGetFloat(string key, out float value) =>
        _floatData.TryGetValue(key, out value);

    public void SaveString(string key, string value)
    {
        if (_stringData.ContainsKey(key))
            _stringData.Remove(key);

        _stringData.Add(key, value);
    }

    public bool TryGetString(string key, out string value) =>
        _stringData.TryGetValue(key, out value);

    public void SaveBoolean(string key, bool value)
    {
        if (_booleanData.ContainsKey(key))
            _booleanData.Remove(key);

        _booleanData.Add(key, value);
    }

    public bool TryGetBoolean(string key, out bool value) =>
        _booleanData.TryGetValue(key, out value);

    public SavableDataShell GetData() =>
        new SavableDataShell(_intData, _floatData, _stringData, _booleanData);

    public void SetData(SavableDataShell data)
    {
        _intData = data.IntData;
        _floatData = data.FloatData;
        _stringData = data.StringData;
        _booleanData = data.BooleanData;
    }
}
