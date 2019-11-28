using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPointBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "SpawnPoint0")
        {
            FindObjectOfType<playerMovement>().currentSpawnPoint = 0;
                }
        if (gameObject.name == "SpawnPoint1")
        {
            FindObjectOfType<playerMovement>().currentSpawnPoint = 1;
        }
        if (gameObject.name == "SpawnPoint2")
        {
            FindObjectOfType<playerMovement>().currentSpawnPoint = 2;
        }
        
            FindObjectOfType<TextManager>().ChangeSpawnPointText();
        
    }
}
