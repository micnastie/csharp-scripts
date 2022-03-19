using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 Velocity;
    [Header("Gravity vars")]
    public float speedh;
    public float jumpvel, gravdown, maxdownvel;
    public bool OnPlatform;
    public int Jumps;
    public int MaxJumps;
    public Color[] jumpColors; 
    public float springVel;
    public Vector2 tprs;
    public float onTopThreshold;
    public Vector2 Tracer;
    public int Tjumps;
    Animator anim;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tprs = transform.position;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
      //  jumpColors = new Color[MaxJumps]
    }

    // Update is called once per frame
    void Update()
    {

        Velocity.x = Input.GetAxisRaw("Horizontal") * speedh;
      


        if(Velocity.y > -maxdownvel && !OnPlatform)
        {
            Velocity.y -= gravdown * Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump") && Jumps > 0)
        {
            Velocity.y = jumpvel;
            Jumps--;
            sr.color = jumpColors[Jumps];

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Tracer = transform.position;
            Tjumps = Jumps;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            transform.position = Tracer;
            Jumps = Tjumps;
            sr.color = jumpColors[Jumps];
        }


        rb.MovePosition((Vector2)transform.position + Velocity * Time.deltaTime);

        AnimationLogic();
    }
    public void Respawn()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = tprs;
        Velocity.y = 0;
        Tracer = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            OnPlatform = false;
        }
        if (col.transform.CompareTag("spring"))
        {
            Velocity.y = springVel;

        }
        if (col.transform.CompareTag("Respawn"))
        {
            Respawn();
        }
        if (col.transform.CompareTag("Ground"))
        {
            foreach (ContactPoint2D c in col.contacts)
            {
                //print(Vector2.Angle(c.normal, Vector2.up));
                if (Vector2.Angle(c.normal, Vector2.up) <= onTopThreshold)
                {
                    //print("ON TOP YAYA!!!");
                    OnPlatform = true;
                    Velocity.y = 0;
                    Jumps = MaxJumps;
                    sr.color = jumpColors[Jumps];
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            OnPlatform = false;
        }
        
    }
    public void AnimationLogic()
    {
        if (Velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (Velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (Velocity.x < 0)
        {
            sr.flipX = true;
        }
        if (Velocity.y != 0)
        {
            anim.SetBool("Istouchingdaground", false);
        }
        else
        {
            anim.SetBool("Istouchingdaground", true);

        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            tprs = collision.transform.position;
        }
    }
}
