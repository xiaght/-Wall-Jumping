using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D coll;
    //  Animator anim;
    SpriteRenderer sr;

    public Sprite[] anim;

    bool isWall;
    public bool isJump;
    bool inputSpace;

    public int dir;
    public int lookdir;
    public int jumpCount;
    public float jumpPower;
    bool isDeath;
    public HudManager Ui;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        // anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        dir = 1;
        lookdir = 1;
        jumpCount = 3;
        jumpPower = 2.5f;
        isJump = jumpCount == 0 ? false : true;
    }
    public void Jump() {

        sr.flipX = lookdir == 1 ? false : true;
        if (!isWall)
        {
            sr.flipX = lookdir == -1 ? false : true;
        }
        isJump = jumpCount == 0 ? false : true;

        if (inputSpace && isJump)
        {
            isWall = true;
            if (jumpCount == 3)
            {
                rigid.AddForce(new Vector2(dir, jumpPower) * 3, ForceMode2D.Impulse);
                // anim.SetTrigger("Jump1");
                StartCoroutine(Jump1());
                jumpCount--;

            }
            else if (jumpCount == 2)
            {
                rigid.AddForce(new Vector2(dir, jumpPower) * 3, ForceMode2D.Impulse);
                //  anim.SetTrigger("Jump2");
                StartCoroutine(Jump1());
                jumpCount--;

            }
            else if (jumpCount == 1)
            {
                rigid.AddForce(new Vector2(dir, jumpPower) * 3, ForceMode2D.Impulse);
                //  anim.SetTrigger("Jump3");
                StartCoroutine(Jump2());
                jumpCount--;

            }

        }
    }
    private void FixedUpdate()
    {
        GetInput();
        Jump();
        

    }
    public void GetInput()
    {

        inputSpace = Input.GetKeyDown(KeyCode.Space);
    }

    void Update()
    {

    }

    IEnumerator Jump1() {

        sr.sprite = anim[1];
        
        yield return new WaitForSecondsRealtime(0.1f);
        sr.sprite = anim[2];
        yield return new WaitForSecondsRealtime(0.1f);
        sr.sprite = anim[3];
        yield return new WaitForSecondsRealtime(0.1f);
    }

    IEnumerator Jump2()
    {

        sr.sprite = anim[4];

        yield return new WaitForSecondsRealtime(0.1f);
        sr.sprite = anim[5];
        yield return new WaitForSecondsRealtime(0.1f);
    }
    IEnumerator Down()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        sr.sprite = anim[7];
        yield return new WaitForSecondsRealtime(0.1f);
    }
    IEnumerator Death()
    {
        gameObject.layer = 8;
        jumpCount = -4;
        isDeath = true;
        Ui.SetRestartUI();

        if (Ui.bestscore < Ui.scoreInt)
        {
            PlayerPrefs.SetInt("MaxScore", Ui.scoreInt);
        }



        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        sr.sprite = anim[8];
        yield return new WaitForSecondsRealtime(0.5f);
        sr.sprite = anim[9];
        yield return new WaitForSecondsRealtime(0.1f);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {


            StartCoroutine(Death());

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Wall"&&isWall&&!isDeath)
        {
            isWall = false;
            
            Debug.Log("2");
            lookdir *= -1;
            dir *= -1;
            sr.sprite = anim[6];
            jumpCount = 3;
            StartCoroutine(Down());
        }


    }
    
}
