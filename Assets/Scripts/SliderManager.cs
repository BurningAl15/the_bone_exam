using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{

    [Header("UI Components")]
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI textMesh;

    [Header("Logic Slider Properties")]
    [SerializeField] float maxValue;
    [SerializeField] float value;
    [Space]
    [SerializeField] List<ElementContainer> containerElements = new List<ElementContainer>();

    void Awake()
    {
        slider.maxValue = maxValue;
        UpdateValue();
    }

    public void SetValue(float _value)
    {
        slider.value = _value;
        UpdateValue();
    }

    public void SetValueButton(bool _)
    {
        slider.value = _ ? Mathf.Clamp(slider.value + 1, 0, maxValue) : Mathf.Clamp(slider.value - 1, 0, maxValue);
        UpdateValue();
    }

    void UpdateValue()
    {
        value = slider.value;
        textMesh.text = value.ToString();
        for (int i = 0; i < containerElements.Count; i++)
        {
            containerElements[i].UpdateChemistryValue(value / maxValue);
        }
    }
}
