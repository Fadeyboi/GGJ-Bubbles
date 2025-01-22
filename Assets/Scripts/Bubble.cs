using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour
{
    public float swayAmplitude = 0.2f;
    public float swaySpeed = 5f;
    public float gravityForce = 1f;
    private float initialX;
    private float verticalSpeed = 0f;
    public float speedFactor = 0.5f;

    string[] types = { "Apple", "Cherry", "Melon", "Banana" };
    public string bubbleType;

    // Sprites for each type
    public Sprite appleSprite;
    public Sprite cherrySprite;
    public Sprite melonSprite;
    public Sprite bananaSprite;

    // 1) Add your pop sound
    public AudioClip[] popSounds;


    public GameObject popEffect;  // <-- Add this

    void Start()
    {
        SpawnObjectRandomly();
        bubbleType = types[Random.Range(0, types.Length)];
        Debug.Log($"Assigned type: {bubbleType}");

        AddSpriteToBubble();
        
    }

    void Update()
    {
        BubbleSwayWithGravity();
    }

    void SpawnObjectRandomly()
    {
        float screenWidth = Screen.width;
        float columnWidth = screenWidth / 4f;

        int randomColumn = Random.Range(0, 4);

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

        // Simulate gravity-like effect for Y-axis
        verticalSpeed -= speedFactor * Time.deltaTime;
        float newY = transform.position.y + verticalSpeed * Time.deltaTime;

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    void AddSpriteToBubble()
    {
        // Create a child GameObject for the sprite
        GameObject spriteObject = new GameObject("BubbleTypeSprite");
        spriteObject.transform.SetParent(this.transform);

        // Add a SpriteRenderer to the child object
        SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();

        // Assign the sprite based on the bubble type
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

        // Adjust the sprite's position relative to the bubble
        Vector3 spritePosition = spriteObject.name == "Apple_0" ? new Vector3(0, -0.05f, 0) : new Vector3(0, 0, 0);
        spriteObject.transform.localPosition = spritePosition;
        // Adjust the sprite's scale if needed
        spriteObject.transform.localScale = new Vector3(3f, 3f, 1f); // Adjust scale if needed

        // Set the sorting order
        spriteRenderer.sortingOrder = 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test");

        Spike spike = other.GetComponent<Spike>();
        if (spike != null && spike.spikeType == bubbleType)
        {
            GameManager.Instance.AddScore(1);

            
            
        }
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

        Destroy(gameObject);
    }

}
