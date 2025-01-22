using UnityEngine;

public class SquareController : MonoBehaviour
{
    private float cameraHeight;
    private float cameraWidth;
    public GameObject squarePrefab; // Prefab for the square object
    private GameObject[] squares = new GameObject[4]; // Array to store the 4 squares

    void Start()
    {
      
    }

    void Update()
    {
        // Example: Press Space to move all squares to a new random Y position
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveAllSquares(Random.Range(-cameraHeight / 2, cameraHeight / 2));
        }
    }

    // Moves all squares to a new Y position
    void MoveAllSquares(float newY)
    {
        float clampedY = Mathf.Clamp(newY, -cameraHeight / 2, cameraHeight / 2);

        foreach (GameObject square in squares)
        {
            if (square != null)
            {
                Vector3 newPosition = new Vector3(square.transform.position.x, clampedY, 0);
                square.transform.position = newPosition;
            }
        }
    }
}