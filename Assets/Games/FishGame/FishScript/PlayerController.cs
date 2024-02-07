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
    public TimeController timeController;
    private bool facingRight = true;
    private bool isGrounded;
    private Animator anim;
    private Vector2 moveX;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
        
    }
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        moveX = new Vector2(horizontalInput, 0f);
        Debug.Log("MoveX: " + moveX);

        if (horizontalInput > 0f && facingRight == true)
        {
            Flip();
        }
        else if (horizontalInput < 0f && facingRight == false)
        {
            Flip();
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
        }
        

    }
  

 
  
    void FixedUpdate()
    {
        float horizontalVelocity = moveX.normalized.x * speed;
        rb.velocity= new Vector2(horizontalVelocity, rb.velocity.y);
    }
    private void Flip()
    {
        facingRight = !facingRight;
        
        float localScaleX = transform.localScale.x;
        localScaleX *= -1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void LateUpdate()
    {
        anim.SetBool("idle", moveX ==Vector2.zero);
        anim.SetBool("isGrounded", isGrounded);
    }
    public void Die()
    {
       
        anim.SetTrigger("die");
    }
}
