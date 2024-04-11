using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashDuration;
     private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    private float xInput;
    private int facingDir = 1;
    private bool facingRight = false;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGround;

    [Header("Attack info")]
    [SerializeField]private float comboTime = .3f;
    private float comboTimeWindow;

    private bool isAttack;
    private int comboCounter;

    //private bool isMoving; // �Լ� �������� ����ϹǷ� �������� ����

    // Start is called before the first frame update
    void Start()
    {
        // ĳ��
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponentInChildren<Animator>(); // �ڽ� ������Ʈ�� �������°�
        //rb.velocity = new Vector2 (5, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); // ������

        CheckInput(); // �Է� üũ

        CollisionChecks(); // ���� ��Ҵ��� üũ

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;

        comboTimeWindow -= Time.deltaTime;
        

        FlipController(); // ���� ȸ��

        AinmatorController(); // �ִϸ��̼�
    }

    public void AttackOver()
    {
        isAttack = false;
        comboCounter++;

        if (comboCounter > 2)
            comboCounter = 0;
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0 && !isAttack)
        {
        dashCooldownTimer = dashCooldown;
        dashTime = dashDuration;
        }
    }

    private void CollisionChecks()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }

        //if(Input.GetMouseButtonDown(0))
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }
    }

    private void StartAttackEvent()
    {
        if (comboTimeWindow < 0)
            comboCounter = 0;

        isAttack = true;
        comboTimeWindow = comboTime;
    }

    private void Movement()
    {
        if(isAttack)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (dashTime > 0)
        {
            rb.velocity = new Vector2(xInput * dashSpeed, /*rb.velocity.y*/0);
        }
        else
        {
            rb.velocity = new Vector3(xInput * moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (isGround)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void AinmatorController()
    {
        bool isMoving = rb.velocity.x != 0; // 0�� �ƴҰ�� true

        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGround", isGround);

        anim.SetBool("isDash", dashTime > 0);

        anim.SetBool("isAttack", isAttack);
        anim.SetInteger("comboCounter", comboCounter);
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.velocity.x < 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x > 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
