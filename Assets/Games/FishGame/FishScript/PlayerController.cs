using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f; 
    Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    private bool facingRight = true;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
       
        
    }
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        Debug.Log("MoveX: " + moveX);

        if (moveX > 0f && facingRight == true)
        {
            Flip();
        }
        else if (moveX < 0f && facingRight == false)
        {
            Flip();
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

    }
    
    
    private void Flip()
    {
        facingRight = !facingRight;
        
        float localScaleX = transform.localScale.x;
        localScaleX *= -1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
