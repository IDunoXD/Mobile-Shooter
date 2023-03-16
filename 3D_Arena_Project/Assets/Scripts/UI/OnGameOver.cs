using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameOver : MonoBehaviour
{
    [SerializeField] GameObject DeathScreen;
    [SerializeField] GameObject PauseButton;
    void OnEnable(){
        GameManager.OnGameOver += ShowDeathScreen;
    }
    void OnDisable(){
        GameManager.OnGameOver -= ShowDeathScreen;
    }
    void ShowDeathScreen(){
        DeathScreen.SetActive(true);
        PauseButton.SetActive(false);
    }
}
