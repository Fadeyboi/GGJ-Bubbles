using UnityEngine;
using System.Collections.Generic;  // For Queue<T>

public class SpawnPlayer : MonoBehaviour
{
    private float cameraHeight;
    private float cameraWidth;
    public GameObject squarePrefab;

    private GameObject[] squares;    // Holds the 4 squares
    private float columnWidth;       // Distance between columns
    private float bottomY;           // Y-position where squares sit

    void Start()
    {
        // Get main camera
        Camera cam = Camera.main;
        if (cam != null)
        {
            cameraHeight = cam.orthographicSize * 2;
            cameraWidth = cameraHeight * cam.aspect;

            columnWidth = cameraWidth / 4f;

            // Spawn initial squares
            SpawnSquares();
        }
        else
        {
            Debug.LogError("No Main Camera found in the scene.");
        }
    }

    void Update()
    {
        // Press D -> ShiftRight
        if (Input.GetKeyDown(KeyCode.D))
        {
            ShiftRight();
        }
        // Press A -> ShiftLeft
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ShiftLeft();
        }
    }

    /// <summary>
    /// Spawns 4 squares, each in its own column, at the bottom of the screen,
    /// and assigns labels from a queue (A, B, C, D) to each square's SquareLabel.
    /// </summary>
    void SpawnSquares()
    {
        if (squarePrefab == null)
        {
            Debug.LogError("squarePrefab is not assigned! Please assign it in the Inspector.");
            return;
        }

        // Create an array of 4 squares
        squares = new GameObject[4];

        float squareHeight = cameraHeight / 6f;
        bottomY = -cameraHeight / 2 + (squareHeight / 2);

        // Create a queue with our labels
        Queue<string> labelQueue = new Queue<string>();
        labelQueue.Enqueue("A");
        labelQueue.Enqueue("B");
        labelQueue.Enqueue("C");
        labelQueue.Enqueue("D");

        // Instantiate each square in a separate column
        for (int i = 0; i < 4; i++)
        {
            float xPos = (-cameraWidth / 2) + (i + 0.5f) * columnWidth;
            Vector3 spawnPosition = new Vector3(xPos, bottomY, 0);

            squares[i] = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
            squares[i].transform.localScale = new Vector3(columnWidth, squareHeight, 1);

            // Pop the next label from the queue
            string label = labelQueue.Dequeue();

            // Assign the label to the SquareLabel attribute
            SquareLabel sqLabel = squares[i].GetComponent<SquareLabel>();
            if (sqLabel != null)
            {
                sqLabel.label = label;
            }
        }
    }

    /// <summary>
    /// Shifts all squares one position to the left (cyclic). 
    /// The leftmost becomes the rightmost.
    /// </summary>
    void ShiftLeft()
    {
        // Store the first (leftmost) square
        GameObject temp = squares[0];

        // Move squares left: index 0 <- 1, 1 <- 2, 2 <- 3
        squares[0] = squares[1];
        squares[1] = squares[2];
        squares[2] = squares[3];

        // The leftmost square goes to the rightmost index
        squares[3] = temp;

        // Reposition squares from left to right
        RepositionSquares();
    }

    /// <summary>
    /// Shifts all squares one position to the right (cyclic).
    /// The rightmost becomes the leftmost.
    /// </summary>
    void ShiftRight()
    {
        // Store the last (rightmost) square
        GameObject temp = squares[3];

        // Move squares right: index 3 <- 2, 2 <- 1, 1 <- 0
        squares[3] = squares[2];
        squares[2] = squares[1];
        squares[1] = squares[0];

        // The rightmost square goes to the leftmost index
        squares[0] = temp;

        // Reposition squares from left to right
        RepositionSquares();
    }

    /// <summary>
    /// Positions each square from left to right
    /// according to its index in the squares array.
    /// </summary>
    void RepositionSquares()
    {
        for (int i = 0; i < squares.Length; i++)
        {
            if (squares[i] != null)
            {
                float xPos = (-cameraWidth / 2) + (i + 0.5f) * columnWidth;
                squares[i].transform.position = new Vector3(xPos, bottomY, 0);
            }
        }
    }
}
