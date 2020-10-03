using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    //public PlayerData data;
    public Slider tiltSlider;
    public Slider cameraSlider;
    public Toggle musicToggleSwitch;
    public Toggle sfxToggleSwitch;
    public Text testText;
    public Image background;
    public RectTransform backgroundTransform; 
    private Vector2 startBackgroundPos;
    public float scrollSpeed;
    public List<Button> levelButtons;

    // Start is called before the first frame update
    void Start()
    {

        //data = SaveSystem.LoadPlayer();

        tiltSlider.value = DataController.instance.data.tiltSensitivity;
        cameraSlider.value = DataController.instance.data.cameraZoom;

        musicToggleSwitch.isOn = DataController.instance.data.musicOn;
        sfxToggleSwitch.isOn = DataController.instance.data.sfxOn;

        startBackgroundPos = background.rectTransform.anchoredPosition;
        backgroundTransform = background.rectTransform;

        setLevelButtons();        

    }


    private void setLevelButtons() {

        int levelIterator = 1;
        foreach(Button button in levelButtons) {
        
            if (levelIterator <= DataController.instance.data.levelUnlocked || levelIterator == 1) {
                button.interactable = true;
                button.GetComponent<Image>().enabled = false;
            }
            else {
                button.interactable = false;
                button.GetComponent<Image>().enabled = true;

            }

            levelIterator++;
        }

    }


    void Update() {

        backgroundTransform.anchoredPosition = new Vector2(startBackgroundPos.x, startBackgroundPos.y + Mathf.Repeat(Time.time * scrollSpeed, 3072));

    }


    public void adjustCameraZoom(float newZoom) {

        DataController.instance.adjustCameraZoom(newZoom);

    }

    public void adjustTiltSensitivity(float newTilt) {

        DataController.instance.adjustTiltSensitivity(newTilt);

    }

    public void toggleMusic(bool musicStatus) {

        DataController.instance.toggleMusic(musicStatus);

    }
    public void toggleSFX(bool sfxStatus) {

        DataController.instance.toggleSFX(sfxStatus);

    }

    public void UIClick() {

        AudioManager.instance.UIClick();

    }

    public void deletePlayerData() {

        DataController.instance.deletePlayerData();
        tiltSlider.value = DataController.instance.data.tiltSensitivity;
        cameraSlider.value = DataController.instance.data.cameraZoom;
        setLevelButtons();

    }

}
