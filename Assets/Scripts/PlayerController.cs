using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 70f;
    private float forceJump = 25000f;
    public const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";
    private float inputTol = 0.2f; // Tolerancia del input
    private float xInput, yInput;
     
    private Rigidbody2D Rb;
    public int puntiacionCouter;
    private int MaxSaltos = 1;
    public int saltosRes;

    private bool isWalking;
    private Animator _animator;
    private SpriteRenderer Sr;

    

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        saltosRes = MaxSaltos;
    }
    void Update()
    {
        isWalking = false;
        DetectGround();

        xInput = Input.GetAxisRaw(HORIZONTAL);
        if (Mathf.Abs(xInput) > inputTol)
        {
            Vector2 translation = new Vector2(xInput * speed, 0);

            Rb.velocity = translation;

            isWalking = true;
            if (xInput < 0)
            {
                Sr.flipX = true;
            }
            else
            {
                Sr.flipX = false;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                

                if (saltosRes > 0)
                {
                    Rb.AddForce(Vector2.up * forceJump * Time.deltaTime, ForceMode2D.Impulse);
                    saltosRes--;
                }
            }
        }

        

    }

    private void FixedUpdate()
    {
        //SALTO
        



        //DASH
    }

    private void LateUpdate()
    {
        _animator.SetBool("isWalking", isWalking);
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

    void DetectGround()
    {
        float RaycastLimit = 0.7f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, RaycastLimit);

        Color raycolor;
        if (hit.collider != null)
        {
            saltosRes = MaxSaltos;
            raycolor = Color.green;
        }
        else
        {
            raycolor = Color.red;
        }
        Debug.DrawRay(transform.position, Vector2.down * RaycastLimit, raycolor);

        //return hit.collider != null;
    }
}
