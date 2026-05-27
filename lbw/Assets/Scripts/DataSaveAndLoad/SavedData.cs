using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class MyVector3
{
    public float x, y, z;

    public MyVector3() { }
    public MyVector3(Vector3 v3)
    {

        x = v3.x;
        y = v3.y;
        z = v3.z;
    }
}
[Serializable]
public class SavedData
{
    //------------------------------------------------------------------------------------------------------------------------------------------------
    public int score = 0;
    public PlayerItemSaved playerData = new PlayerItemSaved();
    public Dictionary<string, List<SavedData.EnemyItemSaved>> enemies = new Dictionary<string, List<SavedData.EnemyItemSaved>>();


    //------------------------------------------------------------------------------------------------------------------------------------------------
    public static Vector3 GetVector3(MyVector3 v3)
    {

        return new Vector3(v3.x, v3.y, v3.z);
    }

    [Serializable]
    public class CharacterSavedDatabase
    {
        public MyVector3 position;
        public MyVector3 localEulerAngles;
        public MyVector3 localScale;

        public int currentHealth;
    }

    [Serializable]
    public class EnemyItemSaved : CharacterSavedDatabase
    {
    }

    [Serializable]
    public class PlayerItemSaved : CharacterSavedDatabase
    {
        public bool isDead;
    }



}
