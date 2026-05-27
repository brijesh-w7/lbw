using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private void Awake()
    {
    }

    void Start()
    {
        enemy.GetComponent<EnemyHealth>().enemyManagerKey = enemy.name;

        // InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    public void StartSpawning()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);

    }

    public GameObject Spawn()
    {
        if (playerHealth.CurrentHealth <= 0f)
        {
            return null;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        return Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

}
