using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float jumpingControl;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private Transform playerSprite;

    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private int moveDir = 0;
    private bool rightDown = false;
    private bool leftDown = false;
    private bool movingRight = false;
    private bool movingLeft = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = playerSprite.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            rightDown = true;
            movingRight = true;
            movingLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rightDown = false;
            movingRight = false;
            movingLeft = leftDown;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            leftDown = true;
            movingLeft = true;
            movingRight = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            leftDown = false;
            movingLeft = false;
            movingRight = rightDown;
        }

        if (movingRight)
            moveDir = 1;
        else if (movingLeft)
            moveDir = -1;
        else
            moveDir = 0;

        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (IsOnGround())
            rigid.velocity = new Vector2(moveDir * moveSpeed, rigid.velocity.y);
        else
        {
            rigid.velocity += new Vector2(moveDir * moveSpeed * jumpingControl * Time.fixedDeltaTime, 0);
            rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, -moveSpeed, moveSpeed), Mathf.Clamp(rigid.velocity.y, -maxFallSpeed, 100f));
        }
        if (rigid.velocity.x > 0.01)
            playerSprite.localScale = new Vector3(1f, 1f, 1f);
        else if (rigid.velocity.x < -0.01)
            playerSprite.localScale = new Vector3(-1f, 1f, 1f);
        animator.SetFloat("speed", Mathf.Abs(rigid.velocity.x));
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && IsOnGround())
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpVelocity);
            animator.Play("Jump");
        }
    }

    private bool IsOnGround()
    {
        Vector2 size = new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y * 0.5f);
        bool isOnGround = Physics2D.BoxCast(boxCollider.bounds.center, size, 0, Vector2.down, size.y * 0.5f + 0.1f, platformLayer);
        animator.SetBool("isOnGround", isOnGround);
        return isOnGround;
    }

    public void ChangeJumpValues(float jumpVelocity, float gravityScale, float jumpingControl)
    {
        this.jumpVelocity = jumpVelocity;
        rigid.gravityScale = gravityScale;
        this.jumpingControl = jumpingControl;
    }
}
