using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private float forceJump = 250f;
    public const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";
    private float inputTol = 0.2f; // Tolerancia del input
    private float xInput, yInput;
     
    private Rigidbody2D Rb;
    public int puntiacionCouter;
    private int MaxSaltos = 1;
    private int saltosRes;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        saltosRes = MaxSaltos;
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
                if (saltosRes > 0)
                {
                    Rb.AddForce(Vector2.up * forceJump * Time.deltaTime, ForceMode2D.Impulse);
                    saltosRes--;
                }
            }
        
        //DASH
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            saltosRes = MaxSaltos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Items"))
        {
            
            Destroy(collision.gameObject);
            int reco = collision.gameObject.GetComponent<ItemsLogic>().recompensa;

            puntiacionCouter = puntiacionCouter + reco;
            Debug.Log(puntiacionCouter);
        }
    }
}
