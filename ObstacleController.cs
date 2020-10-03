using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    public Transform pos1, pos2, startpos;
    public float respawnTime = 0, pauseTime = 0, unpauseTime = 0, totalPausedTime = 0, speed;
    public PlayerController player;

    void Update()
    {
        if (!player.isPaused)
            transform.localPosition = Vector3.Lerp(pos1.localPosition, pos2.localPosition, Mathf.PingPong((Time.timeSinceLevelLoad - respawnTime - totalPausedTime) * speed, 1));
    }

    public void Pause() {

        pauseTime = Time.timeSinceLevelLoad;

    }
    public void unPause() {

        unpauseTime = Time.timeSinceLevelLoad;
        totalPausedTime += unpauseTime - pauseTime;
    
    }

    public void ResetPosition() 
    {
        totalPausedTime = 0;
        respawnTime = Time.timeSinceLevelLoad;
    }
}
