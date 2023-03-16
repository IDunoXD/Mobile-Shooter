using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] EnemiesManager enemies;
    [SerializeField] TMP_Text score;
    void Awake(){
        score.text = "Kills: " + enemies.kills.ToString();
    }
    public void Restart(){
        GameManager.Resume();
        SceneManager.LoadScene("Game");
    }
    public void Quit(){
        Application.Quit();
    }
}
