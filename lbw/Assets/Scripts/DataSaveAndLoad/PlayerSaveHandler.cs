using UnityEngine;

public class PlayerSaveHandler : MonoBehaviour, ISaveable
{
    [SerializeField] private PlayerHealth playerHealth;


    public void SaveData(ref SavedData data)
    {
        if (playerHealth.IsDead)
        {
            data.playerData = null;
        }
        else
        {
            var playerData = data.playerData;
            if (playerData == null) playerData = new SavedData.PlayerItemSaved();
            playerData.isDead = playerHealth.IsDead;

            playerData.position = new MyVector3(playerHealth.transform.position);
            playerData.localEulerAngles = new MyVector3(playerHealth.transform.localEulerAngles);
            playerData.localScale = new MyVector3(playerHealth.transform.localScale);

            playerData.currentHealth = playerHealth.CurrentHealth;

            data.playerData = playerData;
        }
    }

    public void LoadData(SavedData data)
    {
        if (data != null && data.playerData != null && !data.playerData.isDead)
        {
            var playerData = data.playerData;

            playerHealth.transform.position = SavedData.GetVector3(playerData.position);

            playerHealth.transform.localEulerAngles = SavedData.GetVector3(playerData.localEulerAngles);

            playerHealth.transform.localScale = SavedData.GetVector3(playerData.localScale);

            playerHealth.SetCurrentHealthfromSaveData(playerData.currentHealth);
        }
    }

}