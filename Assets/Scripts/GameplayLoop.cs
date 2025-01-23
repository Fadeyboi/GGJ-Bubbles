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
                int prop = Random.Range(0, 2);
                if (prop == 0)
                {
                    SpawnBubble(false, true);
                }
                else if (prop == 1)
                {
                    SpawnBubble(true, false);
                }
            } else {
                SpawnBubble(false, false);
            }
            
            timeToSpawn = Mathf.Max(timeToSpawn - reduceTimeToSpawn, spawnTimeLimit);
            nextSpawnTime = Time.time + timeToSpawn;
        }
    }

    void SpawnBubble(bool teleporting, bool randomizing)
{
    // Instantiate the bubble
    GameObject newBubble = Instantiate(bubble, new Vector3(70, 70, 0), Quaternion.identity);
    Bubble.IncrementBubbleSpawned();

    // Access the Bubble component to set the isTeleportBubble property
    Bubble bubbleScript = newBubble.GetComponent<Bubble>();
    if (bubbleScript != null)
    {
            bubbleScript.isTeleportBubble = teleporting;
            bubbleScript.isRandomizedBubble = randomizing;

    }
    else
    {
        Debug.LogError("Bubble script not found on the instantiated bubble.");
    }
}
}
