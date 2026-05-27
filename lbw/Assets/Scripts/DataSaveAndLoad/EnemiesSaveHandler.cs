using UnityEngine;
using System.Collections.Generic;

public class EnemiesSaveHandler : MonoBehaviour, ISaveable
{

    [SerializeField] private EnemyManager[] enemyManagerList;
    [SerializeField] private PlayerHealth player;


    public void SaveData(ref SavedData data)
    {
        if (player.IsDead)
        {
            data.enemies = null;
        }
        else
        {
            Dictionary<string, List<SavedData.EnemyItemSaved>> enemyDict;

            if (data.enemies == null)
            {
                enemyDict = new Dictionary<string, List<SavedData.EnemyItemSaved>>();
            }
            else
            {
                data.enemies.Clear();
                enemyDict = data.enemies;
            }

            List<EnemyHealth> enemies = new List<EnemyHealth>(FindObjectsByType<EnemyHealth>());

            SavedData.EnemyItemSaved saveItem;

            foreach (var enemy in enemies)
            {
                if (!enemy.IsDead)
                {
                    saveItem = new SavedData.EnemyItemSaved();

                    saveItem.position = new MyVector3(enemy.transform.position);
                    saveItem.localEulerAngles = new MyVector3(enemy.transform.localEulerAngles);
                    saveItem.localScale = new MyVector3(enemy.transform.localScale);

                    saveItem.currentHealth = enemy.CurrentHealth;

                    if (enemyDict.ContainsKey(enemy.enemyManagerKey)) enemyDict[enemy.enemyManagerKey].Add(saveItem);
                    else enemyDict.Add(enemy.enemyManagerKey, new List<SavedData.EnemyItemSaved>() { saveItem });



                }
            }
            data.enemies = enemyDict;
        }
    }


    public void LoadData(SavedData data)
    {

        if (data != null && data.playerData != null && data.enemies != null && !data.playerData.isDead)
        {
            EnemyManager manager;
            GameObject spawned;
            string prefabKey;
            if (data.enemies != null && data.enemies.Count > 0)
            {
                foreach (var entry in data.enemies)
                {
                    prefabKey = entry.Key;
                    manager = GetEnemyManager(prefabKey);
                    foreach (var dataItem in entry.Value)
                    {
                        spawned = manager.Spawn();
                        spawned.transform.position = SavedData.GetVector3(dataItem.position);
                        spawned.transform.localEulerAngles = SavedData.GetVector3(dataItem.localEulerAngles);
                        spawned.transform.localScale = SavedData.GetVector3(dataItem.localScale);

                        spawned.GetComponent<EnemyHealth>().SetCurrentHealthfromSaveData(dataItem.currentHealth);
                    }
                }
            }
        }
        foreach (var manager in enemyManagerList)
        {
            manager.StartSpawning();
        }
    }

    EnemyManager GetEnemyManager(string prefabKey)
    {
        foreach (var manager in enemyManagerList)
        {
            if (prefabKey.Equals(manager.enemy.name)) return manager;
        }
        return null;

    }

}
