using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] List<GameObject> SpawnPoints = null;
    public int currentSpawnPoint = 0;
    public CharacterController2D characterController2D;
    float horizontalMove=0f;
    [SerializeField] public float moveSpeed=40f;
    [SerializeField] public bool fireResistant = false;
    [SerializeField] GameObject virtualCamera;
    bool jump = false;
    bool crouch = false;
    public void KillPlayer()
    {
        virtualCamera.SetActive(false);

        gameObject.transform.position=SpawnPoints[currentSpawnPoint].transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        virtualCamera.transform.position = gameObject.transform.position;
        virtualCamera.SetActive(true);
        /*if (GameObject.Find("CM vcam1") != null)
        {
            Debug.Log("active");
        }
        else
        {
            Debug.Log("inactive");
        }*/
    }
 
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")* moveSpeed;
        if (Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
           
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
        }
    }
    private void FixedUpdate()
    {
        
        characterController2D.Move(horizontalMove * Time.deltaTime, crouch, jump);
        jump = false;
    }
}
