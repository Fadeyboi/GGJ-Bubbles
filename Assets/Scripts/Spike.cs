using UnityEngine;

public class Spike : MonoBehaviour
{
    public string spikeType;

    void Start()
    {
        // Get the parent's SquareLabel component
        SquareLabel parentLabel = GetComponentInParent<SquareLabel>();

        if (parentLabel != null)
        {
            // Set the spike's type to match the parent's label
            spikeType = parentLabel.label;
            Debug.Log($"Spike type set to: {spikeType}");
        }
        else
        {
            Debug.LogError("Parent does not have a SquareLabel component.");
        }
    }
}
