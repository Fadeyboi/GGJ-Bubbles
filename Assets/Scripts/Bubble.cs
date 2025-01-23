using UnityEngine;
using TMPro;
using System.Collections;

public class Bubble : MonoBehaviour
{
    public float swayAmplitude;
    public float swaySpeed;
    private float initialX;
    public float speedFactor;
    string[] types = { "Apple", "Cherry", "Melon", "Banana" };
    public string bubbleType;
    public Sprite appleSprite;
    public Sprite cherrySprite;
    public Sprite melonSprite;
    public Sprite bananaSprite;
    int randomColumn;

    public bool isTeleportBubble;
    public bool teleportedBefore;
    public float teleportThreshold;
    private bool isFrozen;

     

    private SpriteRenderer spriteRenderer;
    public bool isRandomizedBubble;
    public float RandomizedThreshold; 



    public static int BubbleSpawnCount { get; private set; }

    // 1) Add your pop sound
    public AudioClip[] popSounds;


    public GameObject popEffect;  // <-- Add this

    void Start()
    {
        isFrozen = false;
        isTeleportBubble = false;
        teleportedBefore = false;
        teleportThreshold = 1f;
        speedFactor = -1.8f;
        swaySpeed = 5f;
        swayAmplitude = 0.2f;
        isRandomizedBubble = false;
        RandomizedThreshold = 1f; 
        SpawnObjectRandomly();
        bubbleType = types[Random.Range(0, types.Length)];
        Debug.Log($"Assigned type: {bubbleType}");
        AddSpriteToBubble();
        
    }

    void Update()
    {

            BubbleSwayWithGravity();


        // Handle teleportation for special bubbles
        if (isTeleportBubble && transform.position.y <= teleportThreshold)
        {
            TeleportToNewColumn();
        }

        if (isRandomizedBubble)
        {
            StartCoroutine(RandomizeSprite());
        }
    }

    void SpawnObjectRandomly()
    {
        float screenWidth = Screen.width;
        float columnWidth = screenWidth / 4f;

        randomColumn = Random.Range(0, 4);

        float columnCenterX = (randomColumn * columnWidth) + (columnWidth / 2f);

        Vector3 screenPosition = new Vector3(columnCenterX, Screen.height + 40, 0);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        worldPosition.z = 0;

        this.transform.position = worldPosition;
        initialX = transform.position.x;
    }

    void BubbleSwayWithGravity()
    {
        // Simulate swaying in the X-axis
        float newX = initialX + Mathf.Sin(Time.time * swaySpeed) * swayAmplitude;

        float newY = transform.position.y;
        // Simulate gravity-like effect for Y-axis
        if(!isFrozen){newY = transform.position.y + speedFactor * Time.deltaTime;}
        

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    void AddSpriteToBubble()
    {
        // Create a child GameObject for the sprite
        GameObject spriteObject = new GameObject("BubbleTypeSprite");
        spriteObject.transform.SetParent(this.transform);

        // Add a SpriteRenderer to the child object
        spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();

        // Assign the sprite based on the bubble type
        UpdateSprite();

        // Adjust the sprite's position relative to the bubble
        spriteObject.transform.localPosition = Vector3.zero;
        spriteObject.transform.localScale = new Vector3(3f, 3f, 1f); // Adjust scale if needed

        // Set the sorting order
        spriteRenderer.sortingOrder = 1;
    }


    void UpdateSprite()
    {
        switch (bubbleType)
        {
            case "Apple":
                spriteRenderer.sprite = appleSprite;
                break;
            case "Cherry":
                spriteRenderer.sprite = cherrySprite;
                break;
            case "Melon":
                spriteRenderer.sprite = melonSprite;
                break;
            case "Banana":
                spriteRenderer.sprite = bananaSprite;
                break;
        }
    }

    IEnumerator RandomizeSprite()
    {
        while (transform.position.y > RandomizedThreshold)
        {
            if (PauseManager.GlobalIsPaused || GameManager.GlobalGameEnded)
        {
            break;
        }
            bubbleType = types[Random.Range(0, types.Length)];
            UpdateSprite();
            yield return new WaitForSeconds(1f); // Change sprite every second
        }
    }

    void TeleportToNewColumn()
    {
        if (teleportedBefore)
        {
            return;
        }
        teleportedBefore = true;

        float screenWidth = Screen.width;
        float columnWidth = screenWidth / 4f;

        // 1) Get the bubble's current position in screen space
        Vector3 currentScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        // 2) Determine the current column based on screen-space X
        int currentColumn = Mathf.FloorToInt(currentScreenPos.x / columnWidth);

        // 3) Choose a random column that is different from the current one
        int newColumn;
        do
        {
            newColumn = Random.Range(0, 4);
        } while (newColumn == currentColumn);

        // 4) Calculate the new screen-space X for the chosen column
        float newColumnCenterX = (newColumn * columnWidth) + (columnWidth / 2f);

        // 5) Construct a new screen-space position 
        Vector3 newScreenPos = new Vector3(newColumnCenterX, currentScreenPos.y, 0);

        // 6) Convert that screen-space position back to world space
        Vector3 newWorldPos = Camera.main.ScreenToWorldPoint(newScreenPos);
        newWorldPos.z = 0; // Keep Z at 0

        // 7) Update position and re-initialize initialX for swaying
        transform.position = newWorldPos;
        initialX = newWorldPos.x;

        Debug.Log($"Special bubble teleported from column {currentColumn} to column {newColumn}");

        // Freeze the bubble's movement for X seconds
        StartCoroutine(PauseMovement(0.25f));
    }

    IEnumerator PauseMovement(float duration)
    {
        isFrozen = true;             // Stop the bubble from moving
        yield return new WaitForSeconds(duration);
        isFrozen = false;            // Resume movement
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Spike spike = other.GetComponent<Spike>();
        if (spike != null && spike.spikeType == bubbleType)
        {
            GameManager.Instance.AddScore(1);
            if (popSounds != null && popSounds.Length > 0)
            {
                // Pick a random index from 0 up to popSounds.Length (exclusive upper bound)
                int randomIndex = Random.Range(0, popSounds.Length);
                AudioClip randomClip = popSounds[randomIndex];

                // Play the chosen random clip
                AudioSource.PlayClipAtPoint(randomClip, transform.position, 1.0f);
            }
            if (popEffect != null)
            {
                Instantiate(popEffect, transform.position, Quaternion.identity);
            }
        }
        else
        {
            GameManager.Instance.EndGame();
        }
        Destroy(gameObject);
    }

    public static void IncrementBubbleSpawned()
{
    BubbleSpawnCount++;
}
}
