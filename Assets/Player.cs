using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xInput;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        // Ä³½Ì
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2 (5, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
