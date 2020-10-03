using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject endLevelScreen, pauseScreen, optionsScreen, pauseButton;
    public PlayerController gamePlayer;
    public Rigidbody2D playerRigidBody;
    public Sprite pauseImage, playImage;
    public Image pauseButtonImage;
    public Slider tiltSlider, zoomSlider;
    public Toggle musicToggleSwitch;
    public Toggle sfxToggleSwitch;
    public Vector3 unpausedVelocity;
    public float recordedTime, nextLevelUnlockScore;
    public Text timeText;
    public Text highscoreText;
    public Text levelLockedText;
    public Button nextLevelButton;
    //public Button PauseButton;

    // Start is called before the first frame update
    void Start()
    {
        tiltSlider.value = DataController.instance.data.tiltSensitivity;
        zoomSlider.value = DataController.instance.data.cameraZoom;
        musicToggleSwitch.isOn = DataController.instance.data.musicOn;
        sfxToggleSwitch.isOn = DataController.instance.data.sfxOn;

    }

    public void endLevel() {

        gamePlayer.onTeleporter = true;
        playerRigidBody.velocity = Vector3.zero;
        playerRigidBody.isKinematic = true;
        recordedTime = gamePlayer.timer.time;
        if (recordedTime < DataController.instance.data.highscores[gamePlayer.currentLevelIndex-1] || DataController.instance.data.highscores[gamePlayer.currentLevelIndex-1] == 0)
            DataController.instance.data.highscores[gamePlayer.currentLevelIndex-1] = recordedTime;
        DataController.instance.SavePlayerData();
        gamePlayer.timer.stopTimer();
        pauseButton.SetActive(false);
        
        StartCoroutine("TeleportCoroutine");
    }

    public IEnumerator TeleportCoroutine() {
        yield return new WaitForSeconds(gamePlayer.teleportDelay);
        levelLockedText.text = nextLevelUnlockScore.ToString() + " seconds to unlock next level";
        endLevelScreen.SetActive(true);
        timeText.text = "Time: " + recordedTime.ToString("F2");
        highscoreText.text = "Highscore: " + DataController.instance.data.highscores[gamePlayer.currentLevelIndex-1].ToString("F2");
        if (DataController.instance.data.highscores[gamePlayer.currentLevelIndex-1] < nextLevelUnlockScore) {
            nextLevelButton.gameObject.SetActive(true);
            levelLockedText.gameObject.SetActive(false);
            if (DataController.instance.data.levelUnlocked < gamePlayer.currentLevelIndex + 1) {
                DataController.instance.data.levelUnlocked = gamePlayer.currentLevelIndex + 1;
                DataController.instance.SavePlayerData();
            }
        }
        else {
            nextLevelButton.gameObject.SetActive(false);
            levelLockedText.gameObject.SetActive(true);
        }
    }

    public void pauseLevel() {

        if (gamePlayer.isPaused) { //on unpause
            if (gamePlayer.obstacles.Count > 0) {
                foreach (ObstacleController obstacle in gamePlayer.obstacles) {
                    obstacle.unPause();
                }
            }
            pauseScreen.SetActive(false);
            optionsScreen.SetActive(false);
            gamePlayer.isPaused = false;
            playerRigidBody.velocity = unpausedVelocity;
            playerRigidBody.isKinematic = false;
            pauseButtonImage.sprite = pauseImage;
            gamePlayer.timer.unpauseTimer();
        }
        else { //on pause
            if (gamePlayer.obstacles.Count > 0) {
                foreach (ObstacleController obstacle in gamePlayer.obstacles) {
                    obstacle.Pause();
                }
            }
            pauseScreen.SetActive(true);
            gamePlayer.isPaused = true;
            unpausedVelocity = playerRigidBody.velocity;
            playerRigidBody.isKinematic = true;
            playerRigidBody.velocity = Vector3.zero;
            pauseButtonImage.sprite = playImage;
            gamePlayer.timer.pauseTimer();
        }

    }

    public void UIClick() {

        AudioManager.instance.UIClick();

    }

}
