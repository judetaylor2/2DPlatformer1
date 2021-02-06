using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpHeight;
    float timer;
    bool isGrounded;
    public Animator anim;

    public BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask groundLayerMask;
    RigidbodyConstraints2D rigidbodyConstraints;
    int constraintCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyConstraints = rb.constraints;
        Debug.Log(rigidbodyConstraints);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int randomNumber = Random.Range(1, 3);

        if (timer >= 3)
        {   

            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);

            if (randomNumber == 1)
            {
                transform.localScale = new Vector3(-6, 6, 1);

                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else if (randomNumber == 2)
            {
                transform.localScale = new Vector3(6, 6, 1);

                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }

            timer = 0;
        }

        if (IsGrounded() && constraintCounter < 1)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            constraintCounter++;
        }

        //check if grounded, then play animation.
        if (IsGrounded())
        {
            anim.SetBool("isGrounded", true) ;
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            anim.SetBool("isGrounded", false);
            //rb.constraints -= RigidbodyConstraints2D.FreezePositionX;// | RigidbodyConstraints2D.FreezeRotation;
            constraintCounter = 0;
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
