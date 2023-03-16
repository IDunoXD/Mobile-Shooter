using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    void OnEnable(){
        GameManager.Pause();
    }
    public void Resume(){
        GameManager.Resume();
    }
    public void Restart(){
        GameManager.Resume();
        SceneManager.LoadScene("Game");
    }
    public void Quit(){
        Application.Quit();
    }
}
