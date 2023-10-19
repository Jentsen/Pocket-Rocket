using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    // private Rigidbody rb;
    private Rigidbody2D rb2d;
    public float jump = 10;
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            rb2d.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
    }
}