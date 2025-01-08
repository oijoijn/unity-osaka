using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProを使用するために追加

public class Player : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float JumpForce = 15f;
    public LayerMask GroundLayer;
    private Rigidbody2D rb;
    public int Money = 0;
    public AudioSource JumpSE;
    public AudioSource ResetSE;
    public AudioSource MoneySE;
    public AudioSource BGMSource;
    public GameObject GameoverPanel;
    public TextMeshProUGUI GameoverText;
    private Vector3 initialPosition; // 初期位置を保存

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // 初期位置を記録

        if (BGMSource != null && !BGMSource.isPlaying)
        {
            BGMSource.Play();
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return; // 時間が停止している場合は何もしない

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MoveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            JumpSE.Play();
        }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag("InstaDeath"))
        {
            GameoverText.text = "Game Over!"; //テキスト変更
            ResetSE.Play();
            GameoverPanel.SetActive(true);
            Time.timeScale = 0; // 時間を停止
        }
    }

    public void Retry() // メニューからリトライボタンを押したときに呼び出す
    {
        transform.position = initialPosition; // プレイヤーを初期位置に戻す
        rb.velocity = Vector2.zero; // プレイヤーの速度をリセット
        Time.timeScale = 1; // 時間を再開
        GameoverPanel.SetActive(false); // メニューを非表示
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.GetComponent<Collectable>() != null)
        {
            if (obj.GetComponent<Collectable>().ID == "money")
            {
                Money++;
                GameObject.Destroy(obj);
                MoneySE.Play();
            }
        }
        //Frag
        if (obj.CompareTag("Flag"))
        {
            GameoverPanel.SetActive(true);
            GameoverText.text = "Stage Completed!"; //テキスト変更
            Time.timeScale = 0;
        }
    }
}
