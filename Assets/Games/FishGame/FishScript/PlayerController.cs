using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
    public Animator animTrampoline;
    private Vector2 moveX;
    public GameObject WinText;
    [SerializeField]private GameController gameController;
    public GameObject Cristal;
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
            
            spriteRenderer.color = Color.blue;
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
        if (collision.gameObject.tag == "Heal")
        {
            spriteRenderer.color = Color.white;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water")
        {
            anim.Play("SwimFish");
            WinText.SetActive(true); 
            Cristal.SetActive(true);
            gameController.timeController.gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "fall")
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
            gameController.removeTime(10);
            spriteRenderer.color = Color.red;
            Time.timeScale = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "trampoline")
        {
            Jump(jumpForce * 2f);
            animTrampoline.Play("jumpTrampoline");
        }

    }
    private void Jump(float force)
    {
        rb.velocity = Vector2.up * force;
    }

}
