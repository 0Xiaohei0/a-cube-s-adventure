using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField] List<GameObject> levels = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (gameObject.name == "Objective")
            {
                FindObjectOfType<CharacterController2D>().resetUpgrades();
                FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>().LoadNextScene();

            }
            if (gameObject.name == "JumpUpgrade1")
            {
                collision.GetComponent<CharacterController2D>().m_JumpForce = 600f;
                FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeUpgradeText("Jump Ability Aquired, press space to jump");

            }
            if (gameObject.name == "SpeedUpgrade1")
            {
                collision.GetComponent<playerMovement>().moveSpeed = 50f;
                FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeUpgradeText("Speed Upgrade I Aquired");
            }
            if (gameObject.name == "SpeedUpgrade2")
            {
                collision.GetComponent<playerMovement>().moveSpeed = 40f;
                FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeUpgradeText("Speed Upgrade II Aquired");
            }
            if (gameObject.name == "FireResistant")
            {
                collision.GetComponent<playerMovement>().fireResistant = true;
                FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeUpgradeText("Ability: Fire Resistance Aquired");
            }
            if (gameObject.name == "doubleJumpUpgrade")
            {
                collision.GetComponent<CharacterController2D>().extraJump = 1;
                collision.GetComponent<CharacterController2D>().JumpsLeft = 1;
                FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeUpgradeText("Ability: Double Jump Aquired");
            }

            if (gameObject.name == "LavalandAchievement")
            {

                FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeUpgradeText("Achievement Unlocked: Lavaland");
            }
            if (gameObject.name == "Lv2Trigger")
            {

                levels[0].SetActive(true);
                FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeUpgradeText("Lv2 Unlocked");
            }
            if (gameObject.name == "WallJumpUpgrade")
            {
                collision.GetComponent<CharacterController2D>().CanWallJump = true;
                FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeUpgradeText("Ability: Wall Jump Aquired");
            }
            if (gameObject.name == "DashUpgrade1")
            {
                collision.GetComponent<CharacterController2D>().dash = true;
                FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeUpgradeText("Ability: Dash Aquired(leftControl)");
            }

            /*if (gameObject.name == "FOVUpgrade1")
            {
                FindObjectOfType<GameLogic>().ChangeFOV(5);
                 FindObjectOfType<TextManager>().GetComponent<TextManager>().ChangeText("Speed Upgrade II Aquired");
            }*/
            Destroy(gameObject);
        }
    }
}
