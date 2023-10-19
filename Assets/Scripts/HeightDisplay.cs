using UnityEngine;
using UnityEngine.UI;

public class HeightDisplay : MonoBehaviour
{
    public Transform target; // The object whose height to track
    public Text heightText;  // Reference to the Text UI element
    public bool isActive = false;

    void Update()
    {
        if (target != null && heightText != null)
        {
            float height = target.position.y;
            heightText.text = "Height: " + height.ToString("F2"); // Display height with 2 decimal places
        }
    }
}
