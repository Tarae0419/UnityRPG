using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int _hp;
    public int _mp;
    public float maxSpeed;
    public float jumpForce;
    public bool isGround;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    private Rigidbody2D rigid;

    private void Awake()
    {
        _hp = 100;
        _mp = 100;
        maxSpeed = 5f;
        jumpForce = 5f;


        rigid =  gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        isGround = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
        Move();
        Stop();
        Jump();
    }
    void Move()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * direction, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if(rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

    }

    private void Stop()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.75f, rigid.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGround)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
    }

}

