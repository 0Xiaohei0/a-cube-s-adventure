using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] public float m_JumpForce = 0f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround ;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheckPoint1 = null;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform groundCheckPoint2 = null;                           // A position marking where to check if the player is grounded.
    /*[SerializeField] private Transform wallCheckLeftPoint1 = null;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform wallCheckLeftPoint2 = null;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform wallCheckRightPoint1 = null;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform wallCheckRightPoint2 = null;                           // A position marking where to check if the player is grounded.*/
    [SerializeField] private Transform m_CeilingCheck=null;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider = null;                // A collider that will be disabled when crouching
    [SerializeField] public int extraJump = 0;
    [SerializeField] public int JumpsLeft;

    [SerializeField] public bool CanWallJump = false;
    [SerializeField] private bool WallSliding = false;
    [SerializeField] private float WallSlideSpeedMax = 3;
    public Vector2 wallJumpClimb=new Vector2(7.5f,6f);
    public Vector2 wallJumpOff = new Vector2(8.5f, 7f);
    public Vector2 wallJumpLeap = new Vector2(18f, 17f);
    int wallDirX;
    [SerializeField] float timeToWallUnstick = 0;
    [SerializeField] float wallStickingTime = 0.1f;

    [SerializeField] public bool dash = false;
    [SerializeField] public bool CanDash = false;
    [SerializeField] private float dashForce=200;
    [SerializeField] private float startDashTime = 0.1f;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashdir;
    // float fJumpPressedRemember = 0;
    // [SerializeField]float fJumpPressedRememberTime = 0.2f;
    [SerializeField]float fGroundedRemember = 0;
    [SerializeField]float fGroundedRememberTime = 0.1f;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    [SerializeField] private bool m_Grounded;            // Whether or not the player is grounded.
    [SerializeField] public bool m_LeftWalled;            // Whether or not the player is grounded.
    [SerializeField] public bool m_RightWalled;            // Whether or not the player is grounded.
    //[SerializeField] private bool m_Walled;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    playerMovement playermovement;
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;
    
    private void Awake()
    {
       playermovement =FindObjectOfType<playerMovement>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
        dashTime = startDashTime;
    }
    public void resetUpgrades()
    {
        m_JumpForce = 0f;
        FindObjectOfType<playerMovement>().moveSpeed = 40f;
        playermovement.fireResistant = false;
        extraJump = 0;
        CanWallJump = false;
    }
    private void FixedUpdate()
    {
       // Debug.Log(Physics2D.OverlapArea(new Vector2(wallCheckLeftPoint1.transform.position.x, wallCheckLeftPoint1.transform.position.y), new Vector2(wallCheckLeftPoint2.transform.position.x, wallCheckLeftPoint2.transform.position.y), m_WhatIsGround));
        m_Grounded = Physics2D.OverlapArea(new Vector2(groundCheckPoint1.transform.position.x, groundCheckPoint1.transform.position.y), new Vector2(groundCheckPoint2.transform.position.x, groundCheckPoint2.transform.position.y), m_WhatIsGround);
        if (m_Grounded)
        {
            fGroundedRemember = fGroundedRememberTime;
        }
       
       /* if (Physics2D.OverlapArea(new Vector2(wallCheckRightPoint1.transform.position.x, wallCheckRightPoint1.transform.position.y),
            new Vector2(wallCheckRightPoint2.transform.position.x, wallCheckRightPoint2.transform.position.y),
            m_WhatIsGround)!=null)
        {
            m_RightWalled = true;
        }
        else
        {
            m_RightWalled = false;
        }
       
        if (Physics2D.OverlapArea(new Vector2(wallCheckLeftPoint1.transform.position.x, wallCheckLeftPoint1.transform.position.y),
           new Vector2(wallCheckLeftPoint2.transform.position.x, wallCheckLeftPoint2.transform.position.y),
           m_WhatIsGround) != null)
        {
            m_LeftWalled = true;
        }
        else
        {
            m_LeftWalled = false;
        }*/
        fGroundedRemember -= Time.deltaTime;
      
       if(CanWallJump && m_LeftWalled==true || m_RightWalled == true && m_Rigidbody2D.velocity.y < 0)
         {
             WallSliding = true;
             if (m_Rigidbody2D.velocity.y < -WallSlideSpeedMax)
             {
                 m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,- WallSlideSpeedMax);
             }
             if (timeToWallUnstick > 0)
             {

                 m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);

                 if (Input.GetAxisRaw("Horizontal") != wallDirX && Input.GetAxisRaw("Horizontal") != 0)
                 {
                     timeToWallUnstick -= Time.deltaTime;
                 }
                 else
                 {
                     timeToWallUnstick = wallStickingTime;
                 }
             }
             else
             {
                 timeToWallUnstick = wallStickingTime;
             }
         }
        else
        {
            WallSliding = false;
        }
         if (m_LeftWalled)
         {
             wallDirX = -1;
         }
         if (m_RightWalled)
         {
             wallDirX = 1;
         }
         if (Input.GetKeyDown(KeyCode.D))
             {
                 dashdir = 1;
             }
             if (Input.GetKeyDown(KeyCode.A))
             {
                 dashdir = -1;
             }



        /*bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapArea(new Vector2(groundCheckPoint1.transform.position.x, groundCheckPoint1.transform.position.y), groundCheckPoint1 m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }*/
    }
   /* private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(m_GroundCheck.position, k_GroundedRadius);
    }
    */


    public void Move(float move, bool crouch, bool jump)
    {
       // Debug.Log(m_Grounded);

        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }


                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            
           /* // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }*/
        }
        // If the player should jump...
        if (m_Grounded) {
            JumpsLeft =  extraJump;
            fGroundedRemember = fGroundedRememberTime;
        }
       
       
        if (dash ==true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (CanDash == true)
                {
                    dashTime -= Time.deltaTime;
                    CanDash = false;
                    if (dashTime <= 0)
                    {
                        CanDash = true;
                        dashTime = startDashTime;
                        m_Rigidbody2D.velocity = Vector2.zero;
                    }

                    if (dashdir == 1)
                    {
                        m_Rigidbody2D.velocity = Vector2.right * dashForce;
                    }
                    else if (dashdir == -1)
                    {
                        m_Rigidbody2D.velocity = Vector2.left * dashForce;
                    }
                }
            }
        }
        if (m_Grounded==false && jump)
        {
            if (CanWallJump == true)
            {
                if (WallSliding)
                {
                    JumpsLeft = 1;
                    if (wallDirX == Input.GetAxisRaw("Horizontal"))
                    {
                        m_Rigidbody2D.velocity =new Vector2( -wallDirX * wallJumpClimb.x, wallJumpClimb.y);
                    }
                    else if (Input.GetAxisRaw("Horizontal") == 0)
                    {
                        m_Rigidbody2D.velocity = new Vector2(-wallDirX * wallJumpOff.x,wallJumpOff.y);
                    }
                    else
                    {
                        m_Rigidbody2D.velocity = new Vector2(-wallDirX * wallJumpLeap.x, wallJumpLeap.y);
                        
                    }
                }
            }
            if ((fGroundedRemember > 0))
            {
                
                fGroundedRemember = 0;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
            else if (JumpsLeft > 0)
            {
                JumpsLeft--;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
               m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }
        if (m_Grounded && jump)
        {
            
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}