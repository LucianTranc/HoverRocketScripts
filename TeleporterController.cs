using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{


    //public bool flagReached;
    public Animator flagAnimator;
    public MenuManager gameMenu;
    public PlayerController gamePlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "LandingGear") {
            gamePlayer.onTeleporter = true;
            if (!gamePlayer.isDead) {

                flagAnimator.SetBool("FlagReached", true);
                gameMenu.endLevel();
                AudioManager.instance.PlaySfx("Teleport");
            }
        }

    }

    void OnTriggerExit2D(Collider2D other) {

        if (other.tag == "LandingGear") {
            gamePlayer.onTeleporter = false;
            flagAnimator.SetBool("FlagReached", false);
            gamePlayer.playerAnimation.SetBool("isOnFlag", false);
        }

    }

}
