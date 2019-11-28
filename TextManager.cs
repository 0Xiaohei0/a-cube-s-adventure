using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject textObject = null;
    [SerializeField] GameObject checkPointTextObject = null;
    [SerializeField] float UpgradeTextDuration=3f;
    [SerializeField] float LevelTextDuration = 1f;
    [SerializeField] float mainTimer;
    [SerializeField] float CheckPointTimer;
    [SerializeField] bool mainTextTimerActive=false;
    [SerializeField] bool CheckPointTextTimerActive = false;
    // Start is called before the first frame update
    private void Start()
    {
        //textObject.GetComponent<TextMeshProUGUI>().text = "LV"+" "+ SceneManager.GetActiveScene().buildIndex+1;
        //mainTimer = LevelTextDuration;
        //mainTextTimerActive = true;
    }
    public void ChangeUpgradeText(string text)
    {
        textObject.GetComponent<TextMeshProUGUI>().text = text;
        mainTimer = UpgradeTextDuration;
        mainTextTimerActive = true;
    }
    public void ChangeSpawnPointText()
    {
        checkPointTextObject.GetComponent<TextMeshProUGUI>().text = "Checkpoint Reached!";
        CheckPointTimer = LevelTextDuration;
        CheckPointTextTimerActive = true;
    }
    



    // Update is called once per frame
    void Update()
    {
        if (mainTextTimerActive == true)
        {
            mainTimer -= Time.deltaTime;
            if (mainTimer < 0)
            {
                textObject.GetComponent<TextMeshProUGUI>().text = "";
                mainTextTimerActive = false;
            }
        }
        if (CheckPointTextTimerActive == true)
        {
            CheckPointTimer -= Time.deltaTime;
            if (CheckPointTimer < 0)
            {
                checkPointTextObject.GetComponent<TextMeshProUGUI>().text = "";
                CheckPointTextTimerActive = false;
            }
        }
    }
}
