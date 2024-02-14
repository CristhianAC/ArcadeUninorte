using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f; 
    Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    private bool isGrounded;
    private Animator anim;
    private Vector2 moveX;
    public TextMeshProUGUI WinText;
    [SerializeField]private GameController gameController;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        moveX = new Vector2(horizontalInput, 0f);

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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Heal")
        {
            gameController.addTime(10);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Damage")
        {
            gameController.removeTime(10);
            spriteRenderer.color = Color.red;
            Die();
        }
    }
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            spriteRenderer.color = Color.red;
            Die();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            anim.Play("idleFish");
            spriteRenderer.color = Color.white;
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water")
        {
            anim.Play("SwimFish");
            WinText.gameObject.SetActive(true);
        }
        if(collision.gameObject.tag == "fall")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

}
