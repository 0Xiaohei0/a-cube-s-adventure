using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "Leap")
        {
            GameObject.Find("TriggerTextCanvas").transform.Find("LeapText").gameObject.SetActive(true);
        }
        if (gameObject.name == "There is nothing here")
        {
            GameObject.Find("TriggerTextCanvas").transform.Find("NothingText1").gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.name == "Leap")
        {
            GameObject.Find("TriggerTextCanvas").transform.Find("LeapText").gameObject.SetActive(false);
        }
        if (gameObject.name == "There is nothing here")
        {
            GameObject.Find("TriggerTextCanvas").transform.Find("NothingText1").gameObject.SetActive(false);
        }
    }
}
