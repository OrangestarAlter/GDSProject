using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpVelocity = 5f;
    [SerializeField] private float jumpingControl = 2.5f;
    [SerializeField] private LayerMask platformLayer;

    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider;

    private int moveDir = 0;
    private bool rightDown = false;
    private bool leftDown = false;
    private bool movingRight = false;
    private bool movingLeft = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
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

        if (IsOnGround())
            rigid.velocity = new Vector2(moveDir * moveSpeed, rigid.velocity.y);
        else
        {
            rigid.velocity += new Vector2(moveDir * moveSpeed * jumpingControl * Time.deltaTime, 0);
            rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, -moveSpeed, moveSpeed), rigid.velocity.y);
        }
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && IsOnGround())
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpVelocity);
        }
    }

    private bool IsOnGround()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, platformLayer);
    }
}
