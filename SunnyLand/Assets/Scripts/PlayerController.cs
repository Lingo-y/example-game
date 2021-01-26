using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpforce;
    private Animator anim;
    public LayerMask ground;
    public Collider2D coll;
    public Collider2D disColl;
    public Transform ceilingCheck,groundCheck;
    public int cherry;
    public Text CherryNum;
    private bool isHurt;
    //public AudioSource jumpAudio, hurtAudio, cherryAudio;
    private bool isGround;
    private int extraJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }
    void Movement()
    {
        float hrizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = new Vector2(hrizontalmove * speed, rb.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(facedirection));
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        /*  if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
          {

              rb.velocity = new Vector2(rb.velocity.x, jumpforce);
              jumpAudio.Play();
              anim.SetBool("Up", true);
          }*/
        NewJump();
        Crouch();
    }
    void SwitchAnim()
    {
        anim.SetBool("Idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("Down", true);
        }
        if (anim.GetBool("Up"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("Up", false);
                anim.SetBool("Down", true);
            }

        }
        else if (isHurt)
        {
            anim.SetBool("Hurt", true);
            if (rb.velocity.x < 0.1f)
            {
                anim.SetBool("Hurt", false);
                anim.SetBool("Idle", true);
                anim.SetFloat("Speed", 0);
                isHurt = false;
            }

        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("Down", false);
            anim.SetBool("Idle", true);
        }
    }
    //触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集物品
        if (collision.tag == "Collection")
        {
            /*cherryAudio.Play();*/
            SoundManager.instance.CherryAudio();
            Destroy(collision.gameObject);
            cherry++;
            CherryNum.text = cherry.ToString();
        }
        //出界重开
        if (collision.tag == "DeadLine")
        {
            //延迟运行
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (anim.GetBool("Down"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("Up", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                SoundManager.instance.HurtAudio();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                /*hurtAudio.Play();*/
                SoundManager.instance.HurtAudio();
                isHurt = true;
            }
        }
    }
    private void Crouch()
    {
        if (!Physics2D.OverlapCircle(ceilingCheck.position, 0.2f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("Crouch", true);
                disColl.enabled = false;
            }
            else
            {
                anim.SetBool("Crouch", false);
                disColl.enabled = true;
            }
        }
    }
    private void NewJump()
    {
        if (isGround)
        {
            extraJump = 1;
        }
        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJump--;
            /*jumpAudio.Play();*/
            /*SoundManager soundManager = gameObject.GetComponent<SoundManager>();
            soundManager.JumpAudio();*/
            SoundManager.instance.JumpAudio();
            anim.SetBool("Up", true);
        }
        if (Input.GetButtonDown("Jump") && extraJump == 0&&isGround)
        {
            rb.velocity = Vector2.up * jumpforce;

            /* jumpAudio.Play();*/
            SoundManager.instance.JumpAudio();
            anim.SetBool("Up", true);
        }
    }
    private void Restart()
    {


        //不要直接写当前场景名字，不然每次换场景都要改
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
