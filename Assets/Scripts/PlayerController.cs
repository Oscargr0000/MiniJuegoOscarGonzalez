using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [SerializeField]private float forceJump = 30000f;
    public const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";
    private float inputTol = 0.2f; // Tolerancia del input
    private float xInput, yInput;
     
    
    
    private int MaxSaltos = 1;
    private int saltosRes;

    private bool onAir;
    private bool isWalking;

    private Animator _animator;
    private SpriteRenderer Sr;
    private Rigidbody2D Rb;
    private CapsuleCollider2D Cc;
    private AudioManager Am;
    private ItemsLogic Il;
    private SpawnManager Sm;
    private UiManager Um;

    public LayerMask Ground;
    public int puntiacionCouter;
    public int totalPuntos;

    private bool activateDash;
    private bool activateTime;
    private bool timeStopState;

    public bool desactivateTime;
    public int dashColdDown = 3;
    public bool activatecolddown;
    private bool dashState;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        Cc = GetComponent<CapsuleCollider2D>();
        Am = FindObjectOfType<AudioManager>();
        Il = FindObjectOfType<ItemsLogic>();
        Sm = FindObjectOfType<SpawnManager>();
        Um = FindObjectOfType<UiManager>();
        

        if (PlayerPrefs.GetInt("dashBool").Equals(1))
        {
            activateDash = true;
        }
        
        if (PlayerPrefs.GetInt("dobleJump").Equals(1))
        {
            MaxSaltos = 2;
        } 
        
        if (PlayerPrefs.GetInt("timeStop").Equals(1))
        {
            activateTime = true;
        }
    }

    private void Start()
    {
        saltosRes = MaxSaltos;
        Am.PLayMusic(0);
        desactivateTime = false;
        timeStopState = false;
        activatecolddown = false;
        dashState = true;

    }
    void Update()
    {
        isWalking = false;

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DetectGround();
            if (saltosRes > 0)
            {
                Rb.AddForce(Vector2.up * forceJump * Time.deltaTime, ForceMode2D.Impulse);
                saltosRes--;
            }
        }


        xInput = Input.GetAxisRaw(HORIZONTAL);
            if (Mathf.Abs(xInput) > inputTol && onAir.Equals(false))
            {
                Vector2 translation = new Vector2(xInput * speed, 0);
                Rb.AddForce(translation, ForceMode2D.Force);

                isWalking = true;
                if (xInput < 0)
                {
                    Sr.flipX = true;
                }
                else
                {
                    Sr.flipX = false;
                }
            }

            //DASH
        if(activateDash.Equals(true))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashState.Equals(true))
            {
                float direction = xInput * 2;
                transform.position = new Vector2(transform.position.x + direction, transform.position.y);
                dashState = false;
                StartCoroutine(DashColdDown(dashColdDown));
                activatecolddown = true;

            }

            if (activatecolddown.Equals(true))
            {
                Um.cronometro();
            }
        }

        //TIMESTOP
        if (activateTime.Equals(true))
        {
            if (Input.GetKeyDown(KeyCode.R) && timeStopState.Equals(false))
            {
                timeStopState = true;
                desactivateTime = true;
                Sm.spawnON = false;
                
                StartCoroutine(ActivateTimeIE());
            }
        }
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
            totalPuntos = puntiacionCouter;
            Am.PLaySound(Random.Range(0,3));
        }
    }

    void DetectGround()
    {
        float RayCastExtented = 0.01f;
        float RayCastLimit = Cc.bounds.extents.y + RayCastExtented;
        RaycastHit2D hit = Physics2D.BoxCast(Cc.bounds.center,Cc.bounds.size,0f, Vector2.down, RayCastExtented, Ground);

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

        Debug.DrawRay(transform.position + new Vector3(Cc.bounds.extents.x,0), Vector2.down * RayCastLimit, raycolor);
        Debug.DrawRay(transform.position - new Vector3(Cc.bounds.extents.x, 0), Vector2.down * RayCastLimit, raycolor);
        Debug.DrawRay(transform.position - new Vector3(Cc.bounds.extents.x, RayCastLimit), Vector2.right * Cc.bounds.extents.x, raycolor);
    }


    //DASH COLDDOWN
    public IEnumerator DashColdDown(int time)
    {
        yield return new WaitForSeconds(time);
        activateDash = true;
        dashState = true;
        
    }

    public IEnumerator ActivateTimeIE()
    {
        
        yield return new WaitForSeconds(3);
        desactivateTime = false;
        Sm.spawnON = true;
        StartCoroutine(Sm.Spawn(2));
    }
}
