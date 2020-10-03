using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
  
  public float cameraZoom;
  public float tiltSensitivity;
  public float[] highscores;
  public bool musicOn, sfxOn;
  public int levelUnlocked;

  //public float[] scores = new float[10];

  public PlayerData () {

      //Debug.Log("CREATING NEW PLAYER DATA");

      cameraZoom = 15;
      tiltSensitivity = 5;
      highscores = new float[21];
      musicOn = true;
      sfxOn = true;
      levelUnlocked = 1;

  }


}
