using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Slider sliderY;
    [SerializeField] Slider sliderX;
    void Awake(){
        sliderY.value = player.CameraYSensetivity;
        sliderX.value = player.CameraXSensetivity;
    }
    public void VerticalSensetivity(float value){
        player.CameraYSensetivity = value;
    }  
    public void HorizontalSensetivity(float value){
        player.CameraXSensetivity = value;
    }
}
