using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightDisplay : MonoBehaviour
{
    public Transform springTransform;  // Reference to the "Spring" object's Transform.
    public Text heightText;  // Reference to the Text UI element.

    void Update()
    {
        if (springTransform != null && heightText != null)
        {
            // Get the height of the "Spring" object and display it as text.
            float height = springTransform.position.y;
            heightText.text = "Height: " + height.ToString("F2");  // Display the height with two decimal places.
        }
    }
}
