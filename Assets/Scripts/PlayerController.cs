using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5.0f;
    private float forceJump = 250f;
    private float dashForce = 200f;
    public const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";
    private float inputTol = 0.2f; // Tolerancia del input
    private float xInput, yInput;

    private Rigidbody2D Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        xInput = Input.GetAxisRaw(HORIZONTAL);
        if (Mathf.Abs(xInput) > inputTol)
        {
            Vector3 translation = new Vector3(xInput * speed * Time.deltaTime, 0, 0);
            transform.Translate(translation);
        }
       
    }

    private void FixedUpdate()
    {
        //SALTO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rb.AddForce(Vector2.up * forceJump * Time.deltaTime, ForceMode2D.Impulse);
        }

        //DASH
    }
}
