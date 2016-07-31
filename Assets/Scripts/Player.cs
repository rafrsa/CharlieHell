using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float maxSpeed = 10f;
    private bool facingRight = true;

    private Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float jumpForce = 5f;


    void Start ()
    {
        anim = this.GetComponent<Animator>();
	}
	
	void FixedUpdate ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", this.GetComponent<Rigidbody2D>().velocity.y);

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, this.GetComponent<Rigidbody2D>().velocity.y);

        if(move > 0 && !facingRight)
        {
            Flip();
        }
        else if(move < 0 && facingRight)
        {
            Flip();
        }
    }

    void Update()
    {
        if(grounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("Ground", false);
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
        if (grounded && Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Ground", false);
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
