using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindObjectOfType<playerMovement>().fireResistant == false)
        {
            FindObjectOfType<playerMovement>().KillPlayer();
        }
        else if(FindObjectOfType<playerMovement>().fireResistant == true)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (FindObjectOfType<playerMovement>().fireResistant == true)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
    }
}
