using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float swayAmplitude = 0.2f;  // Reduced amplitude for X movement
    public float swaySpeed = 5f;        // Speed for X-axis sway
    public float gravityForce = 1f;    // Simulated gravity effect
    private float initialX;
    private float verticalSpeed = 0f;  // Bubble's vertical velocity

    public float speedFactor = 0.5f;

    void Start()
    {
        SpawnObjectRandomly();
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
}
