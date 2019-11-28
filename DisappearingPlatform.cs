using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private bool disappearTimerActive = false;
    private bool reappearTimerActive = false;
    [SerializeField] float disappearTime = 1.2f;
    [SerializeField] float disappearTimer;
    [SerializeField] float reappearTime = 1.2f;
    [SerializeField] float reappearTimer;

    public void PlayerOnPlatform()
    {
        if (disappearTimerActive == false)
        {
            Debug.Log("33");
            disappearTimer = disappearTime;
            disappearTimerActive = true;
        }
    }
        
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.name == "Player")
        {
            disappearTimer = time;
            timerActive = true;

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            disappearTimer = time;
            timerActive = true;

            // Destroy(gameObject, 1.2f);
        }



    }*/
    private void Update()
    {
        if (disappearTimerActive == true)
        {
            disappearTimer -= Time.deltaTime;
            if (disappearTimer < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                disappearTimerActive = false;
                reappearTimerActive = true;
                reappearTimer = reappearTime;
            }
        }
        if (reappearTimerActive == true)
        {
            reappearTimer -= Time.deltaTime;
            if (reappearTimer < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                reappearTimerActive = false;
               
            }
        }
    }
}
