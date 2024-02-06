using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed = 2;
    [SerializeField] private float jumSpeed = 5;
    public SpriteRenderer spriteRenderer; 
    
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void LateUpdate()
    {
        if (rb.velocity.x > 0)
        {
            anim.SetBool("Walking", true);
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            anim.SetBool("Walking", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new UnityEngine.Vector2(x * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && GroundCheckScript.IsGrounded)
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumSpeed);
        }
        if(GroundCheckScript.IsGrounded==false)
        {
            anim.SetBool("Jumping", false);
            
        }
        
    }
}
