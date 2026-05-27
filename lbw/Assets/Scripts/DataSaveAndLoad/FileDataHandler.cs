using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class FileDataHandler
{
    private string dataDirPath;
    private string dataFileName;

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public void Save(SavedData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            Debug.Log("save: " + json);
            File.WriteAllText(fullPath, json);

            Debug.Log("Game Saved");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Save Failed: " + e);
        }
    }

    public SavedData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (!File.Exists(fullPath))
        {
            Debug.Log("No Save File Found");
            return null;
        }

        try
        {
            string json = File.ReadAllText(fullPath);

            return JsonConvert.DeserializeObject<SavedData>(json);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Load Failed: " + e);
            return null;
        }
    }
}