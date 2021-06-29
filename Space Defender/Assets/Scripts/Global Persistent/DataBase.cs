using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using System;

public class DataBase : GlobalSingleton<DataBase>
{
    public const string SaveFileName = "SpaceDefenderSavedData";
    public const string SaveFileExtension = ".save";

    private Dictionary<string, int> _intData = new Dictionary<string, int>();
    private Dictionary<string, float> _floatData = new Dictionary<string, float>();
    private Dictionary<string, string> _stringData = new Dictionary<string, string>();
    private Dictionary<string, bool> _booleanData = new Dictionary<string, bool>();

    public event EventHandler Loaded;

    public string SavePath => Path.Combine(Application.persistentDataPath,
                                           SaveFileName + SaveFileExtension);

    protected override void Awake()
    {
        base.Awake();

        LoadDataFromHardDrive();
    }

    private void SaveDataToHardDrive()
    {
        using var writer = new FileStream(SavePath, FileMode.Create);
        var shell = new SavableDataShell(_intData, _floatData, _stringData, _booleanData);
        var serializer = new DataContractSerializer(typeof(SavableDataShell));

        serializer.WriteObject(writer, shell);
    }

    private void LoadDataFromHardDrive()
    {
        if (File.Exists(SavePath))
        {
            using var reader = new FileStream(SavePath, FileMode.Open);
            var serializer = new DataContractSerializer(typeof(SavableDataShell));
            var shell = (SavableDataShell)serializer.ReadObject(reader);

            _intData = shell.IntData;
            _floatData = shell.FloatData;
            _stringData = shell.StringData;
            _booleanData = shell.BooleanData;

            OnLoaded();
        }
    }

    private void OnLoaded()
    {
        Loaded?.Invoke(this, EventArgs.Empty);
    }

    public void SaveInt(string key, int value)
    {
        if (_intData.ContainsKey(key))
            _intData.Remove(key);

        _intData.Add(key, value);

        SaveDataToHardDrive();
    }

    public bool TryGetInt(string key, out int value) =>
        _intData.TryGetValue(key, out value);

    public void SaveFloat(string key, float value)
    {
        if (_floatData.ContainsKey(key))
            _floatData.Remove(key);

        _floatData.Add(key, value);

        SaveDataToHardDrive();
    }

    public bool TryGetFloat(string key, out float value) =>
        _floatData.TryGetValue(key, out value);

    public void SaveString(string key, string value)
    {
        if (_stringData.ContainsKey(key))
            _stringData.Remove(key);

        _stringData.Add(key, value);

        SaveDataToHardDrive();
    }

    public bool TryGetString(string key, out string value) =>
        _stringData.TryGetValue(key, out value);

    public void SaveBoolean(string key, bool value)
    {
        if (_booleanData.ContainsKey(key))
            _booleanData.Remove(key);

        _booleanData.Add(key, value);

        SaveDataToHardDrive();
    }

    public bool TryGetBoolean(string key, out bool value) =>
        _booleanData.TryGetValue(key, out value);
}
