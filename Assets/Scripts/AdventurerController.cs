using UnityEngine;
using System.Collections;


public class AdventurerController : MonoBehaviour
{

    public float jumpSpeed;
    public float horizontalSpeed = 10;

    public LayerMask whatIsGround;
    public Transform groundcheck;
    private float groundRadius = 0.5f;
    private bool grounded;

    private bool jump;

    bool facingRight = true;

    private float hAxis;

    private Rigidbody2D theRigidBody;

    private Animator theAnimator;


    void Start()
    {
        jump = false;
        grounded = false;

        theRigidBody = GetComponent<Rigidbody2D>();
        theAnimator = GetComponent<Animator>();

    }

    void Update()
    {

        jump = Input.GetKeyDown(KeyCode.Space);


        hAxis = Input.GetAxis("Horizontal");

        theAnimator.SetFloat("hspeed", Mathf.Abs(hAxis));
        Collider2D colliderWeCollidedWith = Physics2D.OverlapCircle(groundcheck.position, groundRadius, whatIsGround);

        grounded = (bool)colliderWeCollidedWith;

        theAnimator.SetBool("ground", grounded);

        float yVelocity = theRigidBody.velocity.y;

        theAnimator.SetFloat("vspeed", yVelocity);


        if (grounded)
        {
            if ((hAxis > 0) && (facingRight == false))
            {
                Flip();
            }
            else if ((hAxis < 0) && (facingRight == true))
            {
                Flip();
            }
        }

    }

    void FixedUpdate()
    {

        if (grounded && !jump)
        {

            theRigidBody.velocity = new Vector2(horizontalSpeed * hAxis, theRigidBody.velocity.y);
        }
        else if (grounded && jump)
        {

            theRigidBody.velocity = new Vector2(theRigidBody.velocity.x, jumpSpeed);
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }
}
