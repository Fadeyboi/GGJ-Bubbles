using UnityEngine;

public class GameplayLoop : MonoBehaviour
{
    public GameObject bubble;
    float timeToSpawn = 5f;
    float reduceTimeToSpawn = 0.2f;
    float spawnTimeLimit = 1f;
    private float nextSpawnTime = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        
        if (Time.time >= nextSpawnTime)
        {
            SpawnBubble();
            timeToSpawn = Mathf.Max(timeToSpawn - reduceTimeToSpawn, spawnTimeLimit);
            nextSpawnTime = Time.time + timeToSpawn;
        }
    }

    void SpawnBubble(){
        Instantiate(bubble, new Vector3(70, 70, 0), Quaternion.identity);
    }
}
