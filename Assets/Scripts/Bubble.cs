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
    string[] types = { "Apple", "Orange", "Green", "Yellow" };
    public string bubbleType;

    void Start()
    {
        SpawnObjectRandomly();
        bubbleType = types[Random.Range(0, types.Length)];
        Debug.Log($"Assigned type: {bubbleType}");
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
        verticalSpeed -= (gravityForce * speedFactor) * Time.deltaTime;
        float newY = transform.position.y + verticalSpeed * Time.deltaTime;

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Spike"))
    {
        Spike spike = other.GetComponent<Spike>();
        if (spike != null && spike.spikeType == bubbleType)
        {
            GameManager.Instance.AddScore(1);
        }
        Destroy(gameObject);
    }
}

}
