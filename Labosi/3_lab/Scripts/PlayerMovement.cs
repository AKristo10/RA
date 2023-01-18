using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    public float transitionForce = 14f;

    [SerializeField] public bool isTransporting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransporting)
        {
            rb.AddForce(new Vector2(transitionForce, 0));
            rb.velocity = new Vector2(rb.velocity.x, 0);
            lightning();
            return;
        } else
        {
            var c = sr.color;
            c.a = 255;
            sr.color = c;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimation();

        if (rb.position.y <= -50 || rb.position.y >= 50)
        {
            this.GetComponent<PlayerLife>().Die();
        }
    }

    private void UpdateAnimation()
    {
        if (dirX > 0)
        {
            anim.SetBool("running", true);
            sr.flipX = true;
        }
        else if (dirX < 0)
        {
            anim.SetBool("running", true);
            sr.flipX = false;
        }
        else
        {
            anim.SetBool("running", false);
        }

        if (rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
        else
        {
            anim.SetBool("falling", false);
        }
    }

    private void lightning()
    {

        if (Random.Range(0, 3) == 0)
        {
            var c = sr.color;
            c.a = (1 - c.a / 255) * 255;
            sr.color = c;
        }
        else
        {
            var c = sr.color;
            c.a = 255;
            sr.color = c;
        }
    }
}
