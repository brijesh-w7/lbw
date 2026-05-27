using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private List<ISaveable> saveables;
    private FileDataHandler dataHandler;

    private SavedData gameData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        dataHandler = new FileDataHandler(Application.persistentDataPath, "savegame.json");
    }

    private void Start()
    {
        saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToList(); LoadGame();
    }

    public void SaveGame()
    {
        gameData = new SavedData();

        foreach (ISaveable saveable in saveables)
        {
            saveable.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            return;
        }

        foreach (ISaveable saveable in saveables)
        {
            saveable.LoadData(gameData);
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void OnPlayerDeath()
    {
        gameData.playerData = null;
        gameData.enemies = null;
        SaveGame();
    }
}
