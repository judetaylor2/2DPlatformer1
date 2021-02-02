using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayerMask;

    public float moveSpeed;
    public float jumpHeight;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2d;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsTouchingWall();
        IsGrounded();
        //makes played move on the x axis
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed , rb.velocity.y);

        //makes played move on the y axis if the space key is pressed and the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
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

    public bool IsTouchingWall()
    {
        float extraHeightText = 0.25f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.right, boxCollider2d.bounds.extents.x + extraHeightText, groundLayerMask);
        RaycastHit2D raycastHit2 = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.left, boxCollider2d.bounds.extents.x + extraHeightText, groundLayerMask);

        Color raycastColour;

        if (raycastHit.collider != null)
        {
            raycastColour = Color.green;
        }
        else
        {
            raycastColour = Color.red;
        }

        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.right * (boxCollider2d.bounds.extents.x + extraHeightText));
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.left * (boxCollider2d.bounds.extents.x + extraHeightText));

        bool result;

        if (raycastHit.collider != null && raycastHit.collider != null)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result != false;
    }
}
