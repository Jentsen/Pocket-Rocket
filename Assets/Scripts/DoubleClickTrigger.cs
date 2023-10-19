using UnityEngine;

public class DoubleClickTrigger : MonoBehaviour
{
    public CameraFollow cameraFollow; // Reference to the CameraFollow script
    public HeightDisplay heightDisplay; // Reference to the HeightDisplay script

    private float lastClickTime = 0;
    private float doubleClickDelay = 0.3f; // Adjust this value as needed

    private bool isHovering = false; // Flag to track if the mouse is over the "Trigger" object

    private void Start()
    {
        cameraFollow.isActive = false; // Initialize the camera follow as inactive
        heightDisplay.isActive = false;
    }

    private void Update()
    {
        if (isHovering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                float timeSinceLastClick = Time.time - lastClickTime;

                if (timeSinceLastClick <= doubleClickDelay)
                {
                    // Double-click detected while hovering over the "Trigger" object
                    ToggleScripts();
                }
                lastClickTime = Time.time;
            }
        }
    }

    private void OnMouseEnter()
    {
        isHovering = true;
    }

    private void OnMouseExit()
    {
        isHovering = false;
    }

    private void ToggleScripts()
    {
        cameraFollow.isActive = !cameraFollow.isActive; // Toggle the isActive flag
        heightDisplay.isActive = !heightDisplay.isActive;
    }
}
