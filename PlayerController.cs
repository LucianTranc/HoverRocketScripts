using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public int currentLevelIndex;
    public float spawnDelay, teleportDelay, thrustPower, movement, SideEngineSensitivity, tiltSensitivity;
    public bool isDead, isRespawning, onTeleporter = false, isPaused, engineOn;
    public Rigidbody2D rigidBody;
    public Animator sideEngineRightAnimator, sideEngineLeftAnimator, playerAnimation;
    public LevelManager gameLevelManager;
    public Vector3 respawnPoint;
    public TimerController timer;
    public List<ObstacleController> obstacles;
    public ParticleSystem EngineParticleMiddle, EngineParticleLeft, EngineParticleRight;

    // Start is called before the first frame update
    void Start()
    {        
        tiltSensitivity = DataController.instance.data.tiltSensitivity;

        respawnPoint = transform.position;
        
        StartCoroutine("SpawnCoroutine");
        
    }
    public IEnumerator SpawnCoroutine() {
        AudioManager.instance.PlaySfx("Spawn");
        isRespawning = true;
        rigidBody.isKinematic = true;
        yield return new WaitForSeconds(spawnDelay);
        rigidBody.isKinematic = false;
        isRespawning = false;
        timer.startTimer();
    }

    // Update is called once per frame
    void Update()
    {


        movement = Input.acceleration.x;
        if (movement != 0 && !isDead && !isRespawning && !onTeleporter && !isPaused) {

            transform.Rotate(0, 0, -(movement * tiltSensitivity * Time.deltaTime * 100), Space.Self);

            if (movement < -SideEngineSensitivity) {
                sideEngineRightAnimator.SetBool("SideEngineOn", true);
            }
            else {
                sideEngineRightAnimator.SetBool("SideEngineOn", false);
            }
            if (movement > SideEngineSensitivity) {
                sideEngineLeftAnimator.SetBool("SideEngineOn", true);
            } 
            else {
                sideEngineLeftAnimator.SetBool("SideEngineOn", false);
            }          

        }

        if (Input.touchCount > 0 && !isRespawning && !onTeleporter && !isPaused && !isDead) {
            EngineParticleMiddle.gameObject.SetActive(true);
            EngineParticleLeft.gameObject.SetActive(true);
            EngineParticleRight.gameObject.SetActive(true);
            AudioManager.instance.startEngine();
            engineOn = true;
            playerAnimation.SetBool("EngineOn", true);
            rigidBody.AddRelativeForce(new Vector2(0, thrustPower * Time.deltaTime));

        }
        else {
            EngineParticleMiddle.gameObject.SetActive(false);
            EngineParticleLeft.gameObject.SetActive(false);
            EngineParticleRight.gameObject.SetActive(false);
            AudioManager.instance.stopEngine();
            engineOn = false;
            playerAnimation.SetBool("EngineOn", false);
        }

        if (isDead) {
            playerAnimation.SetBool("IsPlayerDead", true);
            sideEngineRightAnimator.SetBool("SideEngineOn", false);
            sideEngineLeftAnimator.SetBool("SideEngineOn", false);
        }
        else {
            playerAnimation.SetBool("IsPlayerDead", false);
        }

        if (onTeleporter) {
            sideEngineRightAnimator.SetBool("SideEngineOn", false);
            sideEngineLeftAnimator.SetBool("SideEngineOn", false);
            playerAnimation.SetBool("isOnFlag", true);
        }
        
    }


    public void adjustTiltSensitivity(float newTilt) {

        tiltSensitivity = newTilt;
        DataController.instance.adjustTiltSensitivity(newTilt);

    }

    public void toggleMusic(bool musicStatus) {

        DataController.instance.toggleMusic(musicStatus);

    }

    public void toggleSFX(bool sfxStatus) {

        DataController.instance.toggleSFX(sfxStatus);

    }

    public void pausePlayer () {

        isPaused = !isPaused;

    }

    void OnTriggerEnter2D(Collider2D other) {

        if ((other.tag == "Ground" || other.tag == "Obstacle") && !onTeleporter && !isDead) {
            AudioManager.instance.PlaySfx("PlayerDeath");
            timer.stopTimer();
            isDead = true;
            isRespawning = false;
            //AudioManager.instance.StopSfx("RocketThruster");
            gameLevelManager.Respawn();            
        }
    }



}
