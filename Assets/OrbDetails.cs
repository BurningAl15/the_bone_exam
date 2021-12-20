using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrbDetails : MonoBehaviour
{
    [SerializeField] Image orbImg;
    [SerializeField] TextMeshProUGUI orbName;
    [SerializeField] TextMeshProUGUI orbMultiplication;
    [SerializeField] SliderManager sliderManager;

    public void UpdateOrbDetails(Sprite _orbImg, string _orbName, bool[] validationArray)
    {
        orbImg.sprite = _orbImg;
        orbName.text = _orbName;
        sliderManager.Init(validationArray);
    }

    public void UpdateOrbMultiplier(float _orbMultiplication)
    {
        orbMultiplication.text = "x" + _orbMultiplication.ToString();
    }
}
