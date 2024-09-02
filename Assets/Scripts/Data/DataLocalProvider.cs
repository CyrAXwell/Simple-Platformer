using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

public class DataLocalProvider : IDataProvider
{
    private const string FileName = "PlayerSave";
    private const string SaveFileExension = ".json";
    private string SavePath => Application.persistentDataPath;
    private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExension}");
    private IPersistentData _persistentData;

    [Inject]
    public DataLocalProvider(IPersistentData persistentData)
    {
        _persistentData = persistentData;
        Debug.Log("DataLocalProvider");
        LoadDataOrInit();
        Debug.Log(_persistentData.PlayerData);
    }

    public bool TryLoad()
    {
        if (IsDataAlreadyExist() == false)
            return false;

        _persistentData.PlayerData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(FullPath));
        return true;
    }

    public void Save()
    {
        File.WriteAllText(FullPath, JsonConvert.SerializeObject(_persistentData.PlayerData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }

    private bool IsDataAlreadyExist() => File.Exists(FullPath);

    private void LoadDataOrInit()
    {
        if (TryLoad() == false)
            _persistentData.PlayerData = new PlayerData();
    }
}
