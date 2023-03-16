using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Energy : MonoBehaviour
{
    public float energy;
    public float MaxEnergy;
    public UnityAction<float, float> energyUpdate;
    public void AddEnergy(float value){
        energy += value;
        energy = Mathf.Clamp(energy, 0, MaxEnergy);
        energyUpdate?.Invoke(MaxEnergy, energy);
    }
    public void RemoveEnergy(float value){
        energy -= value;
        energy = Mathf.Clamp(energy, 0, MaxEnergy);
        energyUpdate?.Invoke(MaxEnergy, energy);
    }
}
