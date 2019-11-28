using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherBehavior : MonoBehaviour
{
    Vector3 crusherPosition;
    //[SerializeField] float movetime = 3f;
    [SerializeField] float downspeed = 3f;
    [SerializeField] float upspeed = 3f;
    private Rigidbody2D m_Rigidbody2D;
    private bool goingUp=false;
    float groundWaitTime=1f;
    //private Vector3 m_Velocity = Vector3.zero;
    //[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    // Start is called before the first frame update
    void Start()
    {
        crusherPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(crusherPosition);
        //Debug.Log(goingUp);
        if (gameObject.transform.position.y > crusherPosition.y)
         {
             goingUp = false;
         }
         
         if ( goingUp == false)
         {
            // Debug.Log("22");
             m_Rigidbody2D.velocity =new Vector2(0, downspeed * -1f);
             // Move the character by finding the target velocity
             //Vector3 targetVelocity = new Vector2(0, downspeed * 10f*-1f);
             // And then smoothing it out and applying it to the character
             //m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
             // movetime -= Time.deltaTime;
         }
        if (goingUp == true)
        {
          //  Debug.Log("22");
            m_Rigidbody2D.velocity = new Vector2(0, upspeed);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            collision.gameObject.GetComponent<playerMovement>().KillPlayer();
        }
        m_Rigidbody2D.velocity = new Vector2(0,0);
        groundWaitTime = 1f;
        while (groundWaitTime > 0)
        {
            groundWaitTime -= Time.deltaTime;
        }
        
        goingUp = true;
       
    }
}
