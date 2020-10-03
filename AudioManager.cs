using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

/*https://www.youtube.com/watch?v=6OT43pvUyfY*/

public class AudioManager : MonoBehaviour
{

    public Sound[] sfx;
    public Sound[] music;
    public static AudioManager instance;
    public bool sfxEnabled, playingSfx;
    public Sound EngineSound;
    public Sound intro;
    public Sound UISound;

    // Start is called before the first frame update
    void Awake()
    {
        
        if (instance == null) 
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);


        foreach(Sound s in sfx) {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach(Sound s in music) {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    void Start() {


        sfxEnabled = DataController.instance.data.sfxOn;
        intro = Array.Find(music, sound => sound.name == "IntroTheme");
        EngineSound = Array.Find(sfx, sound => sound.name == "RocketThruster");
        UISound = Array.Find(sfx, sound => sound.name == "UIClick");
        startMusicIntro();

        EngineSound.source.volume = 0f;
        EngineSound.source.Play();

    }


    public void startMusicIntro() {

        if (DataController.instance.data.musicOn) {
            PlayMusic("IntroTheme");
            PlayMusicDelayed("Theme", intro.clip.length);

        }
        else {
            StopMusic("Theme");
            StopMusic("IntroTheme");
        }

    }

    public void updateMusic() {

        if (DataController.instance.data.musicOn)
            PlayMusic("Theme");
        else {
            StopMusic("Theme");
            StopMusic("IntroTheme");

        }

    }

    public void startEngine() {

        float fadeSpeed = 4.0f;

        if (EngineSound.source && EngineSound.source.volume < 0.4 && sfxEnabled) {

            EngineSound.source.volume += (fadeSpeed * Time.deltaTime);

        }

    }

    public void stopEngine() {

        float fadeSpeed = 4.0f;

        if (EngineSound.source && EngineSound.source.volume > 0) {

            EngineSound.source.volume -= (fadeSpeed * Time.deltaTime);

        }

    }

    public void UIClick() {

        if (UISound.source)
            UISound.source.Play();

    }

    public void PlaySfx (string name) {

        Sound s = Array.Find(sfx, sound => sound.name == name);
        if (s == null)
            return;
        
        if (sfxEnabled)
            s.source.Play();

    }
    public void StopSfx (string name) {

        Sound s = Array.Find(sfx, sound => sound.name == name);
        if (s == null)
            return;
        
        if (sfxEnabled)
            s.source.Stop();

    }

    public void PlayMusic (string name) {

        Sound s = Array.Find(music, sound => sound.name == name);
        if (s == null) {
            Debug.Log("Error");
            return;            
        }
        
        s.source.Play();

    }
    public void PlayMusicDelayed (string name, float delay) {

        Sound s = Array.Find(music, sound => sound.name == name);
        if (s == null)
            return;
        
        s.source.PlayDelayed(delay);

    }
    public void StopMusic (string name) {

        Sound s = Array.Find(music, sound => sound.name == name);
        if (s == null) 
            return;

        s.source.Stop();

    }
}
