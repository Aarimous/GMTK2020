using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenuCtrl : MonoBehaviour
{

    public static bool IsPaused = false;

    public GameObject PauseMenu;

    public AudioMixer AudioMixer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (IsPaused == true){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
         PauseMenu.SetActive(false);
         Time.timeScale = 1f;
         IsPaused = false;
    }

    public void Pause(){
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void SetVolume(float volume){
          AudioMixer.SetFloat("Volume", volume);
    }

    public void LoadMenu(){
        IsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadHowTo(){
        SceneManager.LoadScene("HowTo");
    }


    public void LoadGame(){
        SceneManager.LoadScene("Main");
    }

    public void QuitGame(){
        IsPaused = false;
        Application.Quit();
    }
}
