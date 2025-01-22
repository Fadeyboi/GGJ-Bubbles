using UnityEngine;
using System.Collections.Generic;

public class SpawnPlayer : MonoBehaviour
{
    private float cameraHeight;
    private float cameraWidth;
    public GameObject squarePrefab;
    public GameObject spikePrefab;
    public Sprite appleSprite;
    public Sprite cherrySprite;
    public Sprite melonSprite;
    public Sprite bananaSprite;

    private GameObject[] squares;    // Holds the 4 squares
    private float columnWidth;       // Distance between columns
    private float bottomY;           // Y-position where squares sit
    private Dictionary<string, Sprite> labelToSprite; // Map labels to sprites

    void Start()
    {
        // Initialize the label-to-sprite dictionary
        labelToSprite = new Dictionary<string, Sprite>
        {
            { "Apple", appleSprite },
            { "Cherry", cherrySprite },
            { "Melon", melonSprite },
            { "Banana", bananaSprite }
        };

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
        if (Input.GetKeyDown(KeyCode.D))
        {
            ShiftRight();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ShiftLeft();
        }
    }

    void SpawnSquares()
    {
        if (squarePrefab == null)
        {
            Debug.LogError("squarePrefab is not assigned! Please assign it in the Inspector.");
            return;
        }

        if (spikePrefab == null)
        {
            Debug.LogError("spikePrefab is not assigned! Please assign it in the Inspector.");
            return;
        }

        // Create an array of 4 squares
        squares = new GameObject[4];

        float squareHeight = cameraHeight / 6f;
        bottomY = -cameraHeight / 2 + (squareHeight / 2);

        // Create a queue with our labels
        Queue<string> labelQueue = new Queue<string>();
        labelQueue.Enqueue("Apple");
        labelQueue.Enqueue("Cherry");
        labelQueue.Enqueue("Melon");
        labelQueue.Enqueue("Banana");

        // Instantiate each square in a separate column
        for (int i = 0; i < 4; i++)
        {
            float xPos = (-cameraWidth / 2) + (i + 0.5f) * columnWidth;
            Vector3 spawnPosition = new Vector3(xPos, bottomY, 0);

            // Instantiate the square
            squares[i] = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
            squares[i].transform.localScale = new Vector3(columnWidth, squareHeight, 1);

            // Instantiate the spike as a child of the square
            GameObject spike = Instantiate(spikePrefab, squares[i].transform);

            // Set the spike's local position
            Vector3 spikePosition = new Vector3(0, 0.75f, 0); // Adjust the position as needed
            spike.transform.localPosition = spikePosition;

            // Adjust the spike's scale to fit the square
            spike.transform.localScale = new Vector3(8.434419f, 10.5475f, 1);

            // Assign the label to the SquareLabel attribute
            SquareLabel sqLabel = squares[i].GetComponent<SquareLabel>();
            if (sqLabel != null)
            {
                string label = labelQueue.Dequeue();
                sqLabel.label = label;

                // Add the fruit sprite based on the label
                if (labelToSprite.TryGetValue(label, out Sprite sprite))
                {
                    AddSpriteToSquare(squares[i], sprite);
                }
            }
        }
    }

    void AddSpriteToSquare(GameObject square, Sprite sprite)
    {
        // Create a new child GameObject for the fruit sprite
        GameObject fruitSpriteObject = new GameObject("FruitSprite");
        fruitSpriteObject.transform.SetParent(square.transform);

        // Add a SpriteRenderer and set the sprite
        SpriteRenderer spriteRenderer = fruitSpriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        // Determine position based on the sprite or label
        Vector3 spritePosition = sprite.name == "Apple_0" ? new Vector3(0, -0.05f, 0) : new Vector3(0, 0, 0);
        fruitSpriteObject.transform.localPosition = spritePosition;

        // Adjust the sprite's scale
        fruitSpriteObject.transform.localScale = new Vector3(1f, 2f, 1f); // Adjust scale if needed

        // Set the sorting order
        spriteRenderer.sortingOrder = 1;
    }

    void ShiftLeft()
    {
        GameObject temp = squares[0];
        squares[0] = squares[1];
        squares[1] = squares[2];
        squares[2] = squares[3];
        squares[3] = temp;
        RepositionSquares();
    }

    void ShiftRight()
    {
        GameObject temp = squares[3];
        squares[3] = squares[2];
        squares[2] = squares[1];
        squares[1] = squares[0];
        squares[0] = temp;
        RepositionSquares();
    }

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
