using UnityEngine;

public interface ISaveable
{
    void SaveData(ref SavedData data);
    void LoadData(SavedData data);
}
