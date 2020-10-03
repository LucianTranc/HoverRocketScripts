using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public PlayerController gamePlayer;
    public Rigidbody2D playerRigidBody;
    public float respawnDelay;
    public float respawnAnimationDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Respawn() {
        StartCoroutine("RespawnCoroutine");        
    }
    public IEnumerator RespawnCoroutine() {
        //gamePlayer.gameObject.SetActive(false);
        playerRigidBody.velocity = Vector3.zero;
        playerRigidBody.isKinematic = true;
        yield return new WaitForSeconds(respawnDelay);
        AudioManager.instance.PlaySfx("Spawn");
        playerRigidBody.isKinematic = false;
        gamePlayer.transform.position = gamePlayer.respawnPoint;
        gamePlayer.isDead = false;
        gamePlayer.transform.rotation = Quaternion.identity;
        gamePlayer.isRespawning = true;
        if (gamePlayer.obstacles.Count > 0) {
            foreach (ObstacleController obstacle in gamePlayer.obstacles) {
                obstacle.ResetPosition();
            }
        }
        StartCoroutine("RespawnAnimationCoroutine");
        //gamePlayer.gameObject.SetActive(true);
    }

    public IEnumerator RespawnAnimationCoroutine() {

        yield return new WaitForSeconds(respawnAnimationDelay);
        gamePlayer.isRespawning = false;
        gamePlayer.timer.startTimer();
        
    }

}
