using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Animator sceneTransitionAnimator;
    private int nextlevel;

    void Start() {

        //Application.targetFrameRate = 300;

        sceneTransitionAnimator.SetBool("transition", false);

    }

    public void PlayGame(int level) {

        nextlevel = level;

        sceneTransitionAnimator.SetBool("transition", true);

        StartCoroutine("triggerNextScene");
    
    }
    public IEnumerator triggerNextScene() {
      
        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene(nextlevel);
        
    }
    public void QuitGame() {

        Debug.Log("Quit");
        Application.Quit();

    }
}
