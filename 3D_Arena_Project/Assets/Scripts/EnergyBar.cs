using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] Energy energy;
    [SerializeField] Gradient gradient;
    [SerializeField] RectTransform barOrigin;
    [SerializeField] Image energyImage;
    [SerializeField] TMP_Text energyText;
    Vector3 updatedScale;
    float energyPercentage;
    void Start(){
        SetEnergy(energy.MaxEnergy, energy.energy);
    }
    void OnEnable(){
        energy.energyUpdate += SetEnergy;
    }
    void OnDisable(){
        energy.energyUpdate -= SetEnergy;
    }
    public void SetEnergy(float max, float curent){
        energyPercentage = curent/max;
        updatedScale = barOrigin.localScale;
        updatedScale.x = energyPercentage;

        barOrigin.localScale = updatedScale;
        energyImage.color = gradient.Evaluate(energyPercentage);
        energyText.text = curent + "/" + max;
    }
}
