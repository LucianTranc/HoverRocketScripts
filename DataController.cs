using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{

    public static DataController instance;

    public PlayerData data;

    void Awake() {

        if (instance == null) 
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        data = SaveSystem.LoadPlayer();
    }

    void Start() {
    }

    public void SavePlayerData () {
        SaveSystem.SavePlayer(data);
    }

    public void adjustCameraZoom(float newZoom) {

        data.cameraZoom = newZoom;
        SaveSystem.SavePlayer(data);

    }

    public void adjustTiltSensitivity(float newTilt) {

        data.tiltSensitivity = newTilt;
        SaveSystem.SavePlayer(data);

    }

    public void toggleMusic(bool musicStatus) {

        data.musicOn = musicStatus;
        SaveSystem.SavePlayer(data);
        AudioManager.instance.updateMusic();

    }
    public void toggleSFX(bool sfxStatus) {

        data.sfxOn = sfxStatus;
        SaveSystem.SavePlayer(data);
        AudioManager.instance.sfxEnabled = sfxStatus;

    }

    public void deletePlayerData() {

        SaveSystem.DeleteData();
        data = SaveSystem.LoadPlayer();

    }
    
}
