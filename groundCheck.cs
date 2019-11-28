using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DP")
        {
            /*if (gameObject.name == "ground hitbox" || gameObject.name == "rightWall hitbox" || gameObject.name == "leftWall hitbox")
            {*/
            //Debug.Log("22");
                collision.GetComponent<DisappearingPlatform>().PlayerOnPlatform();
            //}
        }
        if (collision.tag == "ground" || collision.tag == "DP")
        {
            if (gameObject.name == "rightWall hitbox")
            {
                Debug.Log("rightenter");
                gameObject.GetComponentInParent<CharacterController2D>().m_RightWalled = true;
            }
            if (gameObject.name == "leftWall hitbox")
            {
                Debug.Log("leftenter");
                gameObject.GetComponentInParent<CharacterController2D>().m_LeftWalled = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag == "ground" || collision.tag == "DP")
        {
            if (gameObject.name == "rightWall hitbox")
            {
                gameObject.GetComponentInParent<CharacterController2D>().JumpsLeft = 1;
            }
            if (gameObject.name == "leftWall hitbox")
            {
                gameObject.GetComponentInParent<CharacterController2D>().JumpsLeft = 1;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.tag == "ground" || collision.tag == "DP" && gameObject.name == "leftWall hitbox"&& gameObject.GetComponentInParent<CharacterController2D>().m_LeftWalled == true)
        {
           
            gameObject.GetComponentInParent<CharacterController2D>().m_LeftWalled = false;
        }
        if (collision.tag == "ground" || collision.tag == "DP" && gameObject.name == "rightWall hitbox" && gameObject.GetComponentInParent<CharacterController2D>().m_RightWalled == true)
        {
           
            gameObject.GetComponentInParent<CharacterController2D>().m_RightWalled = false;
        }

    }
}
