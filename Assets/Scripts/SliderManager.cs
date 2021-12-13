using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ChemistryType
{
    RED,
    YELLOW,
    GREEN,
    SKYBLUE,
    BLUE,
    PINK
}
[Serializable]
public class ContainerValue
{
    public ChemistryType chemistryType;
    public float maxValue;
    public float currentValue;
    public TextMeshProUGUI text;

    public void UpdateChemistryValue(float updateValue)
    {
        currentValue = Mathf.Lerp(0, maxValue, updateValue);
        text.text = FormatValue();
    }

    string FormatValue()
    {
        return KParse(maxValue) + "/" + KParse(currentValue);
    }
    string KParse(float value)
    {
        return value > 100 ? (value / 1000).ToString("F1") + "K" : (value / 1000).ToString("F1");
    }
}

public class SliderManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float maxValue;
    [SerializeField] float value;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] List<ContainerValue> containerValues = new List<ContainerValue>();

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
        for (int i = 0; i < containerValues.Count; i++)
        {
            containerValues[i].UpdateChemistryValue(value / maxValue);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
