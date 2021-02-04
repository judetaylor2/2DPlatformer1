using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;

    public float moveSpeed;
    public float jumpHeight;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2d;

    float defaultMoveSpeed;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        defaultMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        //makes played move on the x axis
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);

        anim.SetBool("isPlayerGrounded", IsGrounded());

        if (Input.GetAxis("Horizontal") != 0 && IsGrounded())
        {
            anim.SetBool("isPlayerRunning", true);
        }
        else
        {
            anim.SetBool("isPlayerRunning", false);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //makes played move on the y axis if the space key is pressed and the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        //attack
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("isPlayerAttacking", true);
        }
        else
        {
            anim.SetBool("isPlayerAttacking", false);
        }




    }

    public bool IsGrounded()
    {
        float extraHeightText = 0.25f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText, groundLayerMask);
        
        Color raycastColour;

        if (raycastHit.collider != null)
        {
            raycastColour = Color.green;
        }
        else
        {
            raycastColour = Color.red;
        }

        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText));

        return raycastHit.collider != null;
    }

}
