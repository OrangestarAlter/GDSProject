using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float jumpingControl;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private Transform playerSprite;

    [SerializeField] private AudioClip dieClip;

    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    public bool canMove = false;
    private int moveDir = 0;
    private bool rightDown = false;
    private bool leftDown = false;
    private bool movingRight = false;
    private bool movingLeft = false;
    private bool isInSolid = false;
    private bool isInLiquid = false;
    private float moveMultiplier = 1f;
    private float jumpMultiplier = 1f;
    private bool isDead = false;
    private bool isSuffocating = false;
    private float blinkTimer = 0;
    private float dieTimer = 0;

    private void Awake()
    {
        instance = this;
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = playerSprite.GetComponent<Animator>();
        spriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = RespawnPosition.instance.transform.position;
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

        if (canMove)
        {
            Jump();
            if (Input.GetKeyDown(KeyCode.R) && InputController.instance.canInput)
                Die();
        }

        if (isSuffocating && !isDead)
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= 0.1f)
            {
                SetSpriteAlpha(Mathf.Abs(spriteRenderer.color.a - 1f));
                blinkTimer = 0;
            }
            dieTimer += Time.deltaTime;
            if (dieTimer >= 5f)
                Die();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
            Move();
    }

    private void Move()
    {
        float speed = moveSpeed * moveMultiplier;
        if (IsOnGround())
            rigid.velocity = new Vector2(moveDir * speed, rigid.velocity.y);
        else
        {
            rigid.velocity += new Vector2(moveDir * speed * jumpingControl * Time.fixedDeltaTime, 0);
            rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, -speed, speed), Mathf.Clamp(rigid.velocity.y, -maxFallSpeed, 100f));
        }
        if (rigid.velocity.x > 0.01f)
            playerSprite.localScale = new Vector3(1f, 1f, 1f);
        else if (rigid.velocity.x < -0.01f)
            playerSprite.localScale = new Vector3(-1f, 1f, 1f);
        animator.SetFloat("speed", Mathf.Abs(rigid.velocity.x));
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && IsOnGround())
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpVelocity * jumpMultiplier);
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

    public void InSolid()
    {
        if (!isInSolid)
        {
            isInSolid = true;
            canMove = false;
            rigid.simulated = false;
            boxCollider.isTrigger = true;
            animator.speed = 0;
            isSuffocating = true;
        }
    }

    public void OutSolid(bool inAir)
    {
        if (isInSolid)
        {
            isInSolid = false;
            canMove = true;
            rigid.simulated = true;
            boxCollider.isTrigger = false;
            animator.speed = 1f;
            if (inAir)
            {
                isSuffocating = false;
                SetSpriteAlpha(1f);
                dieTimer = 0;
            }
        }
    }

    public void InLiquid()
    {
        if (!isInLiquid)
        {
            isInLiquid = true;
            rigid.drag = 5f;
            moveMultiplier = 0.5f;
            jumpMultiplier = 2f;
            animator.speed = 0.5f;
            isSuffocating = true;
        }
    }

    public void OutLiquid(bool inAir)
    {
        if (isInLiquid)
        {
            isInLiquid = false;
            rigid.drag = 0;
            moveMultiplier = 1f;
            jumpMultiplier = 1f;
            animator.speed = 1f;
            if (inAir)
            {
                isSuffocating = false;
                SetSpriteAlpha(1f);
                dieTimer = 0;
            }
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            rigid.simulated = false;
            boxCollider.enabled = false;
            canMove = false;
            InputController.instance.canInput = false;
            animator.speed = 0;
            GameUI.instance.HideUI();
            audioSource.PlayOneShot(dieClip);
            StartCoroutine(Dying(0.5f));
        }
    }

    IEnumerator Dying(float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            SetSpriteAlpha(Mathf.Lerp(1f, 0, timer / duration));
            timer += Time.deltaTime;
            yield return null;
        }
        SetSpriteAlpha(0);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetSpriteAlpha(float alpha)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
    }
}
