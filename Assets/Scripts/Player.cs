using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float JumpForce = 15f;
    public LayerMask GroundLayer; // メソッド外に移動
    private Rigidbody2D rb;
    public int Money = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 移動処理
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MoveSpeed, rb.velocity.y);

        // Jumpの制御
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        // playerの反転
        if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private bool isGrounded()
    {
        BoxCollider2D c = GetComponent<BoxCollider2D>();
        return Physics2D.BoxCast(c.bounds.center, c.bounds.size, 0f, Vector2.down, .1f, GroundLayer);
    }
    // add
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //コード簡略化のためにobjを追加
        GameObject obj = collision.gameObject;

        //InstaDeath
        if (obj.CompareTag("InstaDeath"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //コード簡略化のためにobjを追加
        GameObject obj = collision.gameObject;

        //Collectable
        if (obj.GetComponent<Collectable>() != null)
        {
            if (obj.GetComponent<Collectable>().ID == "money")
            {
                Money++;
                GameObject.Destroy(obj);
            }
        }
    }
}
