using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private float doubleJumpPower = 10;
    [SerializeField] private float longJumpGravity = 0.7f;
    [SerializeField] private float normalGravity = 1;
    [SerializeField] private float fallingGravity = 4.5f;
    [SerializeField] private float slidingSpeed = -3;
    [SerializeField] private float wallJumpPowerX,wallJumpPowerY;
    
    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck, wallCheck;
    [SerializeField] private LayerMask groundLayer, wallLayer;
    
    private float _horizontalInput;
    private float SlideMomentum = 0;
    private bool _canDoubleJumped;
    private bool _isOnWall;
    private bool _isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        IsGrounded();
        IsOnWall();
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallingGravity;
        }

        Flip();
        SlideLogic();
        JumpLogic();
        


    }

    private void Flip()
    {
        if (_horizontalInput > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(_horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void SlideLogic()
    {
        if (_isOnWall)
        {
            if (transform.localScale.x == 1 && _horizontalInput > 0)
            {
                SlideMomentum += slidingSpeed * Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x,SlideMomentum);
            }
            else if(transform.localScale.x == -1 && _horizontalInput < 0)
            {
                SlideMomentum += slidingSpeed * Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x,SlideMomentum);
            }
        }
        else
        {
            SlideMomentum = 0;
        }
        
        
    }

    private void JumpLogic()
    {
        
        if (_isGrounded && !Input.GetButton("Jump"))
        {
            _canDoubleJumped = false;
        }

        if (Input.GetButtonDown("Jump") && _canDoubleJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpPower);
            rb.gravityScale = longJumpGravity;
            _canDoubleJumped = false;
        }
        
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            rb.gravityScale = longJumpGravity;
            _canDoubleJumped = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.gravityScale = normalGravity;
        }
    }

    private void IsGrounded()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
    }

    private void IsOnWall()
    {
        _isOnWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void FixedUpdate()
    {
        
        rb.velocity = new Vector2(_horizontalInput * speed, rb.velocity.y);
    }
}
