using UnityEngine;

public class ScoreSaveHandler : MonoBehaviour, ISaveable
{



    public void SaveData(ref SavedData data)
    {
        data.score = ScoreManager.score;
    }

    public void LoadData(SavedData data)
    {
        ScoreManager.score = data.score;
    }

}