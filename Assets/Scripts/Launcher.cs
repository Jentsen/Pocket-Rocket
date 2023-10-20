using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float jump = 2f;
    
    private Rigidbody2D rb2d;
    private bool gravityEnabled = false;
    private CameraFollow cameraFollowScript;
    
    // Variables for double-click detection
    private bool isDoubleClick = false;
    private float doubleClickTime = 0.3f;
    private float lastClickTime = 0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0; // Disable gravity initially

        // Find the CameraFollow script attached to the camera object
        cameraFollowScript = Camera.main.GetComponent<CameraFollow>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsDoubleClick())
            {
                if (!gravityEnabled)
                {
                    EnableGravity();
                    rb2d.bodyType = RigidbodyType2D.Dynamic; //change body type to dynamic

                    // Activate the CameraFollow script
                    cameraFollowScript.isActive = true;
                }
                LaunchObject();
            }
        }
    }

    bool IsDoubleClick()
    {
        float currentTime = Time.time;
        if (currentTime - lastClickTime <= doubleClickTime)
        {
            isDoubleClick = true;
        }
        else
        {
            isDoubleClick = false;
        }

        lastClickTime = currentTime;
        return isDoubleClick;
    }

    void LaunchObject()
    {
        // Check if the mouse is hovering over the "Trigger" object
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if (hitCollider != null && hitCollider.gameObject.CompareTag("Trigger"))
        {
            // Launch the object upwards
            EnableGravity();
            rb2d.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
    }

    void EnableGravity()
    {
        rb2d.gravityScale = 2; // Enable gravity when double-clicking the "Trigger"
        gravityEnabled = true;
    }
}