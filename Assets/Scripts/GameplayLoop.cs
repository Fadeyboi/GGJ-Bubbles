using UnityEngine;

public class GameplayLoop : MonoBehaviour
{
    public GameObject bubble;
    float timeToSpawn = 5f;
    float reduceTimeToSpawn = 0.25f;
    float spawnTimeLimit = 1.25f;
    private float nextSpawnTime = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        
        if (Time.time >= nextSpawnTime)
        {
            if (Bubble.BubbleSpawnCount % 5 == 0 && GameManager.Score != 0){
                SpawnBubble(true);
            } else {
                SpawnBubble(false);
            }
            
            timeToSpawn = Mathf.Max(timeToSpawn - reduceTimeToSpawn, spawnTimeLimit);
            nextSpawnTime = Time.time + timeToSpawn;
        }
    }

    void SpawnBubble(bool teleporting)
{
    // Instantiate the bubble
    GameObject newBubble = Instantiate(bubble, new Vector3(70, 70, 0), Quaternion.identity);
    Bubble.IncrementBubbleSpawned();

    // Access the Bubble component to set the isTeleportBubble property
    Bubble bubbleScript = newBubble.GetComponent<Bubble>();
    if (bubbleScript != null)
    {
        bubbleScript.isTeleportBubble = teleporting;
    }
    else
    {
        Debug.LogError("Bubble script not found on the instantiated bubble.");
    }
}
}
