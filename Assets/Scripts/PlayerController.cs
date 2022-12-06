using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script is attach to the player and contains the logic for movement, powers,...
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

    private bool isJumpPress = false;

    public ParticleSystem smokeParticle;
    public ParticleSystem pickupParticle;


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
        
        //Get the information to look if the power are in the player
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
        //Detect when the jumping button is pressed
        if (Input.GetButtonDown("Jump"))
            isJumpPress = true;
            DetectGround();



        //Dash Logic
        if (activateDash.Equals(true))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashState.Equals(true))  //When is pressed the shift button teleport the player 2m away in the x direction 
            {

                Vector2 lastPos = transform.position;
                Instantiate(smokeParticle, transform.position, transform.rotation);

                float direction = xInput * 2;
                transform.position = new Vector2(transform.position.x + direction, transform.position.y);


                // Evitate go outsite the map
                Vector2 updatedPos = transform.position;
                if(updatedPos.x > 9 || updatedPos.x < -9)
                {
                    transform.position = lastPos;
                }

                dashState = false;
                StartCoroutine(DashColdDown(dashColdDown));
                activatecolddown = true;

            }

            if (activatecolddown.Equals(true))  //When the dash is used, start the recharge for the dashUI
            {
                Um.cronometro();
            }
        }

        //TIMESTOP
        if (activateTime.Equals(true))  //When the R key is pressed, desactivate the movement of the items and the spawnManager.  THIS POWER ONLY WORK 1 TIME
        {
            if (Input.GetKeyDown(KeyCode.R) && timeStopState.Equals(false))
            {
                Um.TimeStoped();
                timeStopState = true;
                desactivateTime = true;
                Sm.spawnON = false;
                StartCoroutine(ActivateTimeIE());
            }
        }
    }


    private void FixedUpdate()
    {
        isWalking = false;  


        //Jumping logic
        if (isJumpPress)
        {
            if (saltosRes > 0)
            {
                Rb.AddForce(Vector2.up * forceJump * Time.deltaTime, ForceMode2D.Impulse);
                saltosRes--;
            }

            isJumpPress = false;
        }

        Movement();
    }

    private void LateUpdate() //Play the animation for the player
    {
        _animator.SetBool("isWalking", isWalking);
    }



    // Detect when player picks up an item
    // The reward for every item is set in the inspector
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Items"))
        {
            Instantiate(pickupParticle, collision.transform.position, transform.rotation);
            Destroy(collision.gameObject);
            int reco = collision.gameObject.GetComponent<ItemsLogic>().recompensa;

            puntiacionCouter = puntiacionCouter + reco;
            totalPuntos = puntiacionCouter;
            Am.PLaySound(Random.Range(0,3));
        }
    }



    //This funtion detect, by raycast, when the player is touching the ground and resets the counter of the jumps
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

    //DESACTIVATE THE TIMESTOP
    public IEnumerator ActivateTimeIE()
    {
        
        yield return new WaitForSeconds(3);
        desactivateTime = false;
        Sm.spawnON = true;
        StartCoroutine(Sm.Spawn(2));
    }


    //MOVEMENT LOGIC    
    private void Movement()
    {
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
    }
}
