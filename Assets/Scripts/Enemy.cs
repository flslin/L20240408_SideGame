using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    bool isAttack;

    [Header("Move info")]
    [SerializeField] private float moveSpeed;
    
    [Header("Player detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetection;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayerDetection)
        {
            if (isPlayerDetection.distance > 1)
            {
                rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);

                Debug.Log("플레이어 찾기");
                isAttack = false;
            }
            else
            {
                Debug.Log("공격" + isPlayerDetection);
                isAttack = true;
            }
        }

        if (!isGround || isWall)
        {
            Flip();
        }

        Movement();
    }

    private void Movement()
    {
        if (!isAttack)
            rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        isPlayerDetection = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }
}
