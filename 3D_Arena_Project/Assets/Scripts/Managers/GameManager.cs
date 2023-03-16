using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static bool GamePaused = false;
    public static bool GameOver = false;
    public static UnityAction OnGameOver;
    void OnEnable(){
        OnGameOver += UpdateGameState;
    }
    void OnDisable(){
        OnGameOver += UpdateGameState;
    }
    void UpdateGameState(){
        GameOver = true;
    }
    public static void Pause(){
        Time.timeScale = 0;
        GamePaused = true;
    }
    public static void Resume(){
        Time.timeScale = 1;
        GamePaused = false;
    }
}
