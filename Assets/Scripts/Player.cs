using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float xInput;
    private int facingDir = 1;
    private bool facingRight = false;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGround;
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

        FlipController(); // ���� ȸ��

        AinmatorController(); // �ִϸ��̼�
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
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if(isGround)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void AinmatorController()
    {
        bool isMoving = rb.velocity.x != 0; // 0�� �ƴҰ�� true
        anim.SetBool("isMoving", isMoving);
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
