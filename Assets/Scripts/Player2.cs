using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player2 : MonoBehaviour
{
    //Config
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float jumpForce = 5f;

    [SerializeField]
    Vector2 deathKick = new Vector2(2, 2);

    public float KnockbackXForce;
    public float KnockbackYForce;
    private float PolyCastTime = 0;
    public float CastTime = 0;
    private bool isCasting = false;
    private bool hadEnemy = false;
    public float Jetpack = 0;
    public float hookTime = 0;
    public int Ammo = 0;
    private GameObject PolymorphAimObject;
    private GameObject PlayerAimObject;
    private GameObject CastBar;
    private float oldDir;
    private bool isStunned = false;
    private float StunTime;

    public Transform DeathPoint;

    public float xDir;

    //State
    public bool isAlive = true;

    //Cached component references
    Rigidbody2D rb;
    Animator animator;

    //SpriteRenderer SR;
    //PolygonCollider2D bodyCollider;
    //CircleCollider2D duckCollider;
    CapsuleCollider2D bodyCollider;
    //BoxCollider2D feet;


    //Message then methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        bodyCollider = GetComponent<CapsuleCollider2D>();
        //feet = GetComponent<BoxCollider2D>();
        //duckCollider = GetComponent<CircleCollider2D>();
        //bodyCollider = GetComponent<PolygonCollider2D>();
        //SR = GetComponent<SpriteRenderer>();

        PolymorphAimObject = transform.GetChild(0).gameObject;
        PlayerAimObject = transform.GetChild(1).gameObject;
        CastBar = transform.GetChild(2).gameObject;
        oldDir = 1.0f;
    }

    public void PowerJetpack(int Fuel)
    {
        Jetpack += Fuel;
    }

    public void LoadE(int count)
    {
        Ammo += count;
        return;
    }

    public void StunPlayer()
    {
        animator.SetBool("isStunned", true);
        isStunned = true;
        StopCasting();
        StunTime = 3.0f;
        rb.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        if (!isStunned)
        {
            //casting and moving the aim
            if (isCasting)
            {
                CastPolymorph();
                PolymorphAim();
            }
            //cancel casting with the same button
            if (isCasting && (Input.GetMouseButtonDown(2) || CrossPlatformInputManager.GetButtonDown("Jump")))
            {
                StopCasting();
                return;
            }
            //start casting
            if (Input.GetMouseButtonDown(2))
            {
                if (rb.velocity.y == 0)
                {
                    PlayerAimObject.SetActive(false);
                    PolyCastTime = 0.0f;
                    PolymorphAimObject.transform.localPosition = new Vector3(0, 0, -2);
                    PolymorphAimObject.SetActive(true);
                    isCasting = true;
                    rb.velocity = Vector2.zero;
                }
            }
            //default running state, not casting
            if (!isCasting)
            {
                //PolymorphAimObject.SetActive(false);
                //Hook();
                FlyJetpack();
                KnockBack();
                Beam();
                Run();
                FlipSprite();
                Jump();
                Duck();
                Die();
            }
        }
        else
        {
            isCasting = false;
            PolymorphAimObject.SetActive(false);
            PolyCastTime = 0.0f;
            if (StunTime > 0)
            {
                //Debug.Log("stunned");
                //rb.velocity = Vector2.zero;
                StunTime -= Time.deltaTime;
                return;
            }
            isStunned = false;
            animator.SetBool("isStunned", false);
        }
    }

    private void PolymorphAim()
    {
        if (Input.GetKey(KeyCode.A))
        {
            PolymorphAimObject.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            PolymorphAimObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            PolymorphAimObject.transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            PolymorphAimObject.transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    private void StopCasting()
    {
        //Debug.Log("Stopcasting");
        PolymorphAimObject.SetActive(false);
        PlayerAimObject.SetActive(true);
        PolyCastTime = 0.0f;
        isCasting = false;
        animator.SetBool("isCasting", false);
        CastBar.SetActive(false);
    }

    private void CastPolymorph()
    {
        if(rb.velocity == Vector2.zero && PolyCastTime < CastTime)
        {
            animator.SetBool("isCasting", true);
            CastBar.SetActive(true);
            CastBar.transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(PolyCastTime, 0.5f);
            //CastBar.transform.GetChild(0).localScale = new Vector3(PolyCastTime, 0.8f, 1.0f);         //need to map polycasttime from 0 to 1 for this or get castbar into ui
            PolyCastTime += Time.deltaTime;
        }
        if(PolyCastTime >= CastTime)
        {
            CapsuleCollider2D CC = PolymorphAimObject.GetComponent<CapsuleCollider2D>();
            //not using layer.isTouching to get the enemy
            Collider2D[] cols = Physics2D.OverlapCircleAll(CC.bounds.center, CC.size.x / 2); //cc.bounds.extents
            if(cols != null)
            {
                for(int i = 0; i < cols.Length; i++)
                {
                    if(cols[i].tag == "enemy")
                    {
                        Debug.Log("Polymorph hit: " + cols[i].name);
                        cols[i].gameObject.GetComponent<Enemy2>().PolymorphMe();
                        break;
                    }
                    if(cols[i].tag == "polymorphed")
                    {
                        //rework
                        //if it get's the enemy collider or the enemy polymorph child collider
                        //Debug.Log("Perma poly: " + cols[i].name);
                        if (cols[i].GetComponent<Enemy2>() != null)
                        {
                            cols[i].gameObject.GetComponent<Enemy2>().PolymorphPerma();
                        }
                        else
                        {
                            cols[i].transform.parent.GetComponent<Enemy2>().PolymorphPerma();
                        }
                        break;
                    }
                }
            }
            PolyCastTime = 0;
            isCasting = false;
            PolymorphAimObject.SetActive(false);
            PlayerAimObject.SetActive(true);
            animator.SetBool("isCasting", false);
            CastBar.SetActive(false);
            return;
        }
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1/1
        Vector2 velocity = new Vector2(controlThrow * speed, rb.velocity.y);
        rb.velocity = velocity;

        oldDir = controlThrow;

        //animate running
        bool isRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", isRunning);
    }

    private void Beam()
    {
        if (Input.GetKey(KeyCode.F))
        {
            RaycastHit2D beamHit = RayCastAtTarget(10.0f, LayerMask.GetMask("Enemy", "Ground"));
            if (beamHit.collider != null)
            {
                animator.SetBool("isShooting", true);
                if (beamHit.collider.tag == "enemy")
                {
                    PlayerAimObject.SetActive(false);
                    Debug.DrawLine(transform.position, beamHit.point, Color.red);
                    beamHit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    beamHit.collider.gameObject.transform.position = Vector2.Lerp(beamHit.collider.gameObject.transform.position, transform.position, 1.0f * Time.deltaTime);
                }
                //else
                //{
                //    if (hookTime < 0.3)
                //    {
                //        hookTime += Time.deltaTime;
                //        Debug.DrawLine(transform.position, beamHit.point, Color.white);
                //        rb.velocity = Vector2.zero;
                //        transform.position = Vector2.MoveTowards(transform.position, beamHit.point, 10.0f * Time.deltaTime);
                //    }
                //}
            }
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            hookTime = 0;
            PlayerAimObject.SetActive(true);
            animator.SetBool("isShooting", false);
        }
    }

    private void KnockBack()
    {
        if(Input.GetKeyDown(KeyCode.E) && Ammo > 0)
        {
            RaycastHit2D knockHit = RayCastAtTarget(1.5f, LayerMask.GetMask("Enemy"));
            if (knockHit.collider != null)
            {
                if(Vector2.Distance(transform.position, knockHit.point) < 2f)
                {
                    animator.SetBool("isShooting", true);
                    Debug.DrawLine(transform.position, knockHit.point, Color.green);
                    //knockback to right or left
                    if(transform.position.x < knockHit.point.x)
                    {
                        knockHit.collider.gameObject.GetComponent<EnemyController>().KnockbackMe(new Vector2(KnockbackXForce, KnockbackYForce));
                        Ammo--;
                        return;
                    }
                    else
                    {
                        knockHit.collider.gameObject.GetComponent<EnemyController>().KnockbackMe(new Vector2(-KnockbackXForce, KnockbackYForce));
                        Ammo--;
                        return;
                    }
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("isShooting", false);
        }
    }

    ////in beam
    //private void Hook()
    //{
    //    if (Input.GetKey(KeyCode.G)) //or mouse
    //    {
    //        //Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        //Vector3 dir = target - transform.position;
    //        //RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 15.0f, LayerMask.GetMask("Ground"));
    //        RaycastHit2D hookHit = RayCastAtTarget(10.0f, LayerMask.GetMask("Ground"));
    //        if (hookHit.collider != null)
    //        {
    //            Debug.DrawLine(transform.position, hookHit.point, Color.white);
    //            rb.velocity = Vector2.zero;
    //            transform.position = Vector2.MoveTowards(transform.position, hookHit.point, 10.0f * Time.deltaTime);
    //        }
    //    }
    //}

    private RaycastHit2D RayCastAtTarget(float length, LayerMask mask)
    {
        Vector3 target = PlayerAimObject.transform.GetChild(0).position;
        Vector3 dir = target - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, length, mask);
        return hit;
    }

    private void Jump()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
            CrossPlatformInputManager.GetButtonDown("Jump"))
        {

            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
            rb.velocity += jumpVelocityToAdd;
        }

        if (rb.velocity.y != 0)
        {
            //print(rb.velocity.y);
        }

        //animate jumping
        bool isJumping = Mathf.Abs(rb.velocity.y) > 1;
        animator.SetBool("isJumping", isJumping);
    }

    private void Duck()
    {
        //get values dont hardcode it
        if (CrossPlatformInputManager.GetButtonDown("Duck"))
        {
            //Debug.Log("duck on");
            speed /= 2;
            //bodyCollider.enabled = false;
            //duckCollider.enabled = true;
            bodyCollider.size = new Vector2(bodyCollider.size.x, bodyCollider.size.y / 2);
            bodyCollider.offset = new Vector2(bodyCollider.offset.x, -0.25f);
            bool isDucking = true;
            animator.SetBool("isDucking", isDucking);
        }
        else if (CrossPlatformInputManager.GetButtonUp("Duck"))
        {
            //Debug.Log("duck off");
            speed *= 2;
            //bodyCollider.enabled = true;
            //duckCollider.enabled = false;
            bodyCollider.size = new Vector2(bodyCollider.size.x, bodyCollider.size.y * 2);
            bodyCollider.offset = new Vector2(bodyCollider.offset.x, -0.008890361f);
            bool isDucking = false;
            animator.SetBool("isDucking", isDucking);
        }
    }

    private void FlyJetpack()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            if(Jetpack > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 250.0f * Time.deltaTime);
                Jetpack -= Time.deltaTime;
            }
        }
    }

    private void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            Death();
        }
    }

    //when you collide with top of an enemy and ^^ 
    public void Death()
    {
        isAlive = false;
        PolymorphAimObject.SetActive(false);
        PlayerAimObject.SetActive(false);
        animator.SetTrigger("Die");
        rb.velocity = deathKick;
        transform.position = DeathPoint.position;
    }

    private void FlipSprite()
    {
        bool isMovingHorizontally = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (isMovingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
            //transform.GetChild(1).transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
        }
    }
}
